using UnityEngine;
using System;

public class PlayerInputs : MonoBehaviour, ITriggerable {

    public bool triggered { get; set; }

    private InputController inputController;

    //shoot inputs
    public Action<Vector2> dragging;
    public Action<Vector2> releaseInDir;
    public Action release;
    public Action holding;
    public Action tappedExpired;
    public Action cancelDrag;

    //movement inputs
    public Action action;
    public Action reverse;

    private void Awake()
    {
        inputController = GetComponent<InputController>();
        inputController.RetrieveInputTarget();
        SetInputs(true);
    }

    public void TriggerActivate()
    {
        SetShootInput(true);
        SetActionInput(true);
        SetReverseInput(true);
    }

    public void TriggerStop()
    {
        SetShootInput(false);
        SetActionInput(false);
        SetReverseInput(false);
    }

    public void SetInputs(bool _input)
    {
        inputController.InputTarget.SetInputs(_input);
    }

    public void SetActionInput(bool _inputState)
    {
        if(_inputState)
        {
            inputController.InputTarget.action += Action;
        }
        else if(!_inputState)
        {
            inputController.InputTarget.action -= Action;
        }
    }

    public void SetReverseInput(bool _inputState)
    {
        if (_inputState)
        {
            inputController.InputTarget.reverse += Reverse;
        }
        else if (!_inputState)
        {
            inputController.InputTarget.reverse -= Reverse;
        }
    }

    public void SetDoubleReverseInput(bool _inputState)
    {
        if (_inputState)
        {
            inputController.InputTarget.action += Reverse;
        }
        else if (!_inputState)
        {
            inputController.InputTarget.action -= Reverse;
        }
    }

    public void SetHoldInput(bool _inputState)
    {
        if(_inputState)
        {
            inputController.InputTarget.holding += holding;
        }
        else
        {
            inputController.InputTarget.holding -= holding;
        }
    }

    public void SetShootInput(bool _inputState)
    {
        if (_inputState)
        {

            if(inputController.InputTarget.release == null)
                inputController.InputTarget.release += Release;

            if (inputController.InputTarget.releaseInDir == null)
                inputController.InputTarget.releaseInDir += ReleaseInDir;

            if (inputController.InputTarget.dragging == null)
                inputController.InputTarget.dragging += Dragging;

            if (inputController.InputTarget.cancelDrag == null)
                inputController.InputTarget.cancelDrag += CancelDrag;

            if (inputController.InputTarget.tappedExpired == null)
                inputController.InputTarget.tappedExpired += TappedExpired;
        }
        else if (!_inputState)
        {
            inputController.InputTarget.release -= Release;
            inputController.InputTarget.dragging -= Dragging;
            inputController.InputTarget.cancelDrag -= CancelDrag;
            inputController.InputTarget.tappedExpired -= TappedExpired;
        }
    }

    public void ResetTouched()
    {
        inputController.InputTarget.ResetTouched();
    }

    private void Release()
    {
        if (release != null)
        {
            release();
        }
    }

    private void ReleaseInDir(Vector2 _dir)
    {
        if(releaseInDir != null)
        {
            releaseInDir(_dir);
        }
    }

    private void TappedExpired()
    {
        if(tappedExpired != null)
        {
            tappedExpired();
        }
    }

    private void Dragging(Vector2 _dir)
    {
        if (dragging != null)
        {
            dragging(_dir);
        }
    }

    private void CancelDrag()
    {
        if(cancelDrag != null)
        {
            cancelDrag();
        }
    }

    private void Holding()
    {
        if(holding != null)
        {
            holding();
        }
    }

    private void Action()
    {
        if (action != null)
        {
            action();
        }
    }

    private void Reverse()
    {
        if (reverse != null)
        {
            reverse();
        }
    }
} 
