using UnityEngine;
using System.Collections;

public class ControlDirection : MonoBehaviour {

    private CharRaycasting ray;
    private ControlVelocity controlVelocity;

    //the last directions before it was set to zero
    private Vector2 lastDir;

    void Start() {
        ray = GetComponent<CharRaycasting>();
        controlVelocity = GetComponent<ControlVelocity>();

        lastDir = controlVelocity.GetDirection;
    }

    public void CheckDirection(Vector2 _grappleDirection = default(Vector2)) {
        StartCoroutine(WaitForRigidBodyCorrection(_grappleDirection));
    }

    //we have to wait one frame, because the first frame of collision the object might be inside the other object, 
    //so we wait until the rigidbody has adjusted our position 
    IEnumerator WaitForRigidBodyCorrection(Vector2 _grappleDirection)
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        DirectionLogic(_grappleDirection);
    }

    public void DirectionLogic(Vector2 _grappleDirection) {

        //get the collision directions of the raycasts
        Vector2 rayDir = new Vector2(ray.CheckHorizontalDir(), ray.CheckVerticalDir());

        //if we have collision on only one axis, check the direction of our velocity to decide our direction
        if (rayDir.x != 0 && rayDir.y == 0) {
            //if we have velocity towards a direction (we could move at a platform from an angle), use that for the new direction
            if (_grappleDirection.y != 0)
            {
                if (_grappleDirection.y > 0)
                    lastDir.y = 1;
                else
                    lastDir.y = -1;

                controlVelocity.TempSpeedUp();
            }
            else {
                controlVelocity.TempSlowDown();
            }

            controlVelocity.TempSlowDown();
            controlVelocity.SetDirection(new Vector2(0, lastDir.y));
        }
        else if (rayDir.y != 0 && rayDir.x == 0)
        {
            //if we have velocity towards a direction (we could move at a platform from an angle), use that for the new direction
            if (_grappleDirection.x != 0)
            {
                if (_grappleDirection.x > 0)
                    lastDir.x = 1;
                else
                    lastDir.x = -1;

                controlVelocity.TempSpeedUp();
            }
            else {
                controlVelocity.TempSlowDown();
            }

            controlVelocity.SetDirection(new Vector2(lastDir.x, 0));
        }
        else //here we know we are hitting more than one wall, or no wall at all
        {
            if (rayDir == Vector2.zero)
            {
                controlVelocity.SetDirection(lastDir);
            }
            else
            {
                //if the velocity is zero on the x, we know we are wall 
                if (controlVelocity.GetDirection.x != 0)
                {
                    lastDir.y = rayDir.y * -1;
                    controlVelocity.SetDirection(new Vector2(0, lastDir.y));
                }
                else
                {
                    lastDir.x = rayDir.x * -1;
                    controlVelocity.SetDirection(new Vector2(lastDir.x, 0));
                }

                controlVelocity.TempSlowDown();
            }
        }
    }
}
