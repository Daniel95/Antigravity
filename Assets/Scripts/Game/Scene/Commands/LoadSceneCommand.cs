using IoCPlus;
using UnityEngine;

public class LoadSceneCommand : Command {

    [Inject] private IContext context;
    [Inject] private SceneState sceneState;

    [Inject] private LoadSceneCompletedEvent loadSceneCompletedEvent;

    [InjectParameter] private Scenes scene;

    protected override void ExecuteOverTime() {
        string sceneName = scene.ToString();

        if (!SceneListCheck.Has(sceneName)) {
            Debug.LogWarning(sceneName + " is not available.");
            Abort();
            return;
        }

        SceneHelper.LoadSceneOverTime(sceneName, OnLoaded);
    }

    private void OnLoaded() {
        sceneState.currentSceneIndex = (int)scene;

        ViewContainerView ViewContainerView = Object.FindObjectOfType<ViewContainerView>();
        if (ViewContainerView != null) {
            context.AddView(ViewContainerView);
        }

        loadSceneCompletedEvent.Dispatch(scene);

        Release();
    }
}
