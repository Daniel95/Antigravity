using IoCPlus;
using UnityEngine;

public class LevelEditorSetReleasedSinceLevelObjectSpawnStatusCommand : Command<bool> {

    [Inject] private LevelEditorReleasedSinceLevelObjectSpawnStatus releasedSinceLevelObjectSpawnStatus;

    protected override void Execute(bool releasedSinceLevelObjectSpawn) {
        releasedSinceLevelObjectSpawnStatus.ReleasedSinceLevelObjectSpawn = releasedSinceLevelObjectSpawn;
    }

}
