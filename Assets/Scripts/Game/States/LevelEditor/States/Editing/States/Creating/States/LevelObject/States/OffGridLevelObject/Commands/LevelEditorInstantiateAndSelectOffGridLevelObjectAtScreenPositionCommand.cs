using IoCPlus;
using UnityEngine;

public class LevelEditorInstantiateAndSelectOffGridLevelObjectAtScreenPositionCommand : Command {

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        GenerateableLevelObjectNode generateableLevelObjectNode = LevelEditorSelectedLevelObjectNodeViewStatus.LevelObjectNode;
        GameObject offGridlevelObjectGameObject = Object.Instantiate(generateableLevelObjectNode.Prefab, worldPosition, new Quaternion());
        LevelEditorSelectedOffGridLevelObjectStatus.OffGridLevelObject = offGridlevelObjectGameObject;
    }

}
