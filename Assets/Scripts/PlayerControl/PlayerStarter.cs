using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStarter : MonoBehaviour {

    private PlayerScriptAccess plrAcces;

    private void Awake()
    {
        plrAcces = GetComponent<PlayerScriptAccess>();
    }

    public void StartStandardPlayerInputs()
    {
        plrAcces.controlVelocity.StartDirectionalMovement();
        plrAcces.playerInputs.StartShootInputs();
        StartPlayerKeyInputs();
    }

    public void StartPlayerMovement()
    {
        plrAcces.controlVelocity.StartDirectionalMovement();
    }

    public void StartPlayerShootInputs()
    {
        plrAcces.playerInputs.StartShootInputs();
    } 

    public void StartPlayerKeyInputs()
    {
        plrAcces.playerInputs.StartKeyInputs();
    }
}
