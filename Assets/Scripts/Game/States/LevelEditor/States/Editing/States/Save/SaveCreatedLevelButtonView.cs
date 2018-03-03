using IoCPlus;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SaveCreatedLevelButtonView : View, ISaveCreatedLevelButton {

    [Inject] private SaveCreatedLevelButtonClickedEvent saveCreatedLevelButtonClickedEvent;

    [Inject] private Ref<ISaveCreatedLevelButton> levelEditorSavingSaveButtonRef;

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
        saveCreatedLevelButtonClickedEvent.Dispatch();
    }

    private void Awake() {
        button = GetComponent<Button>();
    }
}
