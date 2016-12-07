using UnityEngine;
using System.Collections;

public class PCInputs : MonoBehaviour {

    [SerializeField]
    private KeyCode jumpInput = KeyCode.Space;

    [SerializeField]
    private KeyCode shootInput = KeyCode.Mouse0;

    public bool JumpInput() {
        return Input.GetKeyDown(jumpInput);
    }

    public Vector2 ShootInput()
    {
        if (Input.GetKeyDown(shootInput) && !InputDetect.CheckUICollision(Input.mousePosition))
        {
            return (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        }
        else return Vector2.zero;
    }
}
