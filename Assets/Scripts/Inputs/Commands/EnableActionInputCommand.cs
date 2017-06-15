using IoCPlus;
using UnityEngine;

public class EnableActionInputCommand : Command {

    [Inject] private InputStatus inputStatus;

    [InjectParameter] private bool enable;

    protected override void Execute() {
        inputStatus.actionInputIsEnabled = enable;
    }
}