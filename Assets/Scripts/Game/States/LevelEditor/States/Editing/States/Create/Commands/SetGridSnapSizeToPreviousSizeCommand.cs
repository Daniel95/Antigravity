using IoCPlus;
using UnityEngine;

public class SetGridSnapSizeToPreviousSizeCommand : Command {

    protected override void Execute() {
        GridSnapSizeStatusView.Size = GridSnapSizeStatusView.PreviousSize;
    }

}
