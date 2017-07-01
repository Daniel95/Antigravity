using IoCPlus;
using UnityEngine;

public class DebugInputView : View {

    [Inject] private Ref<GameStateModel> gameStateModelRef;

    [Inject] private SceneStatus sceneStatus;

    [Inject] private GoToCurrentSceneEvent goToCurrentSceneEvent;
    [Inject] private GoToSceneEvent goToSceneEvent;

    float distanceDown = 0f;
    float distanceToOpen = -100f;

    void Update() {
        if (Input.touchCount == 3 && !SRDebug.Instance.IsDebugPanelVisible) {
            Touch finger = Input.GetTouch(0);
            distanceDown += finger.deltaPosition.y;

            if (distanceDown < distanceToOpen) {
                distanceDown = 0f;
                SRDebug.Instance.ShowDebugPanel();
            }
        } else {
            distanceDown = 0f;
        }
    }

    public void GoToScene(Scenes scene) {
        goToSceneEvent.Dispatch(scene);
    }

    public void CompleteAllLevels() {
        gameStateModelRef.Get().CompletedLevels.Clear();

        int highestLevel = LevelHelper.LevelCount;

        for (int i = 1; i <= highestLevel; i++) {
            if(!LevelHelper.CheckLevelNumberExistence(i)) {
                highestLevel++;
                continue;
            }
            gameStateModelRef.Get().CompletedLevels.Add(i);
        }

        if (sceneStatus.currentScene == Scenes.LevelSelect) {
            goToCurrentSceneEvent.Dispatch();
        }
    }

    public void DeleteCompletedLevelsSave() {
        gameStateModelRef.Get().CompletedLevels.Clear();

        if (sceneStatus.currentScene == Scenes.LevelSelect) {
            goToCurrentSceneEvent.Dispatch();
        }
    }
}