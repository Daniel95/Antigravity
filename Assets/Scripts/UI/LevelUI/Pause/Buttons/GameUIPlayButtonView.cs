using IoCPlus;

public class GameUIPlayButtonView : View {

    [Inject] private GameUIPlayPressedEvent gameUIPlayPressedEvent;

    public void PlayPressed() {
        gameUIPlayPressedEvent.Dispatch();
    }

}
