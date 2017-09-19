using IoCPlus;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelEditorLevelSelectButtonView : View {

    [Inject] private LevelEditorLevelSelectButtonClickedEvent levelEditorLevelSelectButtonClickedEvent;

    [SerializeField] private Text buttonText;

    private string levelName;

    private Button button;

    public void SetButtonName(string name) {
        levelName = name;
        buttonText.text = levelName;
    }

    private void OnClick() {
        levelEditorLevelSelectButtonClickedEvent.Dispatch(levelName);
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
