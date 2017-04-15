using UnityEngine;
using System;
using System.Collections;
using IoCPlus;

public class CharacterMoveDirectionView : View, ICharacterMoveDirection {

    [Inject] private Ref<ICharacterMoveDirection> characterMoveDirectionRef;

    [SerializeField]
    private float directionSpeedNeutralValue = 0.4f;

    [SerializeField]
    private float maxSpeedChange = 0.7f;

    private CharScriptAccess _charAccess;

    //the last directions before it was set to zero
    private Vector2 _lastDir;

    public Action<Vector2> FinishedDirectionLogic;

    private CharacterRaycasting _charRaycasting;

    public override void Initialize() {
        characterMoveDirectionRef.Set(this);
    }

    /// <summary>
    /// apply the next logic direction (game logic), to our controlVelocity script.
    /// </summary>
    /// <param name="currentDir"></param>
    /// <param name="collDir"></param>
    public void TurnToNextDirection(DirectionParameter directionInfo) {
        //our next direction we are going to move towards, depending on our currentdirection, and the direction of our collision(s)
        Vector2 dirLogic = DirectionLogic(directionInfo.MoveDirection, directionInfo.CollisionDirection);

        Vector2 lookDir = _lastDir;

        //use the direction logic for our new dir, but invert it if our speed multiplier is also inverted
        _charAccess.ControlVelocity.SetDirection(dirLogic);

        if (FinishedDirectionLogic != null) {
            FinishedDirectionLogic(lookDir);
        }
    }

    void Start() {
        _charAccess = GetComponent<CharScriptAccess>();
        _charRaycasting = GetComponent<CharacterRaycasting>();

        _lastDir = _charAccess.ControlVelocity.GetDirection();

        if (_lastDir.x == 0)
            _lastDir.x = 1;
        if (_lastDir.y == 0)
            _lastDir.y = 1;

        //wait one frame so all scripts are loaded, then send out a delegate with the direction, used by FutureDirectionIndicator
        GetComponent<Frames>().ExecuteAfterDelay(1, () =>
        {
            if (FinishedDirectionLogic != null)
            {
                FinishedDirectionLogic(_lastDir);
            }
        });
    }




    /// <summary>
    /// the logic we use to control the players direction using collision directions and raycasts collisions, after we have collision with another object
    /// </summary>
    /// <param name="currentDir"></param>
    /// <param name="collDir"></param>
    /// <returns></returns>
    private Vector2 DirectionLogic(Vector2 currentDir, Vector2 collDir)
    {     
        Vector2 newDir;

        //use raycasting & collisionDir to to detect if we are in a corner.
        //when colliding the rigidbody position correction can overshoot which means we exit collision, even thought it looks like we are still colliding
        Vector2 cornersDir = GetCornersDir(collDir);

        //if we are not hitting a wall on both axis or are not moving in an angle
        if (cornersDir == Vector2.zero) {

            //if we are not in a corner, but still touch objects from both axises, invert our dir
            if (collDir.x != 0 && collDir.y != 0)
            {
                newDir = _lastDir = currentDir * -1;
            }
            else
            {

                //we dont want to overwrite our last dir with a zero, we use it determine which direction we should move next
                //if our currentDir.x isn't 0, set is as our lastDir.x
                if (currentDir.x != 0)
                {
                    _lastDir.x = Rounding.InvertOnNegativeCeil(currentDir.x);
                }

                //if our currentDir.y isn't 0, set is as our lastDir.y
                if (currentDir.y != 0)
                {
                    _lastDir.y = Rounding.InvertOnNegativeCeil(currentDir.y);
                }


                //change speed by calculating the angle
                float speedChange = Vector2.Angle(currentDir, collDir) / 90;

                if (speedChange > maxSpeedChange)
                    speedChange = maxSpeedChange;

                _charAccess.ControlSpeed.TempSpeedChange(speedChange, directionSpeedNeutralValue);

                //replace the dir on the axis that we dont have a collision with
                //example: if we hit something under us, move to the left or right, depeding on our lastDir
                newDir = collDir.x != 0 ? new Vector2(0, _lastDir.y) : new Vector2(_lastDir.x, 0);
            }

        }
        else //here we know we are hitting more than one wall, or no wall at all
        {
            _charAccess.ControlSpeed.SpeedDecrease();

            if (currentDir.x == cornersDir.x)
            {
                _lastDir.y = cornersDir.y * -1;
                _lastDir.x = cornersDir.x;
                newDir = new Vector2(0, _lastDir.y);
            }
            else
            {
                _lastDir.x = cornersDir.x * -1;
                _lastDir.y = cornersDir.y;
                newDir = new Vector2(_lastDir.x, 0);
            }
        }

        return newDir;
    }

    /// <summary>
    /// Check collDir & middleRayDir & cornerRayDir if we are in a corner.
    /// If one indicates that that we are in a corner return it, else return V.zero.
    /// </summary>
    /// <param name="collDir"></param>
    /// <returns></returns>
    private Vector2 GetCornersDir(Vector2 collDir)
    {
        if (CheckIsCorner(collDir))
        {
            return collDir;
        }

        Vector2 middleRayHitDir = new Vector2(_charRaycasting.CheckHorizontalMiddleDir(), _charRaycasting.CheckVerticalMiddleDir());

        if (CheckIsCorner(middleRayHitDir))
        {
            return middleRayHitDir;
        }

        return Vector2.zero;
    }

    private static bool CheckIsCorner(Vector2 dir)
    {
        return dir.x != 0 && dir.y != 0;
    }
}
