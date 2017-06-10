using IoCPlus;
using UnityEngine.SceneManagement;

public class ReloadSceneCommand : Command {

    [Inject] private SceneState sceneState;

    protected override void Execute() {
        SceneManager.LoadScene(sceneState.currentSceneIndex);
    }
}
