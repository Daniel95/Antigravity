using IoCPlus;
using UnityEngine;

public class SetGridOverlaySizeToScreenWorldSizeCommand : Command {

    protected override void Execute() {
        float screenHeightInUnits = Camera.main.orthographicSize * 2;
        float screenWidthInUnits = screenHeightInUnits * Screen.width / Screen.height;
        GridOverlay.Instance.Size = new Vector2(screenWidthInUnits, screenHeightInUnits);
    }

}
