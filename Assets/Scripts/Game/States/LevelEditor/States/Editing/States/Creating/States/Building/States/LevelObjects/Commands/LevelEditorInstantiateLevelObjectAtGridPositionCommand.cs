using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorInstantiateLevelObjectAtGridPositionCommand : Command {

    [Inject] private LevelEditorSelectedLevelObjectNodeTypeStatus selectedLevelObjectStatus;
    [Inject] private LevelEditorSelectedLevelObjectSectionStatus selectedLevelObjectSectionStatus;

    [InjectParameter] private Vector2 gridPosition;

    protected override void Execute() {
        LevelObjectType levelObjectType = selectedLevelObjectStatus.LevelObjectType;
        LevelEditorLevelObjectEditorNode levelEditorLevelObjectEditorNode = LevelEditorLevelObjectEditorNodesContainer.Instance.GetNode(levelObjectType);

        Vector2 nodePosition = LevelEditorGridHelper.GridToNodePosition(gridPosition);

        GameObject levelObjectGameObject = Object.Instantiate(levelEditorLevelObjectEditorNode.Prefab, nodePosition, new Quaternion());
        Vector2 gridSize = LevelEditorLevelObjectEditorNodesContainer.Instance.GetLevelObjectEditorNodeGridSize(levelObjectType);

        LevelObject levelObject = new LevelObject {
            GameObject = levelObjectGameObject,
            GridSize = gridSize,
        };

        Vector2 levelObjectSectionStartBuildPoint = new Vector2(gridPosition.x - Mathf.FloorToInt(gridSize.x / 2), gridPosition.y - Mathf.FloorToInt(gridSize.y / 2));

        List<LevelObjectSection> levelObjectSections = new List<LevelObjectSection>();
        for (int y = 0; y < gridSize.y; y++) {
            for (int x = 0; x < gridSize.x; x++) {
                Vector2 levelObjectSectionGridPosition = levelObjectSectionStartBuildPoint + new Vector2(x, y);
                LevelObjectSection levelObjectSection = new LevelObjectSection();
                levelObjectSection.Initiate(levelObjectSectionGridPosition);
                levelObjectSections.Add(levelObjectSection);
            }
        }

        levelObject.Initiate(levelObjectSections);
    }

}
