using IoCPlus;
using UnityEngine;

public class LevelEditorSetGridSnapSizeToPreviousCommand : Command {

    protected override void Execute() {
        LevelEditorGridSnapSizeStatus.Size = LevelEditorGridSnapSizeStatus.PreviousSize;
    }

}
