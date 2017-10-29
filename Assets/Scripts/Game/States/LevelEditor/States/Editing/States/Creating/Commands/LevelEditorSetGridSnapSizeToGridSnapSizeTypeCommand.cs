using IoCPlus;
using UnityEngine;

public class LevelEditorSetGridSnapSizeToGridSnapSizeTypeCommand : Command<GridSnapSizeType> {

    protected override void Execute(GridSnapSizeType gridSnapSizeType) {
        Vector2 gridSnapSize = LevelEditorGridSnapSizeLibrary.Instance.GetGridSnapSize(gridSnapSizeType);
        LevelEditorGridSnapSizeStatus.Size = gridSnapSize;
    }

}
