using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevelObjectsCommand : Command {

    [Inject] private DeserializedLevelSaveDataStatus deserializedLevelSaveDataStatus;
    [Inject] private LevelContainerTransformStatus levelContainerStatus;
    [Inject] private LevelEditorStatus levelEditorStatus;
    [Inject] private LevelEditorLevelObjectsStatus levelObjectsStatus;

    protected override void Execute() {
        LevelSaveData levelSaveData = deserializedLevelSaveDataStatus.LevelSaveData;

        List<LevelObjectSaveData> levelObjectsSaveData = levelSaveData.LevelObjectsSaveData;
        SpawnLevelObjects(levelObjectsSaveData);
    }

    private void SpawnLevelObjects(List<LevelObjectSaveData> levelObjectsSaveData) {
        foreach (LevelObjectSaveData levelObjectSaveData in levelObjectsSaveData) {
            LevelObjectType levelObjectType = levelObjectSaveData.LevelObjectType;
            GenerateableLevelObjectNode levelEditorLevelObjectEditorNode = GenerateableLevelObjectLibrary.GetNode(levelObjectType);
            GameObject prefab = levelEditorLevelObjectEditorNode.Prefab;
            Vector2 position = levelObjectSaveData.Position;

            GameObject levelObjectGameObject = Object.Instantiate(prefab, position, new Quaternion(), levelContainerStatus.LevelContainer);
            levelObjectGameObject.transform.localScale = levelObjectSaveData.Size;

            if (levelEditorStatus.Active) {
                levelObjectsStatus.LevelObjectTypesByGameObject.Add(levelObjectGameObject, levelObjectType);
            }
        }
    }
}
