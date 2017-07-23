using IoCPlus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasUIView : View, ICanvasUI {

    [Inject] private Ref<ICanvasUI> canvasRef;

    [SerializeField] private float dragThresholdInInches = 0.1f;

    private Dictionary<CanvasLayer, Transform> canvasLayers = new Dictionary<CanvasLayer, Transform>();
    private Dictionary<CanvasLayer, Dictionary<string, GameObject>> canvasLayerChildByKey = new Dictionary<CanvasLayer, Dictionary<string, GameObject>>();

    public override void Initialize() {
        canvasRef.Set(this);
    }

    public Transform GetCanvasLayerTransform(CanvasLayer canvasLayer) {
        return canvasLayers[canvasLayer];
    }

    public void AddChildToCanvasLayer(GameObject child, CanvasLayer canvasLayer, string key) {
        Transform layerTransform = canvasLayers[canvasLayer];

        canvasLayerChildByKey[canvasLayer].Add(key, child);

        child.transform.SetParent(layerTransform, false);
    }

    public GameObject GetCanvasLayerChild(CanvasLayer canvasLayer, string key) {
        Dictionary<string, GameObject> layerChilds = canvasLayerChildByKey[canvasLayer];

        GameObject child;
        if (!layerChilds.TryGetValue(key, out child)) {
            Debug.LogWarning("Can't find child id " + key + " in CanvasLayer." + canvasLayer.ToString());
        }

        return child;
    }

    public void DestroyCanvasLayerChild(GameObject child, CanvasLayer canvasLayer, string key, Action onDestroyCompleted = null) {
        canvasLayerChildByKey[canvasLayer].Remove(key);

        PopOutAndDestroyUIView popOutAndDestroyUIView = child.GetComponent<PopOutAndDestroyUIView>();
        if (popOutAndDestroyUIView != null) {
            popOutAndDestroyUIView.PopOutAndDestroy(onDestroyCompleted);
            return;
        }

        View view = child.GetComponent<View>();
        if(view != null) {
            view.Destroy();
            return;
        }

        Destroy(child);
    }

    private void Awake() {
        EventSystem eventSystem = GetComponent<EventSystem>();
        eventSystem.pixelDragThreshold = Mathf.RoundToInt(dragThresholdInInches * Screen.dpi);

        InstantiateLayers();
    }

    private void InstantiateLayers() {
        IEnumerator values = Enum.GetValues(typeof(CanvasLayer)).GetEnumerator();

        RectTransform rectTransform = GetComponent<RectTransform>();

        while (values.MoveNext()) {
            string name = values.Current.ToString();
            GameObject layerGameObject = new GameObject(name, typeof(RectTransform));
            layerGameObject.transform.SetParent(transform, false);
            layerGameObject.GetComponent<RectTransform>().sizeDelta = rectTransform.sizeDelta;

            canvasLayers.Add((CanvasLayer)values.Current, layerGameObject.transform);
            canvasLayerChildByKey.Add((CanvasLayer)values.Current, new Dictionary<string, GameObject>());
        }
    }
}