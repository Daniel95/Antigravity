using IoCPlus;

public class GameUIPlayButtonView : View {

    [Inject] private GameUIPlayEvent gameUIPlayEvent;

    public void PlayPressed() {
        gameUIPlayEvent.Dispatch();
    }

}
