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

    public virtual void OnCollEnter(Collision2D collision) { }

    public virtual void OnCollExit(Collision2D collision) { }

    public virtual void OnTrigEnter(Collider2D collider) { }

    public virtual void OnTrigExit(Collider2D collider) { }
}
