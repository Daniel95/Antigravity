using IoCPlus;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class LevelEditorLevelObjectButtonGridLayoutGroupView : View {

    private const string LEVEL_OBJECT_PREFAB_PATH = "UI/LevelEditor/Editing/Creating/Building/Objects/LevelObjectEditorNodeButtonUI";

    private GridLayoutGroup gridLayoutGroup;


    private void Awake() {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        InstantiateLevelObjectButtons();
    }

    private void InstantiateLevelObjectButtons() {
        GameObject levelObjectButtonPrefab = Resources.Load<GameObject>(LEVEL_OBJECT_PREFAB_PATH);
        foreach (LevelObjectEditorNode levelObjectEditorNode in LevelObjectEditorNodesContainer.Instance.LevelObjectEditorNodes) {
            GameObject levelObjectButtonGameObject = Instantiate(levelObjectButtonPrefab.transform, transform).gameObject;
            LevelObjectButton levelObjectButton = levelObjectButtonGameObject.GetComponent<LevelObjectButton>();
            levelObjectButton.Initiate(levelObjectEditorNode.LevelObjectType);
        }
    }

}
