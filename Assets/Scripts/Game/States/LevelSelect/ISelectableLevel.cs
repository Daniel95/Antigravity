using UnityEngine;

public interface ISelectableLevel {

    int LevelNumber { get; set; }
    LevelProgressState LevelProgressState { get; set; }
    Vector2 WorldPosition { get; }

    void Instantiate(LevelProgressState levelProgressState, int levelNumber);
    void IncreaseLevelProgressStateTo(LevelProgressState levelProgressState);
    void ApplyLevelNumber();
    void ApplyLevelProgressState();
    void Clicked();   
}
