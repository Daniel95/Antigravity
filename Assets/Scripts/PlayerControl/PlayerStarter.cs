using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStarter : MonoBehaviour {

    private PlayerScriptAccess plrAcces;

    private void Awake()
    {
        plrAcces = GetComponent<PlayerScriptAccess>();
    }

    public void StartPlayer()
    {
        plrAcces.playerInputs.StartInputs();
        plrAcces.controlVelocity.StartDirectionalMovement();
    }
}
