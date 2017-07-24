using IoCPlus;

public class IncreasePlayerSessionStatsStatusLevelDeathsCommand : Command {

    [Inject] private PlayerSessionStatsStatus playerSessionStatsStatus;

    protected override void Execute() {
        playerSessionStatsStatus.levelDeaths++;
    }

}
