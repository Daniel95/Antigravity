using UnityEngine;
using System;
using System.Collections;

public class SwitchGravity : MonoBehaviour {

    [SerializeField]
    private float gravitateSpeedIncrement = 0.01f;

    [SerializeField]
    private float gravitateSpeedMax = 5;

    private ControlVelocity velocity;

    private CharRaycasting raycasting;

    private bool gravitating;

	// Use this for initialization
	void Start () {
        velocity = GetComponent<ControlVelocity>();
        raycasting = GetComponent<CharRaycasting>();
    }

    public void StartGravitating() {
        Vector2 raycastResults = new Vector2(raycasting.CheckHorizontalDir(), raycasting.CheckVerticalDir());

        Vector2 newDir = new Vector2();

        //check if we have raycast collision on only one axis, gravity wont work when we are on a corner
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

            velocity.SetDirection(newDir + velocity.GetDirection);
            velocity.StartIncrementingSpeed(gravitateSpeedIncrement, gravitateSpeedMax);

            gravitating = true;
        }
    }

    public void StopGravitating()
    {
        velocity.StopIncrementingSpeed();
        gravitating = false;
    }

    void OnCollisionEnter2D(Collision2D _coll) {
        if (gravitating) {
            velocity.StopIncrementingSpeed();
        }
    }
}
