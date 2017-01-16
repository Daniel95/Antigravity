using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    private enum PlatformType { PC, Mobile };

    private static PlatformType _platformTypeUsed;

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _platformTypeUsed = PlatformType.Mobile;
        }
        else
        {
            _platformTypeUsed = PlatformType.PC;
        }
    }

    public static bool PlatformIsMobile()
    {
        return _platformTypeUsed == PlatformType.Mobile;
    }
}
