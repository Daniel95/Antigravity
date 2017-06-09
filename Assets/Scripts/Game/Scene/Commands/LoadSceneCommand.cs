using IoCPlus;
using UnityEngine;

public class LoadSceneCommand : Command<Scenes> {

    [Inject] private IContext context;
    [Inject] private SceneState gameStateModel;

    private Scenes scene;

    protected override void ExecuteOverTime(Scenes scene) {
        this.scene = scene;
        string sceneName = scene.ToString();

        if (!SceneListCheck.Has(sceneName)) {
            Debug.LogWarning(sceneName + " is not available.");
            Abort();
            return;
        }

        SceneHelper.LoadSceneOverTime(sceneName, OnLoaded);
    }

    private void OnLoaded() {
        gameStateModel.currentSceneIndex = (int)scene;

        LevelView level = Object.FindObjectOfType<LevelView>();
        context.AddView(level);

        Release();
    }
}
