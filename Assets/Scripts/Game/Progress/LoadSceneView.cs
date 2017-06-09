using IoCPlus;
using UnityEngine;

public class LoadSceneView : View {

    [SerializeField] private Scenes scene;

    [Inject] private LoadSceneEvent loadSceneEvent;

    public void LoadScene(Scenes scene) {
        loadSceneEvent.Dispatch(scene);
    }
}
