using IoCPlus;
using UnityEngine;

public class InstantiateSelectedLevelObjectAtScreenPositionCommand : Command {

    [Inject] private LevelEditorSelectedLevelObjectStatus selectedLevelObjectStatus;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        LevelObjectType levelObjectType = selectedLevelObjectStatus.levelObjectType;
        LevelEditorLevelObjectEditorNode levelEditorLevelObjectEditorNode = LevelEditorLevelObjectEditorNodesContainer.Instance.GetNode(levelObjectType);

        float gridNodeSize = LevelEditorGridNodeSize.Instance.NodeSize;
        Vector2 nodePosition = GridHelper.ScreenToNodePosition(screenPosition, gridNodeSize);

        GameObject levelObjectGameObject = Object.Instantiate(levelEditorLevelObjectEditorNode.Prefab, nodePosition, new Quaternion());
        LevelObject levelObject = new LevelObject {
            GameObject = levelObjectGameObject,
            LevelObjectType = levelObjectType,
        };

        LevelEditorLevelObjectGrid.Instance.AddLevelObject(nodePosition, levelObject);
    }

}
