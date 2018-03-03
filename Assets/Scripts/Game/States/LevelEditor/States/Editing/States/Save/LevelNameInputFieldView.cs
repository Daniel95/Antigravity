using IoCPlus;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class LevelNameInputFieldView : View, ILevelNameInputField {

    public string Text { get { return inputField.text; } }

    [Inject] private Ref<ILevelNameInputField> levelNameInputFieldRef;

    [Inject] private LevelNameInputFieldValueChangedEvent levelNameInputFieldValueChangedEvent;

    [Inject] private LevelNameStatus levelNameStatus;

    private InputField inputField;

    public override void Initialize() {
        levelNameInputFieldRef.Set(this);
        inputField.onValueChanged.AddListener(OnValueChanged);
        inputField.onEndEdit.AddListener(OnValueChanged);
        inputField.onEndEdit.AddListener(OnValueChanged);
        FrameHelper.WaitForFrames(1, () => inputField.text = levelNameStatus.Name);
    }

    public override void Dispose() {
        inputField.onValueChanged.RemoveListener(OnValueChanged);
        inputField.onEndEdit.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(string text) {
        levelNameInputFieldValueChangedEvent.Dispatch(text);
    }

    private void Awake() {
        inputField = GetComponent<InputField>();
    }

}
