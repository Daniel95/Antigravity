using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorLoadLevelSaveDataCommand : Command {

    [Inject] private LevelNameStatus levelNameStatus;

    protected override void Execute() {
        string levelFileName = StringHelper.ConvertToXMLCompatible(levelNameStatus.Name);

        LevelSaveData levelSaveData = SerializeHelper.Deserialize<LevelSaveData>(LevelEditorLevelDataPath.Path + levelFileName);

        List<Vector2> tileGridPositions = levelSaveData.TileGridPositions;
        foreach (Vector2 tileGridPosition in tileGridPositions) {
            LevelEditorTileGrid.Instance.SetTile(tileGridPosition, new Tile() { UserGenerated = true });
        }
        TileGenerator.Instance.GenerateTiles(tileGridPositions);

        List<LevelObjectSaveData> levelObjectSaveDatas = levelSaveData.LevelObjectSaveDatas;
        foreach (LevelObjectSaveData levelObjectSaveData in levelObjectSaveDatas) {
            LevelObject levelObject = new LevelObject();

            LevelObjectType levelObjectType = levelObjectSaveData.LevelObjectType;
            LevelEditorLevelObjectEditorNode levelEditorLevelObjectEditorNode = LevelEditorLevelObjectEditorNodesContainer.Instance.GetNode(levelObjectType);
            GameObject prefab = levelEditorLevelObjectEditorNode.Prefab;
            Vector2 gameObjectPosition = levelObjectSaveData.GameObjectPosition;
            GameObject levelObjectGameObject = Object.Instantiate(prefab, gameObjectPosition, new Quaternion());

            levelObject.Initiate(levelObjectSaveData.GridPositions, levelObjectGameObject, levelObjectType);
        }
    }

}
