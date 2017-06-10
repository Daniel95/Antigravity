using IoCPlus;
using UnityEngine;

[RequireComponent(typeof(ScreenShake))]
public class ScreenShakeView : View, IScreenShake {

    [SerializeField] private ScreenShake.ShakeData shakeInData;
    [SerializeField] private ScreenShake.ShakeData shakeOutData;

    private ScreenShake screenShake;

    public void StartScreenShake() {
        screenShake.ShakeInOut(shakeInData, shakeOutData);
    }

    private void Awake() {
        screenShake = GetComponentInParent<ScreenShake>();
    }
}