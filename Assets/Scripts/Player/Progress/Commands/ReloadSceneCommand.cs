using IoCPlus;
using UnityEngine.SceneManagement;

public class ReloadSceneCommand : Command {

    protected override void Execute() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
