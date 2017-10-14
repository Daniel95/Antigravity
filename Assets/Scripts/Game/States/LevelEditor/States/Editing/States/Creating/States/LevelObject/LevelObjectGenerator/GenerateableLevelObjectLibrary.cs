using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateableLevelObjectLibrary : MonoBehaviour {

    public static List<GenerateableLevelObjectNode> GenerateableLevelObjectNodes { get { return GetInstance().generateableLevelObjectNodes; } }

    private static GenerateableLevelObjectLibrary instance;

    [SerializeField] private List<GenerateableLevelObjectNode> generateableLevelObjectNodes;

    private const string GENERATABLE_LEVEL_OBJECT_LIBRARY_PATH = "LevelEditor/Libraries/GenerateableLevelObjectLibrary";

    public static Vector2 GetLevelObjectEditorNodeGridSize(LevelObjectType levelObjectType) {
        GenerateableLevelObjectNode levelEditorLevelObjectEditorNode = GetNode(levelObjectType);
        Vector2 nodeSize = levelEditorLevelObjectEditorNode.Prefab.transform.localScale;
        Vector2 unroundedGridSize = Vector2.one + (nodeSize / LevelEditorGridNodeSize.Instance.NodeSize);
        Vector2 gridSize = VectorHelper.Floor(unroundedGridSize);
        return gridSize;
    } 

    public static GenerateableLevelObjectNode GetNode(LevelObjectType levelObjectType) {
        GenerateableLevelObjectNode levelObjectEditorNode = GetInstance().generateableLevelObjectNodes.Find(x => x.LevelObjectType == levelObjectType);
        return levelObjectEditorNode;
    }

    public static GenerateableLevelObjectNode GetNode(string levelObjectInstanceName) {
        string levelObjectPrefabName = levelObjectInstanceName.Replace("(Clone)", "");
        GenerateableLevelObjectNode levelObjectEditorNode = GetInstance().generateableLevelObjectNodes.Find(x => x.Prefab.name == levelObjectPrefabName);
        return levelObjectEditorNode;
    }

    public static bool Contains(LevelObjectType levelObjectType) {
        bool containsLevelObjectEditorNode = GetInstance().generateableLevelObjectNodes.Exists(x => x.LevelObjectType == levelObjectType);
        return containsLevelObjectEditorNode;
    }

    public static bool Contains(string levelObjectInstanceName) {
        string levelObjectPrefabName = levelObjectInstanceName.Replace("(Clone)", "");
        bool containsLevelObjectEditorNode = GetInstance().generateableLevelObjectNodes.Exists(x => x.Prefab.name == levelObjectPrefabName);
        return containsLevelObjectEditorNode;
    }

    private static GenerateableLevelObjectLibrary GetInstance() {
        if(instance == null) {
            instance = Resources.Load<GenerateableLevelObjectLibrary>(GENERATABLE_LEVEL_OBJECT_LIBRARY_PATH);
        }
        return instance;
    }

}
