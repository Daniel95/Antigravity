using UnityEngine;

public static class Platform {

    public static bool PlatformIsMobile() {
        return Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
    }

}
