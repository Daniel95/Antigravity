using IoCPlus;

public class SetNextSceneToSceneCommand : Command {

    [Inject] private SceneStatus sceneStatus;

    [InjectParameter] private Scenes scene;

    protected override void Execute() {
        sceneStatus.nextScene = scene;
    }
}
