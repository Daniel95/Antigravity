﻿using IoCPlus;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class LevelEditorLevelObjectButtonGridLayoutGroupView : View, ILevelEditorLevelObjectButtonGridLayoutGroup {

    [Inject] private IContext context;

    [Inject] private Ref<ICanvasUI> canvasUIRef;

    [Inject] private Ref<ILevelEditorLevelObjectButtonGridLayoutGroup> levelObjectButtonGridLayoutGroup;

    public override void Initialize() {
        base.Initialize();
        levelObjectButtonGridLayoutGroup.Set(this);
    }

    public void InstantiateLevelObjectButtons(string prefabPath) {
        LevelEditorLevelObjectButtonView levelObjectButtonViewPrefab = Resources.Load<LevelEditorLevelObjectButtonView>(prefabPath);

        if(levelObjectButtonViewPrefab == null) {
            Debug.LogWarning("Can't find levelObjectButtonPrefab at path " + prefabPath);
            return;
        }

        foreach (GenerateableLevelObjectNode levelObjectEditorNode in GenerateableLevelObjectLibrary.GenerateableLevelObjectNodes) {
            LevelEditorLevelObjectButtonView levelObjectButtonView = context.InstantiateView(levelObjectButtonViewPrefab);
            levelObjectButtonView.transform.SetParent(transform, false);
            levelObjectButtonView.SetLevelObject(levelObjectEditorNode.LevelObjectType);
        }
    }

}
/*
[Inject] private IContext context;


protected override void Execute(string prefabPath, CanvasLayer canvasLayer) {
    View prefab = Resources.Load<View>(prefabPath);
    if (prefab == null) {
        Debug.LogWarning("Can't instantiate view prefab as no prefab is found at given path '" + prefabPath + "'.");
        return;
    }
    View view = context.InstantiateView(prefab);
    canvasUIRef.Get().AddChildToCanvasLayer(view.gameObject, canvasLayer, prefabPath);
}*/