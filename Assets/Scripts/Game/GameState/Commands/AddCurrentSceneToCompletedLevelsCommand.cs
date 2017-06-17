using IoCPlus;
using UnityEngine;

public class AddCurrentSceneToCompletedLevelsCommand : Command {

    [Inject] private Ref<GameStateModel> gameStateModel;
    [Inject] private SceneStatus sceneStatus;

    protected override void Execute() {
        int levelNumber = LevelHelper.GetLevelNumber(sceneStatus.currentScene);

        if (!gameStateModel.Get().CompletedLevels.Contains(levelNumber)) {
            gameStateModel.Get().CompletedLevels.Add(levelNumber);
        }
    }

}
