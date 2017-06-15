using IoCPlus;

public class LoadGameStateCommand : Command {

    [Inject] private IGameStateService gameStateService;
    [Inject] private Ref<GameStateModel> gameStateModelRef;
    
    protected override void Execute() {
        if (SROptions.Current.DeleteGameSaveOnStart) {
            gameStateModelRef.Set(new GameStateModel());
        } else {
            gameStateModelRef.Set(gameStateService.Load());
        }
    }

}