using IoCPlus;

public class DispatchLoadNextSceneEventCommand : Command {

    [Inject] private LoadNextSceneEvent loadNextSceneEvent;

    protected override void Execute() {
        loadNextSceneEvent.Dispatch();
    }
}
