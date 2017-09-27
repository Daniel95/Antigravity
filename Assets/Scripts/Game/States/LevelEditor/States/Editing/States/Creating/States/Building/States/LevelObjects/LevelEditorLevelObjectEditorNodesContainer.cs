using System.Collections.Generic;
using UnityEngine;

public class LevelEditorLevelObjectEditorNodesContainer : MonoBehaviour {

    public static LevelEditorLevelObjectEditorNodesContainer Instance { get { return GetInstance(); } }

    public List<LevelEditorLevelObjectEditorNode> LevelObjectEditorNodes { get { return levelObjectEditorNodes; } }

    private static LevelEditorLevelObjectEditorNodesContainer instance;

    [SerializeField] private List<LevelEditorLevelObjectEditorNode> levelObjectEditorNodes;

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
