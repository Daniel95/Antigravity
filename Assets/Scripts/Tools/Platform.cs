using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    public enum PlatformType { PC, Mobile };

    private static PlatformType platformTypeUsed;

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            platformTypeUsed = PlatformType.Mobile;
        }
        else
        {
            platformTypeUsed = PlatformType.PC;
        }
    }

    public static bool PlatformIsMobile()
    {
        return platformTypeUsed == PlatformType.Mobile;
    }
}
