using IoCPlus;
using UnityEngine;

public class LevelEditorInstantiateAndSelectLevelObjectAtScreenPositionCommand : Command {

    [Inject] IContext context;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        GenerateableLevelObjectNode generateableLevelObjectNode = LevelEditorSelectedLevelObjectNodeStatus.LevelObjectNode;
        GameObject levelObjectGameObject = LevelObjectHelper.InstantiateLevelObject(generateableLevelObjectNode, worldPosition, context);

        ILevelObject levelObject = levelObjectGameObject.GetComponent<ILevelObject>();
        levelObject.Select();
    }

}
