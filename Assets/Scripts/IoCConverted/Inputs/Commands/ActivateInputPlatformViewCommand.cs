using IoCPlus;
using UnityEngine;

public class ActivateInputPlatformViewCommand : Command {

    [Inject] IContext context;

    [Inject] private InputModel inputModel;

    [InjectParameter] private bool activate;

    protected override void Execute() {
        if(activate && !inputModel.isActive) {
            context.AddView(inputModel.inputPlatform as View, false);
        } else if(!activate && inputModel.isActive) {
            (inputModel.inputPlatform as View).Delete();
        }

        inputModel.isActive = activate;
    }
}