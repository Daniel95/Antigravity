using IoCPlus;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveGridPositionsCommand : Command {

    [Inject] private Ref<ILevelEditorSavingLevelNameInputField> levelEditorSavingLevelNameInputFieldRef;

    [Inject] private LevelNameStatus levelNameStatus;

    private const string LEVEL_DATA_PATH =  "/Data/Levels/";

    protected override void Execute() {
        Dictionary<Vector2, Tile> grid = TileGrid.Grid;
        List<Vector2> gridPositions = grid.Keys.ToList();

        string levelName = levelEditorSavingLevelNameInputFieldRef.Get().Text;

        if(levelName != levelNameStatus.LoadedLevelName) {
            File.Delete(Application.persistentDataPath + LEVEL_DATA_PATH + levelNameStatus.LoadedLevelName + ".xml");
        }

        SerializeHelper.Serialize(Application.persistentDataPath + LEVEL_DATA_PATH + levelName + ".xml", gridPositions);

        levelNameStatus.LoadedLevelName = levelName;
    }

}