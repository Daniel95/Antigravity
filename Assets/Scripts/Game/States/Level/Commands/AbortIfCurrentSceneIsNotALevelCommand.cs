using IoCPlus;

public class AbortIfCurrentSceneIsNotALevelCommand : Command {

    [Inject] private SceneStatus sceneStatus;

    protected override void Execute() {
        bool currentSceneIsALevel = LevelHelper.CheckIfLevelExistsWithScene(sceneStatus.currentScene);
        if (!currentSceneIsALevel) {
            Abort();
        }
    }
}
