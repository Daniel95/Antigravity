using IoCPlus;
using UnityEngine;

public class AbortIfSceneIsNotALevelCommand : Command {

    [InjectParameter] private Scenes scene;

    protected override void Execute() {
        bool currentSceneIsALevel = LevelHelper.CheckIfLevelExistsWithScene(scene);

        if (!currentSceneIsALevel) {
            Abort();
        }
    }
}
