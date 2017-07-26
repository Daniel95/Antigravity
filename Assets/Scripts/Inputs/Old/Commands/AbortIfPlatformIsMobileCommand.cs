using IoCPlus;

public class AbortIfPlatformIsMobileCommand : Command {

    protected override void Execute() {
        if(PlatformHelper.PlatformIsMobile) {
            Abort();
        }
    }
}
