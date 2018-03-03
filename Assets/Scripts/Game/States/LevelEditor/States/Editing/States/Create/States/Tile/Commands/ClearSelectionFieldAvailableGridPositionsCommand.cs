using IoCPlus;
using UnityEngine;

public class ClearSelectionFieldAvailableGridPositionsCommand : Command {

    [Inject] private Ref<ITileInput> levelEditorTileInputRef;

    protected override void Execute() {
        levelEditorTileInputRef.Get().ClearSelectionFieldAvailableGridPositions();
    }

}
