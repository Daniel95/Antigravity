using IoCPlus;

public class GameUIPauseButtonView : View {

    [Inject] private GameUIPausePressedEvent gameUIPausePressedEvent;

    public void PausePressed() {
        gameUIPausePressedEvent.Dispatch();
    }

}
