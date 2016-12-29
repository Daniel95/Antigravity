using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private RevivedState revivedState;

    private StateMachine stateMachine;

    private TriggerCollisions triggerCollisions;

    private Vector2 lastCheckpointLocation;

    private bool checkpointReached;

    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        revivedState = GetComponent<RevivedState>();
        triggerCollisions = GetComponent<TriggerCollisions>();

        triggerCollisions.onTriggerEnterTrigger += OnTriggerEnterTrigger;
    }

    private void OnTriggerEnterTrigger(Collider2D collider)
    {
        print("Hit: " + collider.name);

        if (collider.transform.CompareTag(Tags.CheckPoint))
        {
            checkpointReached = true;

            lastCheckpointLocation = collider.transform.position;

            //activate the revived state and StartMovingToCenterCheckPoint
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
        stateMachine.ActivateState(StateID.RevivedState);
    }

    public bool CheckPointReached
    {
        get { return checkpointReached; }
    }
}
