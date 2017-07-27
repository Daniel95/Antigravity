using IoCPlus;
using UnityEngine;

public class SetCameraBoundsCommand : Command {

    [Inject] private Ref<ICamera> cameraRef;

    protected override void Execute() {
        CameraBounds cameraBounds = Object.FindObjectOfType<CameraBounds>();

        if(cameraBounds == null) {
            Debug.Log("CameraDounds doesn't exist.");
        }

        Debug.Log(cameraRef.Get());

        cameraRef.Get().SetCameraBounds(cameraBounds);
    }
}
