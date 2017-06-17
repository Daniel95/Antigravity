using IoCPlus;

public class AbortIfCurrentSceneIsNotALevelCommand : Command {

    [Inject] private LevelStatus levelStatus;
    [Inject] private SceneStatus sceneStatus;

    protected override void Execute() {
        int levelNumber = 0;
        if(!LevelHelper.CheckSceneLevelExistence(sceneStatus.currentScene, out levelNumber)) {
            Abort();
        }
    }
}
