using IoCPlus;

public class AbortIfPlatformIsNotMobileCommand : Command {

    protected override void Execute() {
        if(!PlatformHelper.PlatformIsMobile()) {
            Abort();
        }
    }
}
