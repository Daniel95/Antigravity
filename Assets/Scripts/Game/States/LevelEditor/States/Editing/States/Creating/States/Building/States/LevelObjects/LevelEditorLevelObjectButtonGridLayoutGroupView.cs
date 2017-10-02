using IoCPlus;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class LevelEditorLevelObjectButtonGridLayoutGroupView : View {

    private const string LEVEL_OBJECT_PREFAB_PATH = "UI/LevelEditor/Editing/Creating/Building/LevelObjects/LevelObjectButtonUI";

    private void Awake() {
        InstantiateLevelObjectButtons();
    }

    private void InstantiateLevelObjectButtons() {
        GameObject levelObjectButtonPrefab = Resources.Load<GameObject>(LEVEL_OBJECT_PREFAB_PATH);

        if(levelObjectButtonPrefab == null) {
            Debug.LogWarning("Can't find levelObjectButtonPrefab at path " + LEVEL_OBJECT_PREFAB_PATH);
            return;
        }

        foreach (GenerateableLevelObjectNode levelObjectEditorNode in GenerateableLevelObjectLibrary.GenerateableLevelObjectNodes) {
            GameObject levelObjectButtonGameObject = Instantiate(levelObjectButtonPrefab.transform, transform).gameObject;
            LevelEditorLevelObjectButton levelObjectButton = levelObjectButtonGameObject.GetComponent<LevelEditorLevelObjectButton>();
            levelObjectButton.SetLevelObject(levelObjectEditorNode.LevelObjectType);
        }
    }

}
