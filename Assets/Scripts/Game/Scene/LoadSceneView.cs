using IoCPlus;
using UnityEngine;

public class LoadSceneView : View {

    [SerializeField] private Scenes scene;

    [Inject] private GoToSceneEvent loadSceneEvent;

    public void LoadScene() {
        loadSceneEvent.Dispatch(scene);
    }
}
