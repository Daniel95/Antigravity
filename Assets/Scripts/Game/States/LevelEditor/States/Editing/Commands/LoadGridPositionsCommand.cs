using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class LoadGridPositionsCommand : Command {

    [Inject] private SavedLevelNameStatus levelNameStatus;

    protected override void Execute() {
        List<Vector2> gridPositions = SerializeHelper.Deserialize<List<Vector2>>(LevelEditorLevelDataPath.Path + levelNameStatus + ".xml");
        gridPositions.ForEach(x => TileGrid.SetTile(x, new Tile() { UserGenerated = true }));
        TileGenerator.Instance.GenerateTiles(gridPositions);
    }

}
