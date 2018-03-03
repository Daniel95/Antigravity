using IoCPlus;
using UnityEngine;

public class SetGridSnapSizeToPreviousSizeCommand : Command {

    protected override void Execute() {
        GridSnapSizeStatus.Size = GridSnapSizeStatus.PreviousSize;
    }

}
