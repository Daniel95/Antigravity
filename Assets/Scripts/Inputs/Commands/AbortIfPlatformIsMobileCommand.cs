using IoCPlus;

public class AbortIfPlatformIsMobileCommand : Command {

    protected override void Execute() {
        if(Platform.PlatformIsMobile()) {
            Abort();
        }
    }
}
