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

    public void TurnToNextDirection(Vector2 moveDirection, Vector2 surroundingsDirection) {
        Vector2 nextDirection = CalculateDirection(moveDirection, surroundingsDirection);
        characterSetMoveDirectionEvent.Dispatch(gameObject, nextDirection);
    }

    /// <summary>
    /// the logic we use to control the players direction using collision directions and raycasts collisions, after we have collision with another object
    /// </summary>
    /// <param name="currentDirection"></param>
    /// <param name="collisionDirection"></param>
    /// <returns></returns>
    protected Vector2 CalculateDirection(Vector2 moveDirection, Vector2 surroundingsDirection) {     
        Vector2 newDirection;

        bool isInCorner = DirectionIsNotLinear(surroundingsDirection);

        if (!isInCorner) {
            if (moveDirection.x != 0) {
                savedDirection.x = Rounding.InvertOnNegativeCeil(moveDirection.x);
            }
            if (moveDirection.y != 0) {
                savedDirection.y = Rounding.InvertOnNegativeCeil(moveDirection.y);
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
            } else {
                savedDirection.x = surroundingsDirection.x * -1;
                savedDirection.y = surroundingsDirection.y;
                newDirection = new Vector2(savedDirection.x, 0);
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
