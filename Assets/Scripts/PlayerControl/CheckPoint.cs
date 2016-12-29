using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private RevivedState revivedState;

    private StateMachine stateMachine;

    private Vector2 lastCheckpointLocation;

    private bool checkpointReached;

    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        revivedState = GetComponent<RevivedState>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger && collision.transform.CompareTag(Tags.CheckPoint)) {
            checkpointReached = true;

            lastCheckpointLocation = collision.transform.position;

            ActivateRevivedState();
            revivedState.IsInPosition = false;
            revivedState.StartMovingToCenterCheckPoint(lastCheckpointLocation);
        }
    }

    public void Revive()
    {
        ActivateRevivedState();

        transform.position = lastCheckpointLocation;
    }

    //put the player in the revived state
    private void ActivateRevivedState()
    {
        stateMachine.DeactivateAllStates();
        stateMachine.ActivateState(StateID.RevivedState);
    }

    public bool CheckPointReached
    {
        get { return checkpointReached; }
    }
}
