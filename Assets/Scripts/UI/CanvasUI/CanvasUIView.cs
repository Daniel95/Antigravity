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

    private Dictionary<CanvasLayer, List<View>> canvasLayerViews = new Dictionary<CanvasLayer, List<View>>();

    public override void Initialize() {
        base.Initialize();
        canvasRef.Set(this);
    }

    public Transform GetCanvasLayerTransform(CanvasLayer canvasLayer) {
        return canvasLayers[canvasLayer];
    }

    public void AddViewToCanvasLayer(View view, CanvasLayer canvasLayer) {
        Transform layerTransform = canvasLayers[canvasLayer];
        canvasLayerViews[canvasLayer].Add(view);
        view.transform.SetParent(layerTransform, false);
    }

    public View GetCanvasLayerContentView(string name, CanvasLayer canvasLayer) {
        View canvasLayerView = canvasLayerViews[canvasLayer].Find(x => x.name == name + "(Clone)");
        if (canvasLayerView == null) {
            Debug.LogWarning("Can't find CanvasLayer view " + name + " in CanvasLayer." + canvasLayer.ToString());
        }

        return canvasLayerView;
    }

    public void RemoveCanvasLayerContentView(View view, CanvasLayer canvasLayer) {
        canvasLayerViews[canvasLayer].Remove(view);
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
            canvasLayerViews.Add((CanvasLayer)values.Current, new List<View>());
        }
    }
}