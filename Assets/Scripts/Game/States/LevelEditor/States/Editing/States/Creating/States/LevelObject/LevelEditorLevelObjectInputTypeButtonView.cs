﻿using IoCPlus;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelEditorLevelObjectInputTypeButtonView : View {

    [SerializeField] private LevelObjectTransformType levelObjectTransformType;

    [Inject] private LevelEditorLevelObjectTransformTypeButtonClickedEvent levelEditorLevelObjectInputTypeButtonClickedEvent;

    private Button button;

    public override void Initialize() {
        base.Initialize();
        button.onClick.AddListener(OnClick);
    }

    public override void Dispose() {
        base.Dispose();
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick() {
        levelEditorLevelObjectInputTypeButtonClickedEvent.Dispatch(levelObjectTransformType);
    }

    private void Awake() {
        button = GetComponent<Button>();
    }

}
