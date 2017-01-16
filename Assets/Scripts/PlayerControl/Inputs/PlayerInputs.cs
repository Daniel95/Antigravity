using UnityEngine;
using System;

public class PlayerInputs : MonoBehaviour, ITriggerable {

    public bool Triggered { get; set; }

    private InputController _inputController;

    public Action<Vector2> Dragging;
    public Action<Vector2> ReleaseInDir;
    public Action Release;
    public Action Holding;
    public Action TappedExpired;
    public Action CancelDrag;

    //movement inputs
    public Action Jump;
    public Action Reverse;

    private void Awake()
    {
        _inputController = GetComponent<InputController>();
        _inputController.RetrieveInputTarget();
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

    public void SetInputs(bool input)
    {
        _inputController.InputTarget.SetInputs(input);
    }

    public void SetActionInput(bool inputState)
    {
        if(inputState)
        {
            _inputController.InputTarget.Jump += CheckJump;
        }
        else if(!inputState)
        {
            _inputController.InputTarget.Jump -= CheckJump;
        }
    }

    public void SetReverseInput(bool inputState)
    {
        if (inputState)
        {
            _inputController.InputTarget.Reverse += CheckReverse;
        }
        else
        {
            _inputController.InputTarget.Reverse -= CheckReverse;
        }
    }

    public void SetDoubleReverseInput(bool inputState)
    {
        if (inputState)
        {
            _inputController.InputTarget.Jump += CheckReverse;
        }
        else
        {
            _inputController.InputTarget.Jump -= CheckReverse;
        }
    }

    public void SetHoldInput(bool inputState)
    {
        if(inputState)
        {
            _inputController.InputTarget.Holding += CheckHolding;
        }
        else
        {
            _inputController.InputTarget.Holding -= CheckHolding;
        }
    }

    public void SetShootInput(bool inputState)
    {
        if (inputState)
        {

            if(_inputController.InputTarget.Release == null)
                _inputController.InputTarget.Release += CheckRelease;

            if (_inputController.InputTarget.ReleaseInDir == null)
                _inputController.InputTarget.ReleaseInDir += CheckReleaseInDir;

            if (_inputController.InputTarget.Dragging == null)
                _inputController.InputTarget.Dragging += CheckDragging;

            if (_inputController.InputTarget.CancelDrag == null)
                _inputController.InputTarget.CancelDrag += CheckCancelDrag;

            if (_inputController.InputTarget.TappedExpired == null)
                _inputController.InputTarget.TappedExpired += CheckTappedExpired;
        }
        else
        {
            _inputController.InputTarget.Release -= CheckRelease;
            _inputController.InputTarget.Dragging -= CheckDragging;
            _inputController.InputTarget.CancelDrag -= CheckCancelDrag;
            _inputController.InputTarget.TappedExpired -= CheckTappedExpired;
        }
    }

    public void ResetTouched()
    {
        _inputController.InputTarget.ResetTouched();
    }

    private void CheckRelease()
    {
        if (Release != null)
        {
            Release();
        }
    }

    private void CheckReleaseInDir(Vector2 dir)
    {
        if(ReleaseInDir != null)
        {
            ReleaseInDir(dir);
        }
    }

    private void CheckTappedExpired()
    {
        if(TappedExpired != null)
        {
            TappedExpired();
        }
    }

    private void CheckDragging(Vector2 dir)
    {
        if (Dragging != null)
        {
            Dragging(dir);
        }
    }

    private void CheckCancelDrag()
    {
        if(CancelDrag != null)
        {
            CancelDrag();
        }
    }

    private void CheckHolding()
    {
        if(Holding != null)
        {
            Holding();
        }
    }

    private void CheckJump()
    {
        if (Jump != null)
        {
            Jump();
        }
    }

    private void CheckReverse()
    {
        if (Reverse != null)
        {
            Reverse();
        }
    }
} 
