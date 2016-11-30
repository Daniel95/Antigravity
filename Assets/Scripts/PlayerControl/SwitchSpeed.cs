using UnityEngine;
using System.Collections;

public class SwitchSpeed : MonoBehaviour {

    [SerializeField]
    private KeyCode speedNegative = KeyCode.A;

    [SerializeField]
    private KeyCode speedPostive = KeyCode.D;

    [SerializeField]
    private float startSpeed = 1;

    [SerializeField]
    private float changeSpeed = 0.01f;

    [SerializeField]
    private float maxSpeed = 2f;

    [SerializeField]
    private float minSwitchSpeedThreshold = 0.5f;

    private ControlVelocity velocity;

    void Start() {
        velocity = GetComponent<ControlVelocity>();
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKey(speedNegative))
        {
            ChangeSpeed(changeSpeed * Time.deltaTime);
        }
        else if(Input.GetKey(speedPostive)) {
            ChangeSpeed(-changeSpeed * Time.deltaTime);
        }
	}

    private void ChangeSpeed(float _change) {
        print("change: " + _change);
        if (Mathf.Abs(velocity.CurrentSpeed) > minSwitchSpeedThreshold)
        {
            if(velocity.CurrentSpeed < changeSpeed && velocity.CurrentSpeed > -changeSpeed)
                velocity.SetSpeedMultiplier(velocity.CurrentSpeed + _change);
        }
        else {
            velocity.SetSpeedMultiplier(velocity.CurrentSpeed * -1);
        }
        print("new speed: " + velocity.CurrentSpeed);
    }
}
