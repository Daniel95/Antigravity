using IoCPlus;

public class SetLastLevelToCurrentSceneCommand : Command {

    [Inject] private LevelStatus levelStatus;
    [Inject] private SceneStatus sceneStatus;

    protected override void Execute() {
        levelStatus.LastLevelNumber = (int)sceneStatus.currentScene;
    }
}
