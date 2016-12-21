using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStarter : MonoBehaviour {

    private PlayerScriptAccess plrAcces;

    private void Awake()
    {
        plrAcces = GetComponent<PlayerScriptAccess>();
    }

    public void StartPlayerMovement()
    {
        plrAcces.controlVelocity.StartDirectionalMovement();
    }

    public void StartPlayerControls()
    {
        plrAcces.playerInputs.StartInputs();
    } 
}
