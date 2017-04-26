using UnityEngine;

public interface ICharacterTurnDirection {
    Vector2 SavedDirection { set; }
    void TurnToNextDirection(CharacterTurnToNextDirectionParameter characterTurnToNextDirectionParameter);
}
