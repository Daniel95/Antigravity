using IoCPlus;

public class AbortIfReleasedSinceLevelObjectSpawnStatusIsCommand : Command<bool> {

    [Inject] private ReleasedSinceLevelObjectSpawnStatus releasedSinceLevelObjectSpawnStatus;

    protected override void Execute(bool condition) {
        if(releasedSinceLevelObjectSpawnStatus.ReleasedSinceLevelObjectSpawn == condition) {
            Abort();
        }
    }

}
