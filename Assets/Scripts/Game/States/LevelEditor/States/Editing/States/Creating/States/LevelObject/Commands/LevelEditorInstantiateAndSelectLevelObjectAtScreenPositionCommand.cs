using IoCPlus;
using UnityEngine;

public class LevelEditorInstantiateAndSelectLevelObjectAtScreenPositionCommand : Command {

    [Inject] IContext context;

    [Inject] private LevelEditorLevelObjectsStatus levelEditorLevelObjectsStatus;

    [InjectParameter] private Vector2 screenPosition;

    protected override void Execute() {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        GenerateableLevelObjectNode generateableLevelObjectNode = LevelEditorSelectedLevelObjectNodeStatus.LevelObjectNode;

        GameObject prefab = generateableLevelObjectNode.Prefab;
        View view = prefab.GetComponent<View>();

        GameObject levelObjectGameObject;

        if (view != null) {
            levelObjectGameObject = context.InstantiateView(view).gameObject;
            levelObjectGameObject.transform.position = worldPosition;
        } else {
            levelObjectGameObject = Object.Instantiate(generateableLevelObjectNode.Prefab, worldPosition, new Quaternion());
        }

        LevelEditorSelectedLevelObjectStatus.LevelObject = levelObjectGameObject;

        LevelObjectType levelObjectType = LevelEditorSelectedLevelObjectNodeStatus.LevelObjectNode.LevelObjectType;
        levelEditorLevelObjectsStatus.LevelObjectTypesByGameObject.Add(levelObjectGameObject, levelObjectType);
    }

}
