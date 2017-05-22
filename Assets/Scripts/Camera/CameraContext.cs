using IoCPlus;
using UnityEngine;

public class CameraContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<Ref<IScreenShake>>();

        On<EnterContextSignal>()
            .Do<InstantiateCameraCommand>();
    }

}