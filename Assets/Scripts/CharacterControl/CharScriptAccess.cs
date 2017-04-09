using UnityEngine;
using System.Collections;

//contains some general character scripts that are commonly used by other scripts
public class CharScriptAccess : MonoBehaviour {

    [HideInInspector]
    private ControlVelocityView ControlVelocity;
    [HideInInspector]
    private ControlDirection ControlDirection;
    [HideInInspector]
    private ControlSpeed ControlSpeed;
    [HideInInspector]
    private ControlTakeOff ControlTakeOff;
    [HideInInspector]
    private CollisionDirection CollisionDirection;

    void Awake() {
        ControlVelocity = GetComponent<ControlVelocityView>();
        ControlDirection = GetComponent<ControlDirection>();
        ControlSpeed = GetComponent<ControlSpeed>();
        ControlTakeOff = GetComponent<ControlTakeOff>();
        CollisionDirection = GetComponent<CollisionDirection>();
    }
}
