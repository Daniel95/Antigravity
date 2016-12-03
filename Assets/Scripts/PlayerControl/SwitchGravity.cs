using UnityEngine;
using System;
using System.Collections;

public class SwitchGravity : MonoBehaviour {

    private ControlVelocity velocity;

    private CharRaycasting raycasting;

    public Action switchedGravity;

	// Use this for initialization
	void Start () {
        velocity = GetComponent<ControlVelocity>();
        raycasting = GetComponent<CharRaycasting>();
    }

    public void StartGravitating() {
        Vector2 raycastResults = new Vector2(raycasting.CheckHorizontalDir(), raycasting.CheckVerticalDir());

        Vector2 newDir = new Vector2();

        //check if we have raycast collision on only one axis, gravity wont work when we are in a corner
        if (raycastResults.x == 0 || raycastResults.y == 0)
        {
            //check the raycastdir, our newDir is the opposite of one of the axes
            if (raycastResults.x != 0)
            {
                newDir.x = raycastResults.x * -1;
            }
            else
            {
                newDir.y = raycastResults.y * -1;
            }

            velocity.SetDirection(velocity.AdjustDirToMultiplier(newDir + velocity.GetControlledDirection()));

            if (switchedGravity != null)
                switchedGravity();
        }
    }
}
