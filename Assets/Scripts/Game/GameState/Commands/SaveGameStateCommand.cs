using IoCPlus;
using UnityEngine;

public class SaveGameStateCommand : Command {

    [Inject] private IGameStateService gameStateService;
    [Inject] private Ref<GameStateModel> gameStateModelRef;

	protected override void Execute() {
        gameStateService.Save(gameStateModelRef.Get());
    }

}