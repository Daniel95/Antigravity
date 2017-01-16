using UnityEngine;
using System;
using System.Collections;

public class ControlDirection : MonoBehaviour {

    private CharScriptAccess _charAccess;

    //the last directions before it was set to zero
    private Vector2 _lastDir;

    public Action<Vector2> FinishedDirectionLogic;

    private Coroutine _waitRigidbodyCorrectionFrames;

    private CharRaycasting _charRaycasting;

    void Start() {
        _charAccess = GetComponent<CharScriptAccess>();
        _charRaycasting = GetComponent<CharRaycasting>();

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
    /// apply the next logic direction (game logic), to our controlVelocity script.
    /// </summary>
    /// <param name="currentDir"></param>
    /// <param name="collDir"></param>
    public void ApplyLogicDirection(Vector2 currentDir, Vector2 collDir)
    {
        //our next direction we are going to move towards, depending on our currentdirection, and the direction of our collision(s)
        Vector2 dirLogic = DirectionLogic(currentDir, collDir);

        Vector2 lookDir = _charAccess.ControlVelocity.AdjustDirToMultiplier(_lastDir);

        //use the direction logic for our new dir, but invert it if our speed multiplier is also inverted
        _charAccess.ControlVelocity.SetDirection(_charAccess.ControlVelocity.AdjustDirToMultiplier(dirLogic));

        if (FinishedDirectionLogic != null)
        {
            FinishedDirectionLogic(lookDir);
        }
    }

    /// <summary>
    /// the logic we use to control the players direction using collision directions and raycasts collisions, after we have collision with another object
    /// </summary>
    /// <param name="currentDir"></param>
    /// <param name="collDir"></param>
    /// <returns></returns>
    private Vector2 DirectionLogic(Vector2 currentDir, Vector2 collDir)
    {     
        Vector2 newDir = new Vector2();

        //use raycasting for to detect if we are in a corner.
        //when colliding the rigidbody position correction can overshoot which means we exit collision, even thought it looks like we are still colliding
        Vector2 rayHitDir = new Vector2(_charRaycasting.CheckHorizontalMiddleDir(), _charRaycasting.CheckVerticalMiddleDir());

        //if we are not hitting a wall on both axis or are not moving in an angle
        if ((rayHitDir.x == 0 || rayHitDir.y == 0)) {

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
                    _lastDir.x = Rounding.InvertOnNegativeCeil(currentDir.x) * _charAccess.ControlVelocity.GetMultiplierDir();
                }

                //if our currentDir.y isn't 0, set is as our lastDir.y
                if (currentDir.y != 0)
                {
                    _lastDir.y = Rounding.InvertOnNegativeCeil(currentDir.y) * _charAccess.ControlVelocity.GetMultiplierDir();
                }

                if (!_charAccess.ControlVelocity.CheckMovingStandard() || (currentDir.x != 0 && currentDir.y != 0))
                {
                    _charAccess.ControlSpeed.TempSpeedIncrease();
                }

                //replace the dir on the axis that we dont have a collision with
                //example: if we hit something under us, move to the left or right, depeding on our lastDir
                newDir = collDir.x != 0 ? new Vector2(0, _lastDir.y) : new Vector2(_lastDir.x, 0);
            }

        }
        else //here we know we are hitting more than one wall, or no wall at all
        {

            if (rayHitDir == Vector2.zero)
            {
                _charAccess.ControlVelocity.SetDirection(_charAccess.ControlVelocity.AdjustDirToMultiplier(_lastDir));
            }
            else
            {

                if ((currentDir.x * _charAccess.ControlVelocity.GetMultiplierDir()) == rayHitDir.x)
                {
                    _lastDir.y = rayHitDir.y * -1;
                    _lastDir.x = rayHitDir.x;
                    newDir = new Vector2(0, _lastDir.y);
                }
                else
                {
                    _lastDir.x = rayHitDir.x * -1;
                    _lastDir.y = rayHitDir.y;
                    newDir = new Vector2(_lastDir.x, 0);
                }
            }
        }

        return newDir;
    }
}
