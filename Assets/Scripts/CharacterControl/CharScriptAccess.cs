using UnityEngine;
using System.Collections;

//contains some general character scripts that are commonly used by other scripts
public class CharScriptAccess : MonoBehaviour {

    [HideInInspector]
    public ControlVelocity ControlVelocity;
    [HideInInspector]
    public ControlDirection ControlDirection;
    [HideInInspector]
    public ControlSpeed ControlSpeed;
    [HideInInspector]
    public ControlTakeOff ControlTakeOff;
    [HideInInspector]
    public SpeedMultiplier SpeedMultiplier;
    [HideInInspector]
    public CollisionDirection CollisionDirection;

    void Awake() {
        ControlVelocity = GetComponent<ControlVelocity>();
        ControlDirection = GetComponent<ControlDirection>();
        ControlSpeed = GetComponent<ControlSpeed>();
        ControlTakeOff = GetComponent<ControlTakeOff>();
        SpeedMultiplier = GetComponent<SpeedMultiplier>();
        CollisionDirection = GetComponent<CollisionDirection>();
    }
}
