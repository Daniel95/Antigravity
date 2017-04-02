using UnityEngine;
using System.Collections;

public class EnemyCharacter : MonoBehaviour
{
    private StateMachine _stateMachine;

    // Use this for initialization
    void Start()
    {
        _stateMachine = GetComponent<StateMachine>();

        //wait 1 frame before we start the player statemachine, so all scripts have time to retrieve all their needed info
        GetComponent<Frames>().ExecuteAfterDelay(1, StartPlayerStatemachine);
    }

    void StartPlayerStatemachine()
    {
        AssignStates();

        _stateMachine.StartStateMachine(StateID.PatrollingState);
    }

    void AssignStates()
    {
        _stateMachine.AssignState(StateID.PatrollingState, GetComponent<PatrollingView>());
    }
}
