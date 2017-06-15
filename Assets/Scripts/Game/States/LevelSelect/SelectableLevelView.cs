using IoCPlus;
using UnityEngine;

public class SelectableLevelView : View, ISelectableLevel {

    public int LevelNumber { get { return levelNumber; } set { levelNumber = value; } }
    public LevelProgressState LevelProgressState { get { return levelProgressState; } set { levelProgressState = value; } }
    public Vector2 WorldPosition { get { return transform.position; } }

    [Inject] private GoToSceneEvent goToSceneEvent;

    private SpriteRenderer spriteRenderer;
    private TextMesh textMesh;

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
        if (LevelHelper.CheckLevelExistence(levelNumber)) {
            spriteRenderer.color = LevelStatusAppearances.LevelStatusColors[(int)levelProgressState];
        } else {
            spriteRenderer.color = LevelStatusAppearances.LevelStatusColors[0];
        }
    }

    public void ApplyLevelNumber() {
        textMesh.text = levelNumber.ToString();
    }

    public void Clicked() {
        if(levelProgressState > LevelProgressState.Locked) {
            Scenes sceneLevel = LevelHelper.GetLevelScene(levelNumber);
            goToSceneEvent.Dispatch(sceneLevel);
        }
    }

    private void Awake() {
        textMesh = GetComponentInChildren<TextMesh>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
