using UnityEngine;
using System.Collections;

public class MobileInputs : MonoBehaviour {

    public bool JumpInput()
    {
        return Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Moved && !InputDetect.CheckUICollision(Input.GetTouch(0).position);
    }

    public Vector2 ShootInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && Input.GetTouch(0).deltaPosition != Vector2.zero && !InputDetect.CheckUICollision(Input.GetTouch(0).position))
        {
            return Input.GetTouch(0).deltaPosition.normalized;
        }
        else return Vector2.zero;
    }
}
