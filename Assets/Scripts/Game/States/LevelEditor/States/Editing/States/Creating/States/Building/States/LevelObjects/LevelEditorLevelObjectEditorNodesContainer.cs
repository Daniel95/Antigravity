using System.Collections.Generic;
using UnityEngine;

public class LevelEditorLevelObjectEditorNodesContainer : MonoBehaviour {

    public static LevelEditorLevelObjectEditorNodesContainer Instance { get { return GetInstance(); } }

    public List<LevelEditorLevelObjectEditorNode> LevelObjectEditorNodes { get { return levelObjectEditorNodes; } }

    private static LevelEditorLevelObjectEditorNodesContainer instance;

    [SerializeField] private List<LevelEditorLevelObjectEditorNode> levelObjectEditorNodes;

    public Vector2 GetLevelObjectEditorNodeGridSize(LevelObjectType levelObjectType) {
        LevelEditorLevelObjectEditorNode levelEditorLevelObjectEditorNode = GetNode(levelObjectType);
        Vector2 nodeSize = levelEditorLevelObjectEditorNode.Prefab.transform.localScale;
        Vector2 unroundedGridSize = Vector2.one + (nodeSize / LevelEditorGridNodeSize.Instance.NodeSize);
        Vector2 gridSize = VectorHelper.Floor(unroundedGridSize);
        return gridSize;
    } 

    public LevelEditorLevelObjectEditorNode GetNode(LevelObjectType levelObjectType) {
        LevelEditorLevelObjectEditorNode levelObjectEditorNode = levelObjectEditorNodes.Find(x => x.LevelObjectType == levelObjectType);
        return levelObjectEditorNode;
    }

    private static LevelEditorLevelObjectEditorNodesContainer GetInstance() {
        if(instance == null) {
            instance = FindObjectOfType<LevelEditorLevelObjectEditorNodesContainer>();
        }
        return instance;
    }

}
