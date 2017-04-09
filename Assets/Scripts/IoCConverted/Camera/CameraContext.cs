using IoCPlus;
using UnityEngine;

public class CameraContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateCameraCommand>();
    }

}