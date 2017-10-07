using IoCPlus;

public class AbortIfLevelEditorReleasedSinceLevelObjectSpawnStatusIsCommand : Command<bool> {

    [Inject] private LevelEditorReleasedSinceLevelObjectSpawnStatus releasedSinceLevelObjectSpawnStatus;

    protected override void Execute(bool condition) {
        if(releasedSinceLevelObjectSpawnStatus.ReleasedSinceLevelObjectSpawn == condition) {
            Abort();
        }
    }

}
