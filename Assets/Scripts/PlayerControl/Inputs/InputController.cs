using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    private MobileInputs mobileInputs;

    private PCInputs pcInputs;

    private InputsBase inputTarget;

    public void RetrieveInputTarget()
    {
        mobileInputs = GetComponent<MobileInputs>();
        pcInputs = GetComponent<PCInputs>();
        inputTarget = GetInputTarget();
    }

    public InputsBase InputTarget
    {
        get { return inputTarget; }
    }

    private InputsBase GetInputTarget()
    {
        if (Platform.PlatformIsMobile())
        {
            return mobileInputs;
        }
        else
        {
            return pcInputs;
        }
    }
}
