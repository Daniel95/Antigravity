using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private RevivedState _revivedState;

    private StateMachine _stateMachine;

    private Vector2 _lastCheckpointLocation;

    private bool _checkpointReached;

    private void Start()
    {
        _stateMachine = GetComponent<StateMachine>();
        _revivedState = GetComponent<RevivedState>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(Tags.CheckPoint))
        {
            _checkpointReached = true;

            _lastCheckpointLocation = collision.transform.position;

            //activate the revived state and StartMovingToCenterCheckPoint
            ActivateRevivedState();
            _revivedState.IsInPosition = false;
            _revivedState.StartMovingToCenterCheckPoint(_lastCheckpointLocation);
        }
    }

    public void Revive()
    {
        ActivateRevivedState();

        transform.position = _lastCheckpointLocation;
    }

    /// <summary>
    /// put the player in the revived state.
    /// </summary>
    private void ActivateRevivedState()
    {
        _stateMachine.ActivateState(StateID.RevivedState);
    }

    public bool CheckPointReached
    {
        get { return _checkpointReached; }
    }
}
