using IoCPlus;

public class DispatchLoadNextSceneEventCommand : Command {

    [Inject] private UnloadedCurrentSceneEvent loadNextSceneEvent;

    protected override void Execute() {
        loadNextSceneEvent.Dispatch();
    }
}
