using IoCPlus;
using System;
using UnityEngine;

public class CharacterTurnDirectionView : View, ICharacterTurnDirection {

    public Vector2 SavedDirection { get { return savedDirection; } set { savedDirection = value; } }

    public Action<Vector2, Vector2> OnSurfaceTurn;
    public Action<Vector2, Vector2> OnCornerTurn;

    [Inject] private CharacterSetMoveDirectionEvent characterSetMoveDirectionEvent;

    [SerializeField] protected float DirectionSpeedNeutralValue = 0.4f;
    [SerializeField] protected float MaxSpeedChange = 0.7f;

    private Vector2 savedDirection;

    public void TurnToNextDirection(Vector2 moveDirection, Vector2 surroundingsDirection, Vector2 collisionDirection, Vector2 raycastHitDistance) {
        Vector2 nextDirection = CalculateDirection(moveDirection, surroundingsDirection, collisionDirection, raycastHitDistance);
        Debug.Log("collisionDirection " + collisionDirection);
        Debug.Log("surroundingsDirection " + surroundingsDirection);
        Debug.Log("nextDirection " + nextDirection);
        Debug.Log("____________________");
        characterSetMoveDirectionEvent.Dispatch(gameObject, nextDirection);
    }

    protected Vector2 CalculateDirection(Vector2 moveDirection, Vector2 surroundingsDirection, Vector2 collisionDirection, Vector2 raycastHitDistance) {     
        Vector2 newDirection = moveDirection;

        bool isInCorner = DirectionIsNotLinear(surroundingsDirection);

        if (!isInCorner) {
            if (moveDirection.x != 0) {
                savedDirection.x = moveDirection.x;
            }
            if (moveDirection.y != 0) {
                savedDirection.y = moveDirection.y;
            }

            if(surroundingsDirection.x != 0) {
                newDirection = new Vector2(0, savedDirection.y);
            } else {
                newDirection = new Vector2(savedDirection.x, 0);
            }

            if(OnSurfaceTurn != null) {
                OnSurfaceTurn(moveDirection, surroundingsDirection);
            }
        } else {
            if (moveDirection.x == surroundingsDirection.x) {
                savedDirection.y = surroundingsDirection.y * -1;
                savedDirection.x = surroundingsDirection.x;
                newDirection = new Vector2(0, savedDirection.y);
            } else if (moveDirection.y == surroundingsDirection.y) {
                savedDirection.x = surroundingsDirection.x * -1;
                savedDirection.y = surroundingsDirection.y;
                newDirection = new Vector2(savedDirection.x, 0);
            }

            if (surroundingsDirection.x != collisionDirection.x && surroundingsDirection.x == moveDirection.x) {
                float playerToCornerDistanceX = raycastHitDistance.x - (transform.localScale.x / 2);
                transform.position += new Vector3(surroundingsDirection.x * playerToCornerDistanceX, 0);
            } else if (surroundingsDirection.y != collisionDirection.x && surroundingsDirection.y == moveDirection.y) {
                float playerToCornerDistanceY = raycastHitDistance.y - (transform.localScale.y / 2);
                transform.position += new Vector3(0, surroundingsDirection.y * playerToCornerDistanceY);
            }

            if (OnCornerTurn != null) {
                OnCornerTurn(moveDirection, surroundingsDirection);
            }
        }

        return newDirection;
    }

    private static bool DirectionIsNotLinear(Vector2 direction) {
        return direction.x != 0 && direction.y != 0;
    }

}
