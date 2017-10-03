using IoCPlus;
using UnityEngine;

public class SetCameraBoundsCommand : Command {

    [Inject] private Ref<ICamera> cameraRef;

    protected override void Execute() {
        cameraRef.Get().SetCameraBounds(CameraBounds.Instance);
    }
}
