using UnityEngine;
using System.Collections.Generic;

public abstract class State : MonoBehaviour {

    protected StateMachine stateMachine;

    protected virtual void Awake() { }

    protected virtual void Start()
    {
        stateMachine = GetComponent<StateMachine>();
    }

    //when we enter this state
    public virtual void EnterState() {
    }

    //after we exited this state, do a cleanup
    public virtual void ResetState() { }

    //when we act according to our state
    public virtual void Act() { }

    public virtual void OnTriggerEnterTrigger(Collider2D collider) { }

    public virtual void OnTriggerEnterCollider(Collider2D collider) { }

    public virtual void OnTriggerExitTrigger(Collider2D collider) { }

    public virtual void OnTriggerExitCollider(Collider2D collider) { }
}
