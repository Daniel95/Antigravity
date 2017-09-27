using IoCPlus;
using UnityEngine;

public class InstantiateSelectedLevelObjectAtScreenPositionCommand : Command {

    [Inject] private LevelEditorSelectedLevelObjectStatus selectedLevelObjectStatus;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        LevelObjectType levelObjectType = selectedLevelObjectStatus.levelObjectType;
        LevelEditorLevelObjectEditorNode levelEditorLevelObjectEditorNode = LevelEditorLevelObjectEditorNodesContainer.Instance.GetNode(levelObjectType);

        Vector2 worldPostion = Camera.main.ScreenToWorldPoint(screenPosition);
        float gridNodeSize = LevelEditorGridNodeSize.Instance.Size;
        Vector2 gridPosition = GridHelper.WorldToGridPosition(worldPostion, gridNodeSize);

        GameObject levelObjectGameObject = Object.Instantiate(levelEditorLevelObjectEditorNode.Prefab, gridPosition, new Quaternion());
        LevelObject levelObject = new LevelObject {
            GameObject = levelObjectGameObject,
            LevelObjectType = levelObjectType,
        };

        LevelEditorLevelObjectGrid.Instance.AddLevelObject(gridPosition, levelObject);
    }

}
