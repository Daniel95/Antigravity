using IoCPlus;
using UnityEngine;

public class LoadSceneByNameCommand : Command {

    [Inject] private IContext context;
    [Inject] private GameStateModel gameStateModel;

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
        gameStateModel.currentSceneIndex = (int)scene;

        LevelView level = UnityEngine.Object.FindObjectOfType<LevelView>();
        context.AddView(level);

        Release();
    }
}
