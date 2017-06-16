using System;
using UnityEngine;

public class SelectableLevel : MonoBehaviour, ISelectableLevel {

    public Action<Scenes> OnGoToScene { get { return onGoToScene; } set { onGoToScene = value; } }
    public int LevelNumber { get { return levelNumber; } set { levelNumber = value; } }
    public LevelProgressState LevelProgressState { get { return levelProgressState; } set { levelProgressState = value; } }
    public Vector2 WorldPosition { get { return transform.position; } }

    private SpriteRenderer spriteRenderer;
    private TextMesh textMesh;
    private ClickableCollider clickableCollider;
    private Action<Scenes> onGoToScene;
    private int levelNumber;
    private LevelProgressState levelProgressState;

    public void Instantiate(LevelProgressState levelProgressState, int levelNumber) {
        this.levelNumber = levelNumber;
        this.levelProgressState = levelProgressState;
    }

    public void IncreaseLevelProgressStateTo(LevelProgressState levelProgressState) {
        bool nextLevelProgressStateIsHigher = levelProgressState > this.levelProgressState;
        if (nextLevelProgressStateIsHigher) {
            this.levelProgressState = levelProgressState;
        }
    }

    public void ApplyLevelProgressState() {
        if (!LevelHelper.CheckLevelExistence(levelNumber)) {
            levelProgressState = LevelProgressState.Unset;
        }

        spriteRenderer.color = LevelStatusAppearances.LevelStatusColors[(int)levelProgressState];
    }

    public void ApplyLevelNumber() {
        textMesh.text = levelNumber.ToString();
    }

    public void Clicked() {
        if (levelProgressState >= LevelProgressState.Unlocked) {
            Scenes sceneLevel = LevelHelper.GetLevelScene(levelNumber);
            if(OnGoToScene != null) {
                OnGoToScene(sceneLevel);
            }
        }
    }

    private void Awake() {
        textMesh = GetComponentInChildren<TextMesh>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        clickableCollider = GetComponent<ClickableCollider>();

        clickableCollider.OnClicked += Clicked;
    }

    private void OnDestroy() {
        OnGoToScene = null;
    }
}
