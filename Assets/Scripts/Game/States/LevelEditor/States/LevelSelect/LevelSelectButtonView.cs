using IoCPlus;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelSelectButtonView : View {

    [Inject] private SetLevelSelectButtonInteractableEvent levelSelectButtonInteractableEvent;
    [Inject] private LevelSelectButtonClickedEvent levelSelectButtonClickedEvent;

    [SerializeField] private Text buttonText;

    private string levelName;

    private Button button;

    public override void Initialize() {
        base.Initialize();
        levelSelectButtonInteractableEvent.AddListener(OnSetInteractable);
    }

    public override void Dispose() {
        base.Dispose();
        levelSelectButtonInteractableEvent.RemoveListener(OnSetInteractable);
    }

    public void SetButtonName(string name) {
        levelName = name;
        buttonText.text = levelName;
    }   

    private void OnClick() {
        levelSelectButtonClickedEvent.Dispatch(levelName);
    }

    private void OnSetInteractable(bool interactable) {
        button.interactable = interactable;
    }

    private void OnEnable() {
        GetButtonComponent();
        button.onClick.AddListener(OnClick);
    }

    private void OnDisable() {
        button.onClick.RemoveListener(OnClick);
    }

    private Button GetButtonComponent() {
        if(button == null) {
            button = GetComponent<Button>();
        }
        return button;
    }

}
