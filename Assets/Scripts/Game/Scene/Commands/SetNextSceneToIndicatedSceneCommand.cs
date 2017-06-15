using IoCPlus;

public class SetNextSceneToIndicatedSceneCommand : Command<Scenes> {

    [Inject] private SceneStatus sceneStatus;

    protected override void Execute(Scenes scene) {
        sceneStatus.sceneToLoad = scene;
    }
}
