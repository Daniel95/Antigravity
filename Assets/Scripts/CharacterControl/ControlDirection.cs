﻿using UnityEngine;
using System;
using System.Collections;

public class ControlDirection : MonoBehaviour {

    private PlayerScriptAccess plrAccess;

    //the last directions before it was set to zero
    private Vector2 lastDir;

    public Action<Vector2> finishedDirectionLogic;

    private Coroutine waitRigidbodyCorrectionFrames;

    void Start() {
        plrAccess = GetComponent<PlayerScriptAccess>();

        lastDir = plrAccess.controlVelocity.GetDirection();

        if (lastDir.x == 0)
            lastDir.x = 1;
        if (lastDir.y == 0)
            lastDir.y = 1;

        //wait one frame so all scripts are loaded, then send out a delegate with the direction, used by FutureDirectionIndicator
        GetComponent<Frames>().ExecuteAfterDelay(1, () =>
        {
            if (finishedDirectionLogic != null)
            {
                finishedDirectionLogic(lastDir);
            }
        });
    }

    public void ApplyLogicDirection(Vector2 _currentDir, Vector2 _collDir)
    {
        //our next direction we are going to move towards, depending on our currentdirection, and the direction of our collision(s)
        Vector2 dirLogic = DirectionLogic(_currentDir, _collDir);

        Vector2 lookDir = plrAccess.controlVelocity.AdjustDirToMultiplier(lastDir);

        //print("adjusted dirlogic: " + plrAccess.controlVelocity.AdjustDirToMultiplier(dirLogic));

        //use the direction logic for our new dir, but invert it if our speed multiplier is also inverted
        plrAccess.controlVelocity.SetDirection(plrAccess.controlVelocity.AdjustDirToMultiplier(dirLogic));

        if (finishedDirectionLogic != null)
        {
            finishedDirectionLogic(lookDir);
        }
    }

    //the logic we use to control the players direction using raycasts after we had collision with another object
    private Vector2 DirectionLogic(Vector2 _currentDir, Vector2 _collDir)
    {     
        Vector2 newDir = new Vector2();

        //if we are not hitting a wall on both axis
        if (_collDir.x == 0 || _collDir.y == 0) {


            //if we are moving standard, it means we are going in a straight line
            if(plrAccess.controlVelocity.CheckMovingStandard() && (_currentDir.x == 0 || _currentDir.y == 0))
            {

                //we dont want to overwrite our last dir with a zero, we use it determine which direction we should move next
                //if our currentDir.x isn't 0, set is as our lastDir.x
                if (_currentDir.x != 0)
                {
                    lastDir.x = Rounding.InvertOnNegativeCeil(_currentDir.x) * plrAccess.controlVelocity.GetMultiplierDir();
                };

                //if our currentDir.y isn't 0, set is as our lastDir.y
                if (_currentDir.y != 0)
                {
                    lastDir.y = Rounding.InvertOnNegativeCeil(_currentDir.y) * plrAccess.controlVelocity.GetMultiplierDir();
                }
            }
            else { //we are hitting a platform from an angle, use the angle to calculate which way we go next

                if (_currentDir.x != 0)
                {
                    lastDir.x = Rounding.InvertOnNegativeCeil(_currentDir.x) * plrAccess.controlVelocity.GetMultiplierDir();
                }

                //if our currentDir.y isn't 0, set is as our lastDir.y
                if (_currentDir.y != 0)
                {
                    lastDir.y = Rounding.InvertOnNegativeCeil(_currentDir.y) * plrAccess.controlVelocity.GetMultiplierDir();
                }

                plrAccess.controlSpeed.TempSpeedIncrease();
            }

            //replace the dir on the axis that we dont have a collision with
            //example: if we hit something under us, move to the left or right, depeding on our lastDir
            if (_collDir.x != 0)
            {
                newDir = new Vector2(0, lastDir.y);
            }
            else {
                newDir = new Vector2(lastDir.x, 0);
            }

        }
        else //here we know we are hitting more than one wall, or no wall at all
        {
            if (_collDir == Vector2.zero)
            {
                plrAccess.controlVelocity.SetDirection(plrAccess.controlVelocity.AdjustDirToMultiplier(lastDir));
            }
            else
            {

                //if the direction is zero on the x, we know we hit a wall on the Y axis
                if (_currentDir.x != 0)
                {
                    lastDir.y = _collDir.y * -1;
                    newDir = new Vector2(0, lastDir.y);
                }
                else
                {
                    lastDir.x = _collDir.x * -1;
                    newDir = new Vector2(lastDir.x, 0);
                }
            }
        }

        return newDir;
    }
}
