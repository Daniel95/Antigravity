using IoCPlus;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LevelEditorSaveGridPositionsCommand : Command {
    
    [Inject] private LevelNameStatus levelNameStatus;

    [InjectParameter] private string newLevelName;

    protected override void Execute() {
        Dictionary<Vector2, Tile> grid = TileGrid.Grid;
        List<Vector2> gridPositions = grid.Keys.ToList();

        string previousLevelName = levelNameStatus.Name;

        bool levelWasRenamed = !string.IsNullOrEmpty(previousLevelName) && newLevelName != previousLevelName;
        if (levelWasRenamed) {
            string oldLevelFileName = StringHelper.ConvertToXMLCompatible(previousLevelName);

            if (File.Exists(LevelEditorLevelDataPath.Path + oldLevelFileName)) {
                File.Delete(LevelEditorLevelDataPath.Path + oldLevelFileName);
            }
        }

        string levelFileName = StringHelper.ConvertToXMLCompatible(newLevelName);
        SerializeHelper.Serialize(LevelEditorLevelDataPath.Path + levelFileName, gridPositions);
    }

}