using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootControlsTrigger : MonoBehaviour, ITriggerable
{
    public bool triggered { get; set; }

    [SerializeField]
    private GameObject player;

    private PlayerInputs playerInputs;

    private PlayerStarter playerStarter;

    private void Start()
    {
        playerInputs = player.GetComponent<PlayerInputs>();
        playerStarter = player.GetComponent<PlayerStarter>();
    }

    public void TriggerActivate()
    {
        playerInputs.StartShootInputs();
        playerStarter.StartPlayerShootInputs();
    }

    public void TriggerStop()
    {
        playerInputs.StopShootInputs();
        playerStarter.StartPlayerShootInputs();
    }
}
