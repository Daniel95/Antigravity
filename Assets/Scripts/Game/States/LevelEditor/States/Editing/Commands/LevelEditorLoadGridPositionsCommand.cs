using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorLoadGridPositionsCommand : Command {

    [Inject] private LevelNameStatus levelNameStatus;

    protected override void Execute() {
        string levelFileName = StringHelper.ConvertToXMLCompatible(levelNameStatus.Name);

        List<Vector2> gridPositions = SerializeHelper.Deserialize<List<Vector2>>(LevelEditorLevelDataPath.Path + levelFileName);
        gridPositions.ForEach(x => TileGrid.Instance.SetTile(x, new Tile() { UserGenerated = true }));
        TileGenerator.Instance.GenerateTiles(gridPositions);
    }

}
