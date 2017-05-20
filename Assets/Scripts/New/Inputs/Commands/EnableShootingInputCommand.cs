using IoCPlus;

public class EnableShootingInputCommand : Command {

    [Inject] private InputModel inputModel;

    [InjectParameter] private bool enable;

    protected override void Execute() {
        inputModel.shootingInputIsEnabled = enable;
    }
}