using IoCPlus;

public class PlayerRotateAroundCornerStatusView : View {

    [Inject] private static PlayerStartedRotatingAroundCornerEvent playerStartRotatingAroundCornerEvent;
    [Inject] private static PlayerStoppedRotatingAroundCornerEvent playerStopRotatingAroundCorner;

    public static bool Rotating {
        get { return rotating; }
        set {
            rotating = value;
            if(rotating) {
                playerStartRotatingAroundCornerEvent.Dispatch();
            } else {
                playerStopRotatingAroundCorner.Dispatch();
            }
        }
    }

    private static bool rotating;

}
