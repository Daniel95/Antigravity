using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootControlsTrigger : MonoBehaviour, ITriggerable
{
    public bool triggered { get; set; }

    [SerializeField]
    private GameObject player;

    private PlayerStarter playerStarter;

    private void Start()
    {
        playerStarter = player.GetComponent<PlayerStarter>();
    }

    public void TriggerActivate()
    {
        playerStarter.SetPlayerShootInputs(true);
    }

    public void TriggerStop()
    {
        playerStarter.SetPlayerShootInputs(false);
    }
}
