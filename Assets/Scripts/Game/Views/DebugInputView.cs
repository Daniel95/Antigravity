using IoCPlus;
using System;
using System.Collections;
using UnityEngine;

public class DebugInputView : View {

    public static DebugInputView Instance { get { return GetInstance(); } }

    public static bool GoToLevelOnStart {
        get { return !string.IsNullOrEmpty(PlayerPrefs.GetString("AutoOpenScene")); }
    }

    public static Scenes SceneOfStartLevel {
        get {
            string sceneName = PlayerPrefs.GetString("AutoOpenScene");
            Scenes startScene = SceneHelper.Scenes.Find(x => x.ToString() == sceneName);
            return startScene;
        }
    }

    [Inject] private SceneStatus sceneStatus;

    [Inject] private Ref<GameStateModel> gameStateModelRef;

    [Inject] private GoToCurrentSceneEvent goToCurrentSceneEvent;
    [Inject] private GoToSceneEvent goToSceneEvent;

    private static DebugInputView instance;

    float distanceDown = 0f;
    float distanceToOpen = -100f;

    public override void Initialize() {
        if(GoToLevelOnStart) {
            StartCoroutine(WaitFrame(GoToStartLevel));
        }
    }

    public void GoToScene(Scenes scene) {
        goToSceneEvent.Dispatch(scene);
    }

    public void CompleteAllLevels() {
        gameStateModelRef.Get().CompletedLevels.Clear();

        int highestLevel = LevelHelper.LevelCount;

        for (int i = 1; i <= highestLevel; i++) {
            if(!LevelHelper.CheckIfLevelExistsWithNumber(i)) {
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

    private static DebugInputView GetInstance() {
        if (instance != null) { return instance; }

        DebugInputView debugInputView = FindObjectOfType<DebugInputView>();
        if (debugInputView == null) {
            Debug.Log("No DebugInputView instance found.");
            return null;
        }
        instance = debugInputView;

        return instance;
    }

    private void GoToStartLevel() {
        goToSceneEvent.Dispatch(SceneOfStartLevel);
    }

    private void Update() {
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

    private IEnumerator WaitFrame(Action onDone) {
        yield return new WaitForEndOfFrame();
        onDone();
    }
}