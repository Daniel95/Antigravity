using IoCPlus;
using UnityEngine;

public class SetCameraBoundsCommand : Command {

    [Inject] private Ref<IFollowCamera> followCameraRef;

    protected override void Execute() {
        CameraBounds cameraBounds = Object.FindObjectOfType<CameraBounds>();

        if(cameraBounds == null) {
            Debug.Log("CameraDounds doesn't exist.");
        }

        followCameraRef.Get().SetCameraBounds(cameraBounds);
    }
}
