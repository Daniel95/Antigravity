using UnityEngine;

public interface ICharacterTurnDirection {
    Vector2 SavedDirection { get; set; }
    void TurnToNextDirection(CharacterTurnToNextDirectionEvent.Parameter characterTurnToNextDirectionParameter);
}
