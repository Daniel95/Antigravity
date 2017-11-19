﻿using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorDestroyCollisionWithGridLevelObjectsInSelectionFieldCommand : Command {

    [Inject] private Refs<ILevelObject> levelObjectRefs;

    protected override void Execute() {
        Vector2 selectionFieldStartPosition = LevelEditorSelectionFieldStatusView.SelectionFieldStartGridPosition;
        Vector2 selectionFieldEndPosition = LevelEditorSelectionFieldStatusView.SelectionFieldEndGridPosition;

        List<ILevelObject> levelObjectsToRemove = new List<ILevelObject>();
        foreach (ILevelObject levelObject in levelObjectRefs) {
            GenerateableLevelObjectNode generateableLevelObjectNode = GenerateableLevelObjectLibrary.GetNode(levelObject.LevelObjectType);
            if (!generateableLevelObjectNode.CanCollideWithTiles) { continue; }

            GameObject levelObjectGameObject = levelObject.GameObject;
            Vector2 levelObjectGridPosition = LevelEditorGridHelper.WorldToGridPosition(levelObjectGameObject.transform.position);

            if (!levelObjectGridPosition.IsBetweenVectors(selectionFieldStartPosition, selectionFieldEndPosition)) { continue; }

            levelObjectsToRemove.Add(levelObject);
        }

        foreach (ILevelObject levelObject in levelObjectsToRemove) {
            levelObject.DestroyLevelObject();
        }
    }

}
