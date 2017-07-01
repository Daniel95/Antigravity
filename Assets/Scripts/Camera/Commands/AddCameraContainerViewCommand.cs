using IoCPlus;
using UnityEngine;

public class AddCameraContainerViewCommand : Command {

    [Inject] private IContext context;

    protected override void Execute() {
        CameraContainerView cameraContainerView = Object.FindObjectOfType<CameraContainerView>();
        if(cameraContainerView == null) {
            Debug.LogWarning("No CameraContainerView found.");
        }
        context.AddView(cameraContainerView);
    }
}
