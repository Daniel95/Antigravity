using IoCPlus;

public class AbortIfCurrentSceneIsNotALevelCommand : Command {

    [Inject] private LevelStatus levelStatus;
    [Inject] private SceneStatus sceneStatus;

    protected override void Execute() {
        int levelNumber = LevelHelper.GetLevelNumber(sceneStatus.currentScene);

        if(!LevelHelper.CheckLevelExistence(levelNumber)) {
            Abort();
        }
    }
}
