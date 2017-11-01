using IoCPlus;

public class PlayerRotatingAroundCornerStatusView : StatusView {

    [Inject] private static PlayerStartRotatingAroundCornerEvent playerStartRotatingAroundCornerEvent;
    [Inject] private static PlayerStopRotatingAroundCornerEvent playerStopRotatingAroundCorner;

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
