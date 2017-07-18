using IoCPlus;
using UnityEngine;

public class CharacterTurnDirectionView : View, ICharacterTurnDirection {

    public Vector2 SavedDirection { get { return savedDirection; } set { savedDirection = value; } }

    [Inject] private PlayerSetMoveDirectionEvent characterSetMoveDirectionEvent;
    [Inject] private PlayerTemporarySpeedChangeEvent characterTemporarySpeedChangeEvent;
    [Inject] private PlayerTemporarySpeedDecreaseEvent characterTemporarySpeedDecreaseEvent;

    [SerializeField] private float directionSpeedNeutralValue = 0.4f;
    [SerializeField] private float maxSpeedChange = 0.7f;

    private Vector2 savedDirection;

    public void TurnToNextDirection(PlayerTurnToNextDirectionEvent.Parameter characterTurnToNextDirectionParameter) {
        Vector2 nextDirection = CalculateDirection(characterTurnToNextDirectionParameter.MoveDirection, characterTurnToNextDirectionParameter.SurroundingsDirection);
        characterSetMoveDirectionEvent.Dispatch(nextDirection);
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

            float speedChange = Vector2.Angle(moveDirection, surroundingsDirection) / 90;

            if (speedChange > maxSpeedChange) {
                speedChange = maxSpeedChange;
            }

            characterTemporarySpeedChangeEvent.Dispatch(new PlayerTemporarySpeedChangeEvent.Parameter(speedChange, directionSpeedNeutralValue));

            if(surroundingsDirection.x != 0) {
                newDirection = new Vector2(0, savedDirection.y);
            } else {
                newDirection = new Vector2(savedDirection.x, 0);
            }
        } else {
            characterTemporarySpeedDecreaseEvent.Dispatch();

            if (moveDirection.x == surroundingsDirection.x) {
                savedDirection.y = surroundingsDirection.y * -1;
                savedDirection.x = surroundingsDirection.x;
                newDirection = new Vector2(0, savedDirection.y);
            } else {
                savedDirection.x = surroundingsDirection.x * -1;
                savedDirection.y = surroundingsDirection.y;
                newDirection = new Vector2(savedDirection.x, 0);
            }
        }

        return newDirection;
    }

    private static bool DirectionIsNotLinear(Vector2 direction) {
        return direction.x != 0 && direction.y != 0;
    }

}
