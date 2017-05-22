using IoCPlus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasUIView : View, ICanvasUI {

    [Inject] private Ref<ICanvasUI> canvasRef;

    [SerializeField] private float dragThresholdInInches;

    private Dictionary<CanvasLayer, Transform> layers = new Dictionary<CanvasLayer, Transform>();

    public override void Initialize() {
        base.Initialize();
        canvasRef.Set(this);
    }

    public Vector2 ScreenToCanvas(Vector2 screenPosition) {
        Canvas canvas = GetComponent<Canvas>();
        RectTransform rectTransform = canvas.GetComponent<RectTransform>();

        Vector2 res;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPosition, canvas.worldCamera, out res);

        return res;
    }

    public Vector2 ScaleToCanvas(Vector2 scale) {
        return new Vector2(scale.x / transform.localScale.x, scale.y / transform.localScale.y);
    }

    public float GetCanvasScale() {
        return transform.localScale.x;
    }

    public void AddChild(GameObject child, CanvasLayer layer) {
        Transform layerTransform = layers[layer];
        child.transform.SetParent(layerTransform, false);
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

            layers.Add((CanvasLayer)values.Current, layerGameObject.transform);
        }
    }
}