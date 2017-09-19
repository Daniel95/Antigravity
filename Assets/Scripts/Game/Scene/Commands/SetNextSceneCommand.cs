using IoCPlus;

public class SetNextSceneCommand : Command {

    [Inject] private SceneStatus sceneStatus;

    [InjectParameter] private Scenes scene;

    protected override void Execute() {
        sceneStatus.sceneToLoad = scene;
    }
}
