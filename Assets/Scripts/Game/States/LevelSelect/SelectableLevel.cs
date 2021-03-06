﻿using System;
using UnityEngine;

public class SelectableLevel : MonoBehaviour, ISelectableLevel {

    public Action<Scenes> OnGoToScene { get { return onGoToScene; } set { onGoToScene = value; } }
    public int LevelNumber { get { return levelNumber; } set { levelNumber = value; } }
    public LevelProgressState LevelProgressState { get { return levelProgressState; } }
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
        bool nextLevelProgressStateIsHigher = this.levelProgressState < levelProgressState;

        if (nextLevelProgressStateIsHigher) {
            this.levelProgressState = levelProgressState;
        }
    }

    public void ApplyLevelProgressState() {
        if (!LevelHelper.CheckIfLevelExistsWithNumber(levelNumber)) {
            levelProgressState = LevelProgressState.Unset;
        }

        spriteRenderer.color = LevelStatusAppearances.LevelStatusColors[(int)levelProgressState];
    }

    public void ApplyLevelNumber() {
        textMesh.text = levelNumber.ToString();
    }

    public void Clicked() {
        if (levelProgressState >= LevelProgressState.Unlocked) {
            Scenes levelScene = LevelHelper.GetSceneOfLevelWithNumber(levelNumber);
            if(OnGoToScene != null) {
                OnGoToScene(levelScene);
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
