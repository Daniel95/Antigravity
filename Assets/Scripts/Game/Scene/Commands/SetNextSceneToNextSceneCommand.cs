using IoCPlus;

public class SetNextSceneToNextSceneCommand : Command {

    [Inject] private SceneStatus sceneStatus;

    protected override void Execute() {
        int nextSceneIndex = (int)sceneStatus.currentScene + 1;
        sceneStatus.sceneToLoad = (Scenes)nextSceneIndex;
    }
}
