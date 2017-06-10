using IoCPlus;

public class DispatchLoadSceneCommand : Command<Scenes> {

    [Inject] private GoToSceneEvent loadSceneEvent;

    protected override void Execute(Scenes scene) {
        loadSceneEvent.Dispatch(scene);
    }
}
