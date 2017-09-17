using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class LoadGridPositionsCommand : Command {

    protected override void Execute() {
        List<Vector2> gridPositions = SerializeHelper.Deserialize<List<Vector2>>(Application.persistentDataPath + "/Data/Levels/level1.xml");
        gridPositions.ForEach(x => TileGrid.SetTile(x, new Tile() { UserGenerated = true }));
        TileGenerator.Instance.GenerateTiles(gridPositions);
    }

}
