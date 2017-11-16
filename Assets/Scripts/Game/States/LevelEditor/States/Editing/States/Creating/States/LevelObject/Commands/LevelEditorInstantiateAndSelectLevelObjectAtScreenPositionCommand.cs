using IoCPlus;
using UnityEngine;

public class LevelEditorInstantiateAndSelectLevelObjectAtScreenPositionCommand : Command {

    [Inject] IContext context;

    [Inject] private LevelEditorLevelObjectsStatus levelEditorLevelObjectsStatus;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        GenerateableLevelObjectNode generateableLevelObjectNode = LevelEditorSelectedLevelObjectNodeStatus.LevelObjectNode;

        GameObject levelObjectGameObject = levelEditorLevelObjectsStatus.InstantiateLevelObject(generateableLevelObjectNode, worldPosition, context);

        LevelEditorSelectedLevelObjectStatus.LevelObject = levelObjectGameObject;

        LevelObjectType levelObjectType = LevelEditorSelectedLevelObjectNodeStatus.LevelObjectNode.LevelObjectType;
        levelEditorLevelObjectsStatus.LevelObjectTypesByGameObject.Add(levelObjectGameObject, levelObjectType);
    }

}
