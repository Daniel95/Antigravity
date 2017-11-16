using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorDestroyCollisionWithGridLevelObjectsInSelectionFieldCommand : Command {

    [Inject] private LevelEditorLevelObjectsStatus levelEditorLevelObjectsStatus;

    protected override void Execute() {
        Vector2 selectionFieldStartPosition = LevelEditorSelectionFieldStatusView.SelectionFieldStartGridPosition;
        Vector2 selectionFieldEndPosition = LevelEditorSelectionFieldStatusView.SelectionFieldEndGridPosition;

        List<GameObject> levelObjectsToRemove = new List<GameObject>();
        foreach (KeyValuePair<GameObject, LevelObjectType> levelObjectTypeByGameObject in levelEditorLevelObjectsStatus.LevelObjectTypesByGameObject) {
            if (!GenerateableLevelObjectLibrary.GetNode(levelObjectTypeByGameObject.Value).CanCollideWithTiles) { continue; }

            GameObject levelObject = levelObjectTypeByGameObject.Key;
            Vector2 levelObjectGridPosition = LevelEditorGridHelper.WorldToGridPosition(levelObject.transform.position);

            if (!levelObjectGridPosition.IsBetweenVectors(selectionFieldStartPosition, selectionFieldEndPosition)) { continue; }

            levelObjectsToRemove.Add(levelObject);
        }

        foreach (GameObject levelObject in levelObjectsToRemove) {
            levelEditorLevelObjectsStatus.DestroyLevelObject(levelObject);
        }
    }

}
