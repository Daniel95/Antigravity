using IoCPlus;

public class SetNextSceneToCurrentSceneCommand : Command {

    [Inject] private SceneStatus sceneStatus;

    protected override void Execute() {
        sceneStatus.nextScene = sceneStatus.currentScene;
    }
}
