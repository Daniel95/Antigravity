using IoCPlus;
using UnityEngine;

public class UnloadCurrentSceneOverTimeCommand : Command {

    [Inject] private SceneStatus sceneStatus;

    protected override void ExecuteOverTime() {
        string sceneName = (sceneStatus.currentScene).ToString();

        if (!SceneListCheck.Has(sceneName)) {
            Debug.LogWarning(sceneName + " is not available.");
            Abort();
            return;
        }

        SceneHelper.UnloadSceneOverTime(sceneName, Release);
    }
}
