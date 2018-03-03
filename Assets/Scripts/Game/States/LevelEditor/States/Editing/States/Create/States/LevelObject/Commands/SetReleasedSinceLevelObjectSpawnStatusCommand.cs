using IoCPlus;
using UnityEngine;

public class SetReleasedSinceLevelObjectSpawnStatusCommand : Command<bool> {

    [Inject] private ReleasedSinceLevelObjectSpawnStatus releasedSinceLevelObjectSpawnStatus;

    protected override void Execute(bool releasedSinceLevelObjectSpawn) {
        releasedSinceLevelObjectSpawnStatus.ReleasedSinceLevelObjectSpawn = releasedSinceLevelObjectSpawn;
    }

}
