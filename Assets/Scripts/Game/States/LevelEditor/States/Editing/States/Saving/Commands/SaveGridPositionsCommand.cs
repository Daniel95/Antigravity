using IoCPlus;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveGridPositionsCommand : Command {

    [Inject] private Ref<ILevelEditorSavingLevelNameInputField> levelEditorSavingLevelNameInputFieldRef;

    [Inject] private LevelNameStatus levelNameStatus;

    protected override void Execute() {
        Dictionary<Vector2, Tile> grid = TileGrid.Grid;
        List<Vector2> gridPositions = grid.Keys.ToList();

        string levelName = levelEditorSavingLevelNameInputFieldRef.Get().Text;

        string levelNameInDirectory = StringHelper.ConvertToDirectoyFriendly(levelName);
        string levelFileName = levelNameInDirectory + ".xml";

        if (levelName != levelNameStatus.Name) {
            File.Delete(LevelEditorLevelDataPath.Path + levelFileName);
        }

        SerializeHelper.Serialize(LevelEditorLevelDataPath.Path + levelName + ".xml", gridPositions);

        levelNameStatus.Name = levelName;
    }

}