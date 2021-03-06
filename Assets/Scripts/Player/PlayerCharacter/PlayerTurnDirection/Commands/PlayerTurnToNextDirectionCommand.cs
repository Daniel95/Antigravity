﻿using IoCPlus;
using UnityEngine;

public class PlayerTurnToNextDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterTurnDirection> playerMoveDirectionRef;
    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;
    [Inject(Label.Player)] private Ref<ICharacterRaycastDirection> playerRaycastDirectionRef;
    [Inject(Label.Player)] private Ref<ICharacterCollisionDirection> playerCollisionDirectionRef;

    protected override void Execute() {
        Vector2 moveDirection;
        if (playerVelocityRef.Get().IsMovingStandard) {
            moveDirection = playerVelocityRef.Get().MoveDirection;
        } else {
            moveDirection = playerVelocityRef.Get().GetPreviousVelocityDirection();
        }

        Vector2 collisionDirection = playerCollisionDirectionRef.Get().CollisionDirection;
        RaycastData combinedRaycastData = playerRaycastDirectionRef.Get().GetCombinedDirectionAndCenterDistances();

        Vector2 surroundingsDirection = SurroundingDirectionHelper.GetSurroundingsDirection(collisionDirection, combinedRaycastData.Direction);

        playerMoveDirectionRef.Get().TurnToNextDirection(moveDirection, surroundingsDirection, collisionDirection, combinedRaycastData.Distance);
    }
}
