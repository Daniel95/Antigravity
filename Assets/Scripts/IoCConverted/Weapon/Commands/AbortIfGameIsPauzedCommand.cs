using IoCPlus;

public class AbortIfGameIsPauzedCommand : Command {

	protected override void Execute() {
        if (TimeManagement.isPauzed()) {
            Abort();

        }
    }

}