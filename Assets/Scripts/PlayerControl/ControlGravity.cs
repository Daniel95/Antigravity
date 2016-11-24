using UnityEngine;
using System.Collections;

public class ControlGravity : MonoBehaviour {
    //allows other scripts to easily control the gravity of the character.

    private Rigidbody2D rb;
    private ControlVelocity velocity;

    private float standardGravity;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        standardGravity = rb.gravityScale;
	}

    public void SwitchGravity() {
        rb.gravityScale *= -1;
    }

    public void SetGravity(float _newGravity) {
        rb.gravityScale = _newGravity;
    }

    public void ResetGravity() {
        rb.gravityScale = standardGravity;
    }
}
