using IoCPlus;

public class SetLastLevelToCurrentSceneCommand : Command {

    [Inject] private LevelStatus levelStatus;
    [Inject] private SceneStatus sceneStatus;

    protected override void Execute() {
        levelStatus.LastLevelNumber = LevelHelper.GetNumberOfLevelWithScene(sceneStatus.currentScene);
    }
}
