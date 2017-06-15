using IoCPlus;

public class EnableShootingInputCommand : Command {

    [Inject] private InputStatus inputStatus;

    [InjectParameter] private bool enable;

    protected override void Execute() {
        inputStatus.shootingInputIsEnabled = enable;
    }
}