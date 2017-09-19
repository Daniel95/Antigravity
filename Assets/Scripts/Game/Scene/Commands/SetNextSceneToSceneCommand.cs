using IoCPlus;

public class SetNextSceneToSceneCommand : Command<Scenes> {

    [Inject] private SceneStatus sceneStatus;

    protected override void Execute(Scenes scene) {
        sceneStatus.sceneToLoad = scene;
    }
}
