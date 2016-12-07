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
    public SwitchGravity switchGravity;
    [HideInInspector]
    public SpeedMultiplier speedMultiplier;
    [HideInInspector]
    public PlayerInputs playerInputs;

    void Awake() {
        charRaycasting = GetComponent<CharRaycasting>();
        controlVelocity = GetComponent<ControlVelocity>();
        controlDirection = GetComponent<ControlDirection>();
        switchGravity = GetComponent<SwitchGravity>();
        speedMultiplier = GetComponent<SpeedMultiplier>();
        playerInputs = GetComponent<PlayerInputs>();
    }
}
