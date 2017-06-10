using IoCPlus;
using UnityEngine;

public class AddCameraViewsCommand : Command {

    [Inject] private IContext context;

    protected override void Execute() {
        Camera camera = Camera.main;
        foreach (View view in camera.GetComponents<View>()) {
            context.AddView(view);
        }
    }
}
