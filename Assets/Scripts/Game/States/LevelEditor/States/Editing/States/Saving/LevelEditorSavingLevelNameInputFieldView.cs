using IoCPlus;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class LevelEditorSavingLevelNameInputFieldView : View, ILevelEditorSavingLevelNameInputField {

    public string Text { get { return inputField.text; } }

    [Inject] private Ref<ILevelEditorSavingLevelNameInputField> levelEditorSavingLevelNameInputFieldRef;

    [Inject] private LevelEditorSavingLevelNameInputFieldValueChangedEvent levelEditorSavingLevelNameInputFieldValueChangedEvent;

    [Inject] private LevelNameStatus levelNameStatus;

    private InputField inputField;

    public override void Initialize() {
        levelEditorSavingLevelNameInputFieldRef.Set(this);
        inputField.onValueChanged.AddListener(OnValueChanged);
        FrameHelper.WaitForFrames(1, () => inputField.text = levelNameStatus.Name);
    }

    public override void Dispose() {
        inputField.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(string text) {
        levelEditorSavingLevelNameInputFieldValueChangedEvent.Dispatch(text);
    }

    private void Awake() {
        inputField = GetComponent<InputField>();
    }

}
