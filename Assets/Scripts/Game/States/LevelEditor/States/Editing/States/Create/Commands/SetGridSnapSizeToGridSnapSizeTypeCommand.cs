using IoCPlus;
using UnityEngine;

public class SetGridSnapSizeToGridSnapSizeTypeCommand : Command<GridSnapSizeType> {

    protected override void Execute(GridSnapSizeType gridSnapSizeType) {
        Vector2 gridSnapSize = GridSnapSizeLibrary.Instance.GetGridSnapSize(gridSnapSizeType);
        GridSnapSizeStatusView.Size = gridSnapSize;
    }

}
