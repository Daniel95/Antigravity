using IoCPlus;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelEditorLevelSelectButtonView : View {

    [SerializeField] private Text buttonText;

    private Button button;

    public void SetButtonName(string name) {
        buttonText.text = name;
    }

    private void Awake() {
        button = GetComponent<Button>();
    }

}
