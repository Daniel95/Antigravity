using IoCPlus;
using UnityEngine;

public class UnloadCurrentSceneCommand : Command {

    [Inject] private SceneState sceneState;

    protected override void ExecuteOverTime() {
        string sceneName = ((Scenes)sceneState.currentSceneIndex).ToString();

        if (!SceneListCheck.Has(sceneName)) {
            Debug.LogWarning(sceneName + " is not available.");
            Abort();
            return;
        }

        SceneHelper.UnloadSceneOverTime(sceneName, Release);
    }
}
