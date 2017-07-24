using IoCPlus;

public class ResetPlayerSessionStatsStatusLevelDeaths : Command {

    [Inject] private PlayerSessionStatsStatus playerSessionStatsStatus;

    protected override void Execute() {
        playerSessionStatsStatus.levelDeaths = 0;
    }

}
