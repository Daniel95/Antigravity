using UnityEngine;
using System.Collections;

//contains some general character scripts that are commonly used by other scripts
public class PlayerScriptAccess : MonoBehaviour {

    [HideInInspector]
    public ControlVelocity controlVelocity;
    [HideInInspector]
    public ControlDirection controlDirection;
    [HideInInspector]
    public ControlSpeed controlSpeed;
    [HideInInspector]
    public ControlTakeOff controlTakeOff;
    [HideInInspector]
    public SpeedMultiplier speedMultiplier;
    [HideInInspector]
    public PlayerInputs playerInputs;
    [HideInInspector]
    public CollisionDirection collisionDirection;

    void Awake() {
        controlVelocity = GetComponent<ControlVelocity>();
        controlDirection = GetComponent<ControlDirection>();
        controlSpeed = GetComponent<ControlSpeed>();
        controlTakeOff = GetComponent<ControlTakeOff>();
        speedMultiplier = GetComponent<SpeedMultiplier>();
        playerInputs = GetComponent<PlayerInputs>();
        collisionDirection = GetComponent<CollisionDirection>();
    }
}
