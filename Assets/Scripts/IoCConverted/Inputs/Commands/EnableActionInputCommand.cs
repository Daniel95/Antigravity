using IoCPlus;
using UnityEngine;

public class EnableActionInputCommand : Command {

    [Inject] private InputModel inputModel;

    [InjectParameter] private bool enable;

    protected override void Execute() {
        inputModel.actionInputIsEnabled = enable;
    }
}