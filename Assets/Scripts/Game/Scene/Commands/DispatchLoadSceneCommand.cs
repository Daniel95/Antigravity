using IoCPlus;

public class DispatchLoadSceneCommand : Command<Scenes> {

    [Inject] private LoadSceneEvent loadSceneEvent;

    protected override void Execute(Scenes scene) {
        loadSceneEvent.Dispatch(scene);
    }
}
