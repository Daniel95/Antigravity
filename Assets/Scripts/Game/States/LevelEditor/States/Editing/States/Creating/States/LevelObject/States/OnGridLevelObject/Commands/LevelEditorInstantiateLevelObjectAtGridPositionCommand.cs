using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorInstantiateLevelObjectAtGridPositionCommand : Command {

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        GenerateableLevelObjectNode generateableLevelObjectNode = LevelEditorSelectedLevelObjectNodeViewStatus.LevelObjectNode;
        LevelObjectType levelObjectType = generateableLevelObjectNode.LevelObjectType;
        Vector2 gridSize = GenerateableLevelObjectLibrary.GetLevelObjectEditorNodeGridSize(levelObjectType);

        Vector2 levelObjectSectionStartBuildPoint = VectorHelper.Decrement(gridPosition, VectorHelper.Floor(gridSize / 2));

        List<Vector2> levelObjectSectionGridPositions = new List<Vector2>();
        for (int y = 0; y < gridSize.y; y++) {
            for (int x = 0; x < gridSize.x; x++) {
                Vector2 levelObjectSectionGridPosition = levelObjectSectionStartBuildPoint + new Vector2(x, y);
                if(LevelEditorLevelObjectSectionGrid.Instance.Contains(levelObjectSectionGridPosition)) {
                    Abort();
                    return;
                }

                levelObjectSectionGridPositions.Add(levelObjectSectionGridPosition);
            }
        }

        Vector2 gameObjectPosition = LevelEditorGridHelper.GridToNodePosition(gridPosition);
        GameObject levelObjectGameObject = Object.Instantiate(generateableLevelObjectNode.Prefab, gameObjectPosition, new Quaternion());

        LevelObject levelObject = new LevelObject();
        levelObject.Initiate(levelObjectSectionGridPositions, levelObjectGameObject, levelObjectType);
    }

}
