using UnityEngine;

public interface ICharacterTurnDirection {

    Vector2 SavedDirection { get; set; }
    void TurnToNextDirection(Vector2 invertOnNegativeCeiledMoveDirection, Vector2 surroundingsDirection, Vector2 collisionDirection, Vector2 raycastHitDistance);

}
