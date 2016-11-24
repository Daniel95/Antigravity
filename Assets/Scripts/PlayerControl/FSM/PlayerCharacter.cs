﻿using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private StateMachine stateMachine;

    // Use this for initialization
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();

        AssignStates();

        stateMachine.ActivateState(StateID.OnFootState);
        stateMachine.ActivateState(StateID.SwitchingGravityState);
    }

    void AssignStates()
    {
        stateMachine.AssignState(StateID.OnFootState, GetComponent<OnFootState>());
        stateMachine.AssignState(StateID.GrapplingState, GetComponent<GrapplingState>());
        stateMachine.AssignState(StateID.StickyState, GetComponent<StickyState>());
        stateMachine.AssignState(StateID.SwitchingGravityState, GetComponent<SwitchingGravityState>());
    }
}
