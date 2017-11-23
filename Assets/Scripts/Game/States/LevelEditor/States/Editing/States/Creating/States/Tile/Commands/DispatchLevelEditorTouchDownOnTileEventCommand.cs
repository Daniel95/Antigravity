using IoCPlus;
using UnityEngine;

public class DispatchLevelEditorTouchDownOnTileEventCommand : Command {

    [Inject] private LevelEditorTouchDownOnTileEvent levelEditorTouchDownOnTileEvent;

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        levelEditorTouchDownOnTileEvent.Dispatch(gridPosition);
    }

}
