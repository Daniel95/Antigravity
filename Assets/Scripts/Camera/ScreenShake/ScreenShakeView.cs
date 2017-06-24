using IoCPlus;
using UnityEngine;

[RequireComponent(typeof(ScreenShake))]
public class ScreenShakeView : View, IScreenShake {

    [Inject] private Ref<IScreenShake> screenShakeRef;

    [SerializeField] private ScreenShake.ShakeData shakeInData;
    [SerializeField] private ScreenShake.ShakeData shakeOutData;

    private ScreenShake screenShake;

    public override void Initialize() {
        screenShakeRef.Set(this);
    }

    public void StartScreenShake() {
        screenShake.ShakeInOut(shakeInData, shakeOutData);
    }

    private void Awake() {
        screenShake = GetComponentInParent<ScreenShake>();
    }
}