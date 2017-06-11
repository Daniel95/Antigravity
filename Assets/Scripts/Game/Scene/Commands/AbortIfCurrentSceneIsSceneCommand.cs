using IoCPlus;

public class AbortIfCurrentSceneIsSceneCommand : Command<Scenes> {

    [Inject] private SceneStatus sceneStatus;

    protected override void Execute(Scenes scene) {
        if(sceneStatus.currentScene == scene) {
            Abort();
        }
    }
}
