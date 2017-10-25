using IoCPlus;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelEditorLoadLevelSaveDataCommand : Command {

    [Inject] private LevelEditorStatus levelEditorStatus;
    [Inject] private LevelEditorOffGridLevelObjectsStatus offGridLevelObjectsStatus;
    [Inject] private LevelContainerTransformStatus levelContainerStatus;
    [Inject] private LevelNameStatus levelNameStatus;

    protected override void Execute() {
        string levelFileName = StringHelper.ConvertToXMLCompatible(levelNameStatus.Name);

        LevelSaveData levelSaveData = SerializeHelper.Deserialize<LevelSaveData>(LevelEditorLevelDataPath.Path + levelFileName);

        List<Vector2> standardTileGridPositions = levelSaveData.StandardTileGridPositions;
        List<Vector2> nonStandardUserGeneratedTileGridPositions = levelSaveData.NonStandardUserGeneratedTilesSaveData.Select(x => x.GridPosition).ToList();
        List<Vector2> userGeneratedTileGridPositions = standardTileGridPositions.Concat(nonStandardUserGeneratedTileGridPositions).ToList();

        TileGenerator.SpawnTiles(userGeneratedTileGridPositions);

        List<OnGridLevelObjectSaveData> onGridlevelObjectsSaveData = levelSaveData.OnGridLevelObjectsSaveData;
        SpawnOnGridLevelObjects(onGridlevelObjectsSaveData);

        List<OffGridLevelObjectSaveData> offGridlevelObjectsSaveData = levelSaveData.OffGridLevelObjectsSaveData;
        SpawnOffGridLevelObjects(offGridlevelObjectsSaveData);
    }

    private void SpawnOnGridLevelObjects(List<OnGridLevelObjectSaveData> onGridlevelObjectsSaveData) {
        foreach (OnGridLevelObjectSaveData onGridlevelObjectSaveData in onGridlevelObjectsSaveData) {
            OnGridLevelObject onGridlevelObject = new OnGridLevelObject();

            LevelObjectType levelObjectType = onGridlevelObjectSaveData.LevelObjectType;
            GenerateableLevelObjectNode levelEditorLevelObjectEditorNode = GenerateableLevelObjectLibrary.GetNode(levelObjectType);
            GameObject prefab = levelEditorLevelObjectEditorNode.Prefab;
            Vector2 gameObjectPosition = onGridlevelObjectSaveData.GameObjectPosition;

            GameObject levelObjectGameObject = Object.Instantiate(prefab, gameObjectPosition, new Quaternion(), levelContainerStatus.LevelContainer);

            onGridlevelObject.Initiate(onGridlevelObjectSaveData.GridPositions, levelObjectGameObject, levelObjectType);
        }
    }

    private void SpawnOffGridLevelObjects(List<OffGridLevelObjectSaveData> offGridlevelObjectsSaveData) {
        foreach (OffGridLevelObjectSaveData offGridlevelObjectSaveData in offGridlevelObjectsSaveData) {
            LevelObjectType levelObjectType = offGridlevelObjectSaveData.LevelObjectType;
            GenerateableLevelObjectNode levelEditorLevelObjectEditorNode = GenerateableLevelObjectLibrary.GetNode(levelObjectType);
            GameObject prefab = levelEditorLevelObjectEditorNode.Prefab;
            Vector2 position = offGridlevelObjectSaveData.Position;

            GameObject levelObjectGameObject = Object.Instantiate(prefab, position, new Quaternion(), levelContainerStatus.LevelContainer);
            levelObjectGameObject.transform.localScale = offGridlevelObjectSaveData.Size;

            if(levelEditorStatus.Active) {
                offGridLevelObjectsStatus.OffGridLevelObjectsByGameObject.Add(levelObjectGameObject, levelObjectType);
            }
        }
    }

}
