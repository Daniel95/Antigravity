using UnityEngine;
using System.Collections;

//contains some general character scripts that are commonly used by other scripts
public class CharScriptAccess : MonoBehaviour {

    [HideInInspector]
    private CharacterVelocityView ControlVelocity;
    [HideInInspector]
    private CharacterSpeedView ControlSpeed;
    [HideInInspector]
    private CharacterJumpView ControlTakeOff;

    void Awake() {
        ControlVelocity = GetComponent<CharacterVelocityView>();
        ControlSpeed = GetComponent<CharacterSpeedView>();
        ControlTakeOff = GetComponent<CharacterJumpView>();
        CollisionDirection = GetComponent<CollisionDirectionDetection>();
    }
}
