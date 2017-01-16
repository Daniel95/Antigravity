using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    private MobileInputs _mobileInputs;

    private PCInputs _pcInputs;

    private InputsBase _inputTarget;

    public void RetrieveInputTarget()
    {
        _mobileInputs = GetComponent<MobileInputs>();
        _pcInputs = GetComponent<PCInputs>();
        _inputTarget = GetInputTarget();
    }

    public InputsBase InputTarget
    {
        get { return _inputTarget; }
    }

    private InputsBase GetInputTarget()
    {
        if (Platform.PlatformIsMobile())
        {
            return _mobileInputs;
        }
        else
        {
            return _pcInputs;
        }
    }
}
