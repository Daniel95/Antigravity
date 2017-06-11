using IoCPlus;
using UnityEngine;

public class LoadNextSceneOverTimeCommand : Command {

    [Inject] private IContext context;
    [Inject] private SceneStatus sceneStatus;
    [Inject] private ViewContainerStatus viewContainerStatus;

    protected override void ExecuteOverTime() {
        string sceneName = sceneStatus.nextScene.ToString();

        if (!SceneListCheck.Has(sceneName)) {
            Debug.LogWarning(sceneName + " is not available.");
            Abort();
            return;
        }

        SceneHelper.LoadSceneOverTime(sceneName, OnLoaded);
    }

    private void OnLoaded() {
        sceneStatus.currentScene = sceneStatus.nextScene;

        viewContainerStatus.ViewContainer = Object.FindObjectOfType<ViewContainer>();
        if (viewContainerStatus.ViewContainer != null) {
            context.AddView(viewContainerStatus.ViewContainer);
        }

        Release();
    }
}
