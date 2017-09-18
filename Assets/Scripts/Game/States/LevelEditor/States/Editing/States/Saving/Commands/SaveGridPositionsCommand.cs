using IoCPlus;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveGridPositionsCommand : Command {

    [Inject] private Ref<ILevelEditorSavingLevelNameInputField> levelEditorSavingLevelNameInputFieldRef;

    [Inject] private SavedLevelNameStatus savedLevelNameStatus;

    protected override void Execute() {
        Dictionary<Vector2, Tile> grid = TileGrid.Grid;
        List<Vector2> gridPositions = grid.Keys.ToList();

        string levelName = levelEditorSavingLevelNameInputFieldRef.Get().Text;
        string previousLevelName = savedLevelNameStatus.Name;

        if (!string.IsNullOrEmpty(previousLevelName) && levelName != previousLevelName) {
            string oldLevelNameInDirectory = StringHelper.ConvertToDirectoyCompatible(previousLevelName);
            string oldLevelFileName = oldLevelNameInDirectory + ".xml";

            if (File.Exists(LevelEditorLevelDataPath.Path + oldLevelFileName)) {
                File.Delete(LevelEditorLevelDataPath.Path + oldLevelFileName);
            }
        }

        string levelNameInDirectory = StringHelper.ConvertToDirectoyCompatible(levelName);
        string levelFileName = levelNameInDirectory + ".xml";

        SerializeHelper.Serialize(LevelEditorLevelDataPath.Path + levelFileName, gridPositions);

        savedLevelNameStatus.Name = levelName;
    }

}