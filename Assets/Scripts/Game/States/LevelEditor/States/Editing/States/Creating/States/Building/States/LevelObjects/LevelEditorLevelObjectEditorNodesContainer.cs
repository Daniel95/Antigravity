using System.Collections.Generic;
using UnityEngine;

public class LevelEditorLevelObjectEditorNodesContainer : MonoBehaviour {

    public static LevelEditorLevelObjectEditorNodesContainer Instance { get { return GetInstance(); } }

    public List<LevelEditorLevelObjectEditorNode> LevelObjectEditorNodes { get { return levelObjectEditorNodes; } }

    private static LevelEditorLevelObjectEditorNodesContainer instance;

    [SerializeField] private List<LevelEditorLevelObjectEditorNode> levelObjectEditorNodes;

    private void Awake() {
        bool sizeOfNodeIsZero = levelObjectEditorNodes.Find(x => x.Size == Vector2.zero) != null;
        if(sizeOfNodeIsZero) {
            Debug.LogWarning("LevelObjectEditorNodes size cannot be zero.");
        }
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
