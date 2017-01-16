using UnityEngine;
using System;
using System.Collections;

public class InputsBase : MonoBehaviour {

    public Action TappedExpired;

    public Action<Vector2> Dragging;

    public Action Release;

    public Action<Vector2> ReleaseInDir;

    public Action CancelDrag;

    public Action Holding;

    public Action Reverse;

    public Action Jump;

    protected Coroutine inputUpdate;

    protected enum TouchStates { Holding, Dragging, Tapped, None }

    protected TouchStates TouchState = TouchStates.None;

    [SerializeField]
    protected float TimebeforeTappedExpired = 0.15f;

    protected float StartDownTime;

    public virtual void SetInputs(bool _input) { }

    public virtual void ResetTouched()
    {
        if (CancelDrag != null)
            CancelDrag();
    }
}

