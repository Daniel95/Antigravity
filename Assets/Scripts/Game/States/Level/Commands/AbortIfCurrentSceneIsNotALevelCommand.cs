using UnityEngine;
using System.Collections;
using IoCPlus;

public class AbortIfCurrentSceneIsNotALevelCommand : Command {

    [Inject] private LevelStatus levelStatus;
    [Inject] private SceneStatus sceneStatus;

    protected override void Execute() {
        int levelNumber = LevelHelper.ConvertSceneToLevelNumber((int)sceneStatus.currentScene);

        if(!LevelHelper.CheckLevelExistence(levelNumber) {

        }
    }
}
