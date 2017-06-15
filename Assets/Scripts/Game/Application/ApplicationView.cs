using IoCPlus;

public class ApplicationView : View {

    [Inject] private ApplicationPauseEvent applicationPauseEvent;
    [Inject] private ApplicationQuitEvent applicationQuitEvent;

    protected override void OnApplicationQuit() {
        applicationQuitEvent.Dispatch();
    }

    private void OnApplicationPause(bool pause) {
        if (pause) {
            applicationPauseEvent.Dispatch();
        }
    }

}