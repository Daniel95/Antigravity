using IoCPlus;

public class AbortIfPlayerSessionStatsStatusLevelDeathsIsHigherThenZeroCommand : Command {

    [Inject] private PlayerSessionStatsStatus playerSessionStatsStatus;

    protected override void Execute() {
        if(playerSessionStatsStatus.levelDeaths > 0) {
            Abort();
        }
    }

}