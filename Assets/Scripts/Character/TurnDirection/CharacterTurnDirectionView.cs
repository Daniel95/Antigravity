using IoCPlus;
using System;
using UnityEngine;

public class CharacterTurnDirectionView : View, ICharacterTurnDirection {

    public Vector2 SavedDirection { get { return savedDirection; } set { savedDirection = value; } }

    public Action<Vector2> FinishedDirectionLogic;

    [Inject] private Ref<ICharacterTurnDirection> characterMoveDirectionRef;

    [Inject] private CharacterSetMoveDirectionEvent characterSetMoveDirectionEvent;
    [Inject] private CharacterTemporarySpeedChangeEvent characterTemporarySpeedChangeEvent;
    [Inject] private CharacterTemporarySpeedDecreaseEvent characterTemporarySpeedDecreaseEvent;

    [SerializeField] private float directionSpeedNeutralValue = 0.4f;
    [SerializeField] private float maxSpeedChange = 0.7f;

    private Vector2 savedDirection;

    public override void Initialize() {
        characterMoveDirectionRef.Set(this);
    }

    /// <summary>
    /// apply the next logic direction (game logic), to our controlVelocity script.
    /// </summary>
    /// <param name="currentDir"></param>
    /// <param name="collDir"></param>
    public void TurnToNextDirection(CharacterTurnToNextDirectionEvent.Parameter characterTurnToNextDirectionParameter) {
        //our next direction we are going to move towards, depending on our currentdirection, and the direction of our collision(s)
        Vector2 nextDirection = CalculateDirection(characterTurnToNextDirectionParameter);

        Vector2 nextLookDirection = savedDirection;

        //Debug.Log("____");
        //Debug.Log("move dir: " + characterTurnToNextDirectionParameter.MoveDirection);
        //Debug.Log("coll dir: " + characterTurnToNextDirectionParameter.CollisionDirection);
        //Debug.Log("corner dir: " + characterTurnToNextDirectionParameter.CornerDirection);

        //Debug.Log("saved dir: " + savedDirection);
        //Debug.Log("next dir: " + nextDirection);

        //use the direction logic for our new dir, but invert it if our speed multiplier is also inverted
        characterSetMoveDirectionEvent.Dispatch(nextDirection);

        if (FinishedDirectionLogic != null) {
            FinishedDirectionLogic(nextLookDirection);
        }
    }

    void Start() {
        if (savedDirection.x == 0) {
            savedDirection.x = 1;
        }
        if (savedDirection.y == 0) {
            savedDirection.y = 1;
        }

        //wait one frame so all scripts are loaded, then send out a delegate with the direction, used by FutureDirectionIndicator
        GetComponent<Frames>().ExecuteAfterDelay(1, () => {
            if (FinishedDirectionLogic != null) {
                FinishedDirectionLogic(savedDirection);
            }
        });
    }

    /// <summary>
    /// the logic we use to control the players direction using collision directions and raycasts collisions, after we have collision with another object
    /// </summary>
    /// <param name="currentDirection"></param>
    /// <param name="collisionDirection"></param>
    /// <returns></returns>
    private Vector2 CalculateDirection(CharacterTurnToNextDirectionEvent.Parameter characterTurnToNextDirectionParameter) {     
        Vector2 newDirection;

        Vector2 cornerDirection = characterTurnToNextDirectionParameter.CornerDirection;
        Vector2 collisionDirection = characterTurnToNextDirectionParameter.CollisionDirection;
        Vector2 moveDirection = characterTurnToNextDirectionParameter.MoveDirection;

        //if we are not hitting a wall on both axis or are not moving in an angle
        if (cornerDirection == Vector2.zero) {
            if (collisionDirection.x == 0 || collisionDirection.y == 0) {

                //save our direction the axis is not zero
                if (moveDirection.x != 0) {
                    savedDirection.x = Rounding.InvertOnNegativeCeil(moveDirection.x);
                }
                if (moveDirection.y != 0) {
                    savedDirection.y = Rounding.InvertOnNegativeCeil(moveDirection.y);
                }

                //change speed by calculating the angle
                float speedChange = Vector2.Angle(moveDirection, collisionDirection) / 90;

                if (speedChange > maxSpeedChange) {
                    speedChange = maxSpeedChange;
                }

                characterTemporarySpeedChangeEvent.Dispatch(new CharacterTemporarySpeedChangeEvent.Parameter(speedChange, directionSpeedNeutralValue));

                //replace the dir on the axis that we dont have a collision with
                //example: if we hit something under us, move to the left or right, depeding on our lastDir
                newDirection = collisionDirection.x != 0 ? new Vector2(0, savedDirection.y) : new Vector2(savedDirection.x, 0);
            } else {
                newDirection = savedDirection = moveDirection * -1;
            }

        } else {
            characterTemporarySpeedDecreaseEvent.Dispatch();

            if (moveDirection.x == cornerDirection.x) {
                savedDirection.y = cornerDirection.y * -1;
                savedDirection.x = cornerDirection.x;
                newDirection = new Vector2(0, savedDirection.y);
            }
            else {
                savedDirection.x = cornerDirection.x * -1;
                savedDirection.y = cornerDirection.y;
                newDirection = new Vector2(savedDirection.x, 0);
            }
        }

        return newDirection;
    }
}
