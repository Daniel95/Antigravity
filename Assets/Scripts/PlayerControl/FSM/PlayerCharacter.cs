using UnityEngine;
using System.Collections;


public class PlayerCharacter : MonoBehaviour
{
    private StateMachine stateMachine;

    // Use this for initialization
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();

        //wait 1 frame before we start the player statemachine, so all scripts have time to retrieve all their needed info
        GetComponent<Frames>().ExecuteAfterDelay(1,StartPlayerStatemachine);
    }

    void StartPlayerStatemachine()
    {
        AssignStates();

        stateMachine.ActivateState(StateID.OnFootState);
    }

    void AssignStates()
    {
        stateMachine.AssignState(StateID.OnFootState, GetComponent<OnFootState>());
        stateMachine.AssignState(StateID.LaunchedState, GetComponent<LaunchedState>());
        stateMachine.AssignState(StateID.GrapplingState, GetComponent<GrapplingState>());
    }
}
