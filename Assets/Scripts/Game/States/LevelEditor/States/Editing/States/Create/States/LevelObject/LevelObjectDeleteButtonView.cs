using IoCPlus;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelObjectDeleteButtonView : View {

    [Inject] private LevelObjectDeleteButtonClickedEvent levelEditorLevelObjectDeleteButtonClickedEvent;

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
        levelEditorLevelObjectDeleteButtonClickedEvent.Dispatch();
    }

    private void Awake() {
        button = GetComponent<Button>();
    }

}
