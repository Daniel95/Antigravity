using UnityEngine;
using System;
using System.Collections;

public class ControlDirection : MonoBehaviour {

    private PlayerScriptAccess plrAcces;

    //the last directions before it was set to zero
    private Vector2 lastDir;

    public Action<Vector2> finishedDirectionLogic;

    void Start() {
        plrAcces = GetComponent<PlayerScriptAccess>();

        lastDir = plrAcces.controlVelocity.GetControlledDirection();
    }

    public void SetLogicDirection(Vector2 _currentDir) {
        StartCoroutine(WaitForRigidBodyCorrection(_currentDir));
    }

    //we have to wait two frames, because at this time the object might be inside the other object, 
    //so we wait until the rigidbody has adjusted our position 
    IEnumerator WaitForRigidBodyCorrection(Vector2 _currentDir)
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        AdjustDirection(_currentDir);
    }

    private void AdjustDirection(Vector2 _currentDir) {

        Vector2 dirLogic = DirectionLogic(_currentDir);

        Vector2 lookDir = plrAcces.controlVelocity.AdjustDirToMultiplier(lastDir);

        //use the direction logic for our new dir, but invert it if our speed multiplier is also inverted
        plrAcces.controlVelocity.SetDirection(plrAcces.controlVelocity.AdjustDirToMultiplier(dirLogic));

        if (finishedDirectionLogic != null)
        {
            finishedDirectionLogic(lookDir);
        }
    }

    //the logic we use to control the players direction using raycasts after we had collision with another object
    private Vector2 DirectionLogic(Vector2 _currentDir)
    {
        //get the collision directions of the raycasts
        Vector2 rayDir = new Vector2(plrAcces.charRaycasting.CheckHorizontalDir(), plrAcces.charRaycasting.CheckVerticalDir());
        
        Vector2 newDir = new Vector2();

        //if we are not hitting a wall on both axis
        if (rayDir.x == 0 || rayDir.y == 0) {

            //if our velocity dir is zero on one of the axis, we know we are going in a straight line
            if (plrAcces.controlVelocity.GetVelocity.x == 0 || plrAcces.controlVelocity.GetVelocity.y == 0)
            {
                //we dont want to overwrite our last dir with a zero, we use it determine which direction we should move next
                //if our currentDir.x isn't 0, set is as our lastDir.x
                if (_currentDir.x != 0)
                {
                    lastDir.x = Rounding.InvertOnNegativeCeil(_currentDir.x);
                }

                //if our currentDir.y isn't 0, set is as our lastDir.y
                if (_currentDir.y != 0)
                {
                    lastDir.y = Rounding.InvertOnNegativeCeil(_currentDir.y);
                }
            }
            else { //we are hitting a platform from an angle, use the angle to calculate which way we go next
                Vector2 velocityDir = plrAcces.controlVelocity.GetVelocityDirection();

                if (velocityDir.x != 0)
                {
                    lastDir.x = Rounding.InvertOnNegativeCeil(velocityDir.x);
                }

                //if our currentDir.y isn't 0, set is as our lastDir.y
                if (velocityDir.y != 0)
                {
                    lastDir.y = Rounding.InvertOnNegativeCeil(velocityDir.y);
                }
            }

            //replace the dir on the axis that we dont have a collision with
            //example: if we hit something under us, move to the left or right, depeding on our lastDir
            if (rayDir.x != 0)
            {
                newDir = new Vector2(0, lastDir.y);
            }
            else {
                newDir = new Vector2(lastDir.x, 0);
            }
        }
        else //here we know we are hitting more than one wall, or no wall at all
        {
            if (rayDir == Vector2.zero)
            {
                plrAcces.controlVelocity.SetDirection(plrAcces.controlVelocity.AdjustDirToMultiplier(lastDir));
            }
            else
            {
                //if the direction is zero on the x, we know we hit a wall on the Y axis
                if (_currentDir.x != 0)
                {
                    lastDir.y = rayDir.y * -1;
                    newDir = new Vector2(0, lastDir.y);
                }
                else
                {
                    lastDir.x = rayDir.x * -1;
                    newDir = new Vector2(lastDir.x, 0);
                }
            }
        }

        return newDir;
    }
}
