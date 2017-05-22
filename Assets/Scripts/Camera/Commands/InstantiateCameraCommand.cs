using IoCPlus;
using UnityEngine;

public class InstantiateCameraCommand : Command {

    [Inject] IContext context;

    protected override void Execute() {
        View cameraView = GameObject.FindGameObjectWithTag(Tags.MainCamera).GetComponent<SmoothFollowCameraView>();

        context.AddView(cameraView);
    }
}
