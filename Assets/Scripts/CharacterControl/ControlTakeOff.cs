using UnityEngine;
using System;
using System.Collections;

public class ControlTakeOff : MonoBehaviour, ITriggerer {

    private PlayerScriptAccess plrAcces;

    public Action tookOff;

    private bool inBouncyTrigger;

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action activateTrigger { get; set; }
    public Action stopTrigger { get; set; }

    // Use this for initialization
    void Start () {
        plrAcces = GetComponent<PlayerScriptAccess>();
    }

    public void Jump()
    {
        if (stopTrigger != null) {
            stopTrigger();
        }

        plrAcces.controlSpeed.TempSpeedIncrease();

        Vector2 collisionDir = plrAcces.collisionDirection.GetCurrentCollDir();

        print("collisiondir: " + collisionDir);

        //check if we have raycast collision on only one axis, jumping wont work when we are in a corner
        if (collisionDir.x == 0 || collisionDir.y == 0)
        {
            Vector2 newDir = plrAcces.controlVelocity.GetDirection();

            //check the raycastdir, our newDir is the opposite of one of the axes
            if (collisionDir.x != 0)
            {
                newDir.x = collisionDir.x * -1;
            }
            else if(collisionDir.y != 0)
            {
                newDir.y = collisionDir.y * -1;
            }

            plrAcces.controlVelocity.SetDirection(plrAcces.controlVelocity.AdjustDirToMultiplier(newDir));

            if (tookOff != null)
                tookOff();
        }
    }

    public void Bounce(Vector2 _currentDir, Vector2 _collisionDir) {
        plrAcces.controlSpeed.TempSpeedIncrease();

        if (_collisionDir.x != 0 || _collisionDir.y != 0)
        {
            //check the raycastdir, our newDir is the opposite of one of the axes
            if (_collisionDir.x != 0)
            {
                _currentDir.x *= -1;
            }
            if (_collisionDir.y != 0)
            {
                _currentDir.y *= -1;
            }

            plrAcces.controlVelocity.SetDirection(_currentDir);

            if (tookOff != null)
                tookOff();
        }
    }

    public bool CheckToBounce(Collision2D collision)
    {
        return collision.collider.CompareTag(Tags.Bouncy) || inBouncyTrigger;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!inBouncyTrigger && collision.CompareTag(Tags.Bouncy))
        {
            //if we hit a trigger we say that we are currently in a bouncy trigger, in case we hit a non trigger collider
            inBouncyTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (inBouncyTrigger && collision.transform.CompareTag(Tags.Bouncy))
        {
            inBouncyTrigger = false;
        }
    }
}
