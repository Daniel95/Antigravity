using IoCPlus;

public class EnableInputPlatformCommand : Command {

    [Inject] private InputModel inputModel;

    [InjectParameter] private bool enable;

    protected override void Execute() {
        inputModel.activeInputPlatform.EnableInput(enable);
    }
}