using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum StateID
{
    //player
    OnFootState = 0,
    GrapplingState = 1,
    LaunchedState = 2,
    RevivedState = 3,

    //enemy
    PatrollingState = 4,
}

public class StateMachine : MonoBehaviour {

    private Dictionary<StateID, State> _states = new Dictionary<StateID, State>();

    private State _activeState;

    private Coroutine _acting;

    public void StartStateMachine(StateID stateId)
    {
        if(_acting != null)
        {
            StopStateMachine();
        }

        //assign the state we start with
        _activeState = _states[stateId];

        _activeState.EnterState();

        //start acting
        _acting = StartCoroutine(Acting());
    }

    public void StopStateMachine()
    {
        StopCoroutine(_acting);
    }

    IEnumerator Acting()
    {
        while(true)
        {
            _activeState.Act();
            yield return null;
        }
    }

    public void ActivateState(StateID stateId)
    {
        //reset our current state, before assigning a new state
        _activeState.ResetState();

        _activeState = _states[stateId];

        _activeState.EnterState();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_activeState != null)
        {
            _activeState.OnCollEnter(collision);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_activeState != null)
        {
            _activeState.OnCollExit(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_activeState != null)
        {
            _activeState.OnTrigEnter(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_activeState != null)
        {
            _activeState.OnTrigExit(collision);
        }
    }

    //at the start of the game, we put all the states scripts here that we might need for this object.
    //the scripts we add here are limited to those that inherit from State.
    public void AssignState(StateID _stateID, State _state) {
        _states.Add(_stateID, _state);
    }
}

