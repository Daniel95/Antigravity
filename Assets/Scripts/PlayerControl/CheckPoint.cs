using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    private StateMachine stateMachine;

    private Vector2 lastCheckpointLocation;

    private bool checkPointReached;

    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger && collision.transform.CompareTag(Tags.CheckPoint)) {
            checkPointReached = true;
            lastCheckpointLocation = collision.transform.position;
        }
    }

    public void Revive()
    {
        //put the player in the revived state
        stateMachine.DeactivateAllStates();
        stateMachine.ActivateState(StateID.RevivedState);

        transform.position = lastCheckpointLocation;
    }

    public bool CheckPointReached
    {
        get { return checkPointReached; }
    }
}
