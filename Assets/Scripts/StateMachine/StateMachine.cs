using UnityEngine;
using System.Collections.Generic;

public enum StateID
{
    OnFootState = 0,
    GrapplingState = 1,
    LaunchedState = 2,
}

public class StateMachine : MonoBehaviour {

    private Dictionary<StateID, State> states = new Dictionary<StateID, State>();

    private List<State> currentActiveStates = new List<State>();

    void Update()
    {
        //for every state in currentActiveStates, activate the methods:
        //act: act like this state wants us to.

        for (int i = 0; i < currentActiveStates.Count; i++) {
            currentActiveStates[i].Act();
        }
    }

    public void ActivateState(StateID _stateID)
    {
        //we add the new state to currentActive
        currentActiveStates.Add(states[_stateID]);

        //then activate the last state in the list (the new one) 
        currentActiveStates[currentActiveStates.Count - 1].EnterState();
    }

    public void DeactivateState(StateID _stateID)
    {
        //we loop throught the list until we find the right state,
        //and then we activate the Reset method, in case we need to reset anything before we deactivate this state.
        for (int i = 0; i < currentActiveStates.Count; i++)
        {
            if (currentActiveStates[i] == states[_stateID])
            {
                currentActiveStates[i].ResetState();
            }
        }

        //we remove the state from currentActiveStates
        currentActiveStates.Remove(states[_stateID]);
    }

    void OnCollisionEnter2D(Collision2D coll) {

        for (int i = 0; i < currentActiveStates.Count; i++)
        {
            currentActiveStates[i].OnCollEnter2D(coll);
        }
    }

    void OnCollisionExit2D(Collision2D coll) {

        for (int i = 0; i < currentActiveStates.Count; i++)
        {
            currentActiveStates[i].OnCollExit2D(coll);
        }
    }

    //at the start of the game, we put all the states scripts here that we might need for this object.
    //the scripts we add here are limited to those that inherit from State.
    public void AssignState(StateID _stateID, State _state) {
        states.Add(_stateID, _state);
    }
}

