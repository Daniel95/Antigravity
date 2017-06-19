using IoCPlus;
using UnityEngine;

public class CharacterSurroundingDirectionView : View, ICharacterSurroundingDirection {

    [Inject] private Ref<ICharacterCollisionDirection> characterCollisionDirection;
    [Inject(Label.Player)] private Ref<ICharacterRaycastDirection> playerRaycastDirectionRef;
    [Inject] private Ref<ICharacterSurroundingDirection> surroundingDetectionRef;

    private Vector2 collisionDirection;
    private Vector2 raycastDirectionMiddleDirection;
    private Vector2 raycastDirectionCornersDirection;

    public Vector2 GetSurroundingsDirection(bool countCollisionDirection = true, bool countRaycastMiddleDirection = true, bool countRaycastCornerDirection = true) {
        Vector2 surroundingsDirection = new Vector2();

        if (GetUpdatedCollisionDirection().x != 0) {
            surroundingsDirection.x = collisionDirection.x;
        } else if (GetUpdatedRaycastDirectionMiddleDirection().x != 0) {
            surroundingsDirection.x = raycastDirectionMiddleDirection.x;
        } else if(GetUpdatedRaycastDirectionCornersDirection().x != 0) {
            surroundingsDirection.x = raycastDirectionCornersDirection.x;
        }

        if (GetUpdatedCollisionDirection().y != 0) {
            surroundingsDirection.y = collisionDirection.y;
        } else if (GetUpdatedRaycastDirectionMiddleDirection().y != 0) {
            surroundingsDirection.y = raycastDirectionMiddleDirection.y;
        } else if (GetUpdatedRaycastDirectionCornersDirection().y != 0) {
            surroundingsDirection.y = raycastDirectionCornersDirection.y;
        }

        return surroundingsDirection;
    }

    private Vector2 GetUpdatedCollisionDirection() {
        return collisionDirection = characterCollisionDirection.Get().GetCurrentCollisionDirection();
    }

    private Vector2 GetUpdatedRaycastDirectionMiddleDirection() {
        return raycastDirectionMiddleDirection = playerRaycastDirectionRef.Get().CenterRaycastDirection();
    }

    private Vector2 GetUpdatedRaycastDirectionCornersDirection() {
        return raycastDirectionCornersDirection = playerRaycastDirectionRef.Get().GetCornersDirection();
    }
}
