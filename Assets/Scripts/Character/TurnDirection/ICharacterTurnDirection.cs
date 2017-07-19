using UnityEngine;

public interface ICharacterTurnDirection {

    Vector2 SavedDirection { get; set; }
    void TurnToNextDirection(Vector2 moveDirection, Vector2 surroundingsDirection);

}
