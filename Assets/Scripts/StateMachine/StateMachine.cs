using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum StateID
{
    OnFootState = 0,
    GrapplingState = 1,
    LaunchedState = 2,
    RevivedState = 3,
}

public class StateMachine : MonoBehaviour {

    private Dictionary<StateID, State> states = new Dictionary<StateID, State>();

    private State activeState;

    private Coroutine acting;

    public void StartStateMachine(StateID _stateID)
    {
        if(acting != null)
        {
            StopStateMachine();
        }

        //assign the state we start with
        activeState = states[_stateID];

        activeState.EnterState();

        //start acting
        acting = StartCoroutine(Acting());
    }

    public void StopStateMachine()
    {
        StopCoroutine(acting);
    }

    IEnumerator Acting()
    {
        while(true)
        {
            activeState.Act();
            yield return null;
        }
    }

    public void ActivateState(StateID _stateID)
    {
        //reset our current state, before assigning a new state
        activeState.ResetState();

        activeState = states[_stateID];

        activeState.EnterState();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (activeState != null)
        {
            activeState.OnCollEnter(collision);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (activeState != null)
        {
            activeState.OnCollExit(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activeState != null)
        {
            activeState.OnTrigEnter(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (activeState != null)
        {
            activeState.OnTrigExit(collision);
        }
    }

    //at the start of the game, we put all the states scripts here that we might need for this object.
    //the scripts we add here are limited to those that inherit from State.
    public void AssignState(StateID _stateID, State _state) {
        states.Add(_stateID, _state);
    }
}

