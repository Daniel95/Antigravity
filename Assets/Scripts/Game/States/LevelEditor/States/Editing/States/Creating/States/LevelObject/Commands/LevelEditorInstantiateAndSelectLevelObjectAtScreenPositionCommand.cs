﻿using IoCPlus;
using UnityEngine;

public class LevelEditorInstantiateAndSelectLevelObjectAtScreenPositionCommand : Command {

    [Inject] private LevelEditorLevelObjectsStatus levelEditorLevelObjectsStatus;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        GenerateableLevelObjectNode generateableLevelObjectNode = LevelEditorSelectedLevelObjectNodeStatus.LevelObjectNode;
        GameObject levelObjectGameObject = Object.Instantiate(generateableLevelObjectNode.Prefab, worldPosition, new Quaternion());
        LevelEditorSelectedLevelObjectStatus.LevelObject = levelObjectGameObject;

        LevelObjectType levelObjectType = LevelEditorSelectedLevelObjectNodeStatus.LevelObjectNode.LevelObjectType;
        levelEditorLevelObjectsStatus.LevelObjectsByGameObject.Add(levelObjectGameObject, levelObjectType);
    }

}
