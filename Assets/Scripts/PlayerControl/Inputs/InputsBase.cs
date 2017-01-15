using UnityEngine;
using System;
using System.Collections;

public class InputsBase : MonoBehaviour {

    public Action tappedExpired;

    public Action<Vector2> dragging;

    public Action release;

    public Action<Vector2> releaseInDir;

    public Action cancelDrag;

    public Action holding;

    public Action reverse;

    public Action action;

    public Coroutine inputUpdate;

    protected enum TouchStates { Holding, Dragging, Tapped, None }

    protected TouchStates touchState = TouchStates.None;

    [SerializeField]
    protected float timebeforeTappedExpired = 0.15f;

    protected float startDownTime;

    protected bool tappedIsExpired;

    public virtual void SetInputs(bool _input) { }

    public virtual void ResetTouched()
    {
        if (cancelDrag != null)
            cancelDrag();
    }
}

