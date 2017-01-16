using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootControlsTrigger : MonoBehaviour, ITriggerable
{
    public bool Triggered { get; set; }

    [SerializeField]
    private GameObject player;

    private PlayerStarter _playerStarter;

    private void Start()
    {
        _playerStarter = player.GetComponent<PlayerStarter>();
    }

    public void TriggerActivate()
    {
        _playerStarter.SetPlayerShootInputs(true);
    }

    public void TriggerStop()
    {
        _playerStarter.SetPlayerShootInputs(false);
    }
}
