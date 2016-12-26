﻿using UnityEngine;
using System;
using System.Collections;

public class InputsBase : MonoBehaviour {

    public Action<Vector2> dragging;

    public Action<Vector2> release;

    public Action cancelDrag;

    public Action flipSpeed;

    public Action action;

    protected Coroutine updateJoyStickInputs;
    protected Coroutine updateDragInputs;

    public virtual void StartUpdatingDragInputs() { }
    public virtual void StopUpdatingDragInputs() { }
    public virtual void StartUpdatingJoyStickInputs() { }
    public virtual void StopUpdatingJoyStickInputs() { }
}
