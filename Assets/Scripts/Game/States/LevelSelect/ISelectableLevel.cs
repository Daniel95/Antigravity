using System;
using UnityEngine;

public interface ISelectableLevel {

    Action<Scenes> OnGoToScene { get; set; }
    int LevelNumber { get; set; }
    LevelProgressState LevelProgressState { get; }
    Vector2 WorldPosition { get; }

    void Instantiate(LevelProgressState levelProgressState, int levelNumber);
    void IncreaseLevelProgressStateTo(LevelProgressState levelProgressState);
    void ApplyLevelNumber();
    void ApplyLevelProgressState();
    void Clicked();   
}
