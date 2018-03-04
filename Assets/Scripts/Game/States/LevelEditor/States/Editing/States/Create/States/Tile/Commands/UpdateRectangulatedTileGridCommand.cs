using IoCPlus;

public class UpdateRectangulatedTileGridCommand : Command {

    [Inject] private RectangulatedTileGridStatus rectangulatedTileGridStatus;

    protected override void Execute() {
        rectangulatedTileGridStatus.UpdateRectangulatedTileGrid();
    }

}
