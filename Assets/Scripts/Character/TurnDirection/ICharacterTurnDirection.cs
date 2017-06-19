using UnityEngine;

public interface ICharacterTurnDirection {
    Vector2 SavedDirection { get; set; }
    void TurnToNextDirection(PlayerTurnToNextDirectionEvent.Parameter characterTurnToNextDirectionParameter);
}
