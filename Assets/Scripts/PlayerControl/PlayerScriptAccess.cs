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
    public ChangeSpeedMultiplier changeSpeedMultiplier;

    void Awake() {
        charRaycasting = GetComponent<CharRaycasting>();
        controlVelocity = GetComponent<ControlVelocity>();
        controlDirection = GetComponent<ControlDirection>();
        switchGravity = GetComponent<SwitchGravity>();
        changeSpeedMultiplier = GetComponent<ChangeSpeedMultiplier>();
    }
}
