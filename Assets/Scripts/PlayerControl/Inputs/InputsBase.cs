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

    public enum TouchStates { Holding, Dragging, Tapped, None }

    public TouchStates touchState = TouchStates.None;

    public virtual void SetInputs(bool _input) { }

    public virtual void ResetTouched()
    {
        touchState = TouchStates.None;

        if (cancelDrag != null)
            cancelDrag();
    }
}

