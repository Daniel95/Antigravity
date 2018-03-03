using IoCPlus;
using UnityEngine;

public class DispatchTouchDownOnTileEventCommand : Command {

    [Inject] private TouchDownOnTileEvent levelEditorTouchDownOnTileEvent;

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        levelEditorTouchDownOnTileEvent.Dispatch(gridPosition);
    }

}
