using IoCPlus;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelEditorSavingSaveButtonView : View, ILevelEditorSavingSaveButton {

    [Inject] private LevelEditorSavingSaveButtonClickedEvent levelEditorSavingSaveButtonClickedEvent;

    [Inject] private Ref<ILevelEditorSavingSaveButton> levelEditorSavingSaveButtonRef;

    private Button button;

    public override void Initialize() {
        levelEditorSavingSaveButtonRef.Set(this);
        button.onClick.AddListener(OnButtonClick);
    }

    public override void Dispose() {
        button.onClick.RemoveListener(OnButtonClick);
    }

    public void SetInteractable(bool interactable) {
        button.interactable = interactable; 
    }

    private void OnButtonClick() {
        levelEditorSavingSaveButtonClickedEvent.Dispatch();
    }

    private void Awake() {
        button = GetComponent<Button>();
    }
}
