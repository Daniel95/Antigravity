﻿using UnityEngine;

public interface ICharacterMoveDirection {
    Vector2 SavedDirection { set; }
    void TurnToNextDirection(CharacterTurnToNextDirectionParameter characterTurnToNextDirectionParameter);
}
