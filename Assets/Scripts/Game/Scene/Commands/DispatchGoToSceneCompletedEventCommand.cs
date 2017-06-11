using IoCPlus;

public class DispatchGoToSceneCompletedEventCommand : Command {

    [Inject] private SceneStatus sceneStatus;

    [Inject] private GoToSceneCompletedEvent GoToSceneCompletedEvent;

    protected override void Execute() {
        GoToSceneCompletedEvent.Dispatch(sceneStatus.currentScene);
    }
}
