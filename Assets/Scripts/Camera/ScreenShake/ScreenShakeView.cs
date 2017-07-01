using IoCPlus;
using UnityEngine;

[RequireComponent(typeof(ScreenShake))]
public class ScreenShakeView : View, IScreenShake {

    [Inject] private Ref<IScreenShake> screenShakeRef;

    private ScreenShake screenShake;

    public override void Initialize() {
        screenShakeRef.Set(this);
    }

    public void ShakeInOut(ShakeType shakeType) {
        screenShake.ShakeInOut(shakeType);
    }

    private void Awake() {
        screenShake = GetComponentInParent<ScreenShake>();
    }
}