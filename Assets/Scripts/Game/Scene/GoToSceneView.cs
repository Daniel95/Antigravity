using IoCPlus;
using UnityEngine;
using UnityEngine.UI;

public class GoToSceneView : View {

    [SerializeField] private Scenes scene;
    [SerializeField] private bool loadOnGameObjectButtonClick = true;

    [Inject] private GoToSceneEvent loadSceneEvent;

    private Button button;

    public void GoToScene() {
        loadSceneEvent.Dispatch(scene);
    }

    private void Awake() {
        if (loadOnGameObjectButtonClick) {
            button = GetComponent<Button>();
        }
    }

    private void OnEnable() {
        if (button != null) {
            button.onClick.AddListener(GoToScene);
        }
    }

    private void OnDisable() {
        if (button != null) {
            button.onClick.RemoveListener(GoToScene);
        }
    }
}
