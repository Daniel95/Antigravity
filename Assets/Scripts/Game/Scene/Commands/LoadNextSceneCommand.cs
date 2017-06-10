using IoCPlus;
using UnityEngine;

public class LoadNextSceneCommand : Command {

    [Inject] private IContext context;
    [Inject] private SceneState gameStateModel;

    [Inject] private GoToSceneCompletedEvent loadSceneCompletedEvent;

    private Scenes scene;

    protected override void ExecuteOverTime() {
        int nextSceneIndex = gameStateModel.currentSceneIndex + 1;
        scene = (Scenes)nextSceneIndex;
        string sceneName = scene.ToString();

        if (!SceneListCheck.Has(sceneName)) {
            Debug.LogWarning(sceneName + " is not available.");
            Abort();
            return;
        }

        SceneHelper.LoadSceneOverTime(sceneName, OnLoaded);
    }

    private void OnLoaded() {
        gameStateModel.currentSceneIndex++;

        ViewContainerView ViewContainerView = Object.FindObjectOfType<ViewContainerView>();
        if(ViewContainerView != null) {
            context.AddView(ViewContainerView);
        }

        loadSceneCompletedEvent.Dispatch(scene);

        Release();
    }
}
