using IoCPlus;

public class AbortIfPlayerSessionStatsStatusLevelDeathsIsZeroCommand : Command {

    [Inject] private PlayerSessionStatsStatus playerSessionStatsStatus;

    protected override void Execute() {
        if(playerSessionStatsStatus.levelDeaths == 0) {
            Abort();
        }
    }

}