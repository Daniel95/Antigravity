using UnityEngine;
using System.Collections;
using IoCPlus;

public class CalculateTileGridVolumeRectanglesCommand : Command {

    protected override void Execute() {
        foreach (Vector2 position in SelectionFieldStatusView.SelectionFieldGridPositions) {
            if (!levelVolume.Contains(position)) {
                levelVolume.Add(position);
            }
        }
    }

}


