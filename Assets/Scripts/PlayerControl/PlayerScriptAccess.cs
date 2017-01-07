using UnityEngine;
using System.Collections;

//contains some general character scripts that are commonly used by other scripts
public class PlayerScriptAccess : MonoBehaviour {

    [HideInInspector]
    public CharRaycasting charRaycasting;
    [HideInInspector]
    public ControlVelocity controlVelocity;
    [HideInInspector]
    public ControlDirection controlDirection;
    [HideInInspector]
    public ControlSpeed controlSpeed;
    [HideInInspector]
    public SwitchGravity switchGravity;
    [HideInInspector]
    public SpeedMultiplier speedMultiplier;
    [HideInInspector]
    public PlayerInputs playerInputs;
    [HideInInspector]
    public TriggerCollisions triggerCollisions;


    void Awake() {
        charRaycasting = GetComponent<CharRaycasting>();
        controlVelocity = GetComponent<ControlVelocity>();
        controlDirection = GetComponent<ControlDirection>();
        controlSpeed = GetComponent<ControlSpeed>();
        switchGravity = GetComponent<SwitchGravity>();
        speedMultiplier = GetComponent<SpeedMultiplier>();
        playerInputs = GetComponent<PlayerInputs>();
        triggerCollisions = GetComponent<TriggerCollisions>();
    }
}
