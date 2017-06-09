using IoCPlus;
using UnityEngine;

public class LoadNextSceneCommand : Command {

    [Inject] private IContext context;
    [Inject] private SceneState gameStateModel;

    protected override void ExecuteOverTime() {
        int nextSceneIndex = gameStateModel.currentSceneIndex + 1;
        string sceneName = ((Scenes)nextSceneIndex).ToString();

        if (!SceneListCheck.Has(sceneName)) {
            Debug.LogWarning(sceneName + " is not available.");
            Abort();
            return;
        }

        SceneHelper.LoadSceneOverTime(sceneName, OnLoaded);
    }

    private void OnLoaded() {
        gameStateModel.currentSceneIndex++;

        LevelView level = Object.FindObjectOfType<LevelView>();
        context.AddView(level);

        Release();
    }
}
