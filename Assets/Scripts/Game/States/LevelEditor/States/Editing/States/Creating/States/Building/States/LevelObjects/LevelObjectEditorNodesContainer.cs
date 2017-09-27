using System.Collections.Generic;
using UnityEngine;

public class LevelObjectEditorNodesContainer : MonoBehaviour {

    public static LevelObjectEditorNodesContainer Instance { get { return GetInstance(); } }

    public List<LevelObjectEditorNode> LevelObjectEditorNodes { get { return levelObjectEditorNodes; } }

    private static LevelObjectEditorNodesContainer instance;

    [SerializeField] private List<LevelObjectEditorNode> levelObjectEditorNodes;

    public LevelObjectEditorNode GetLevelObjectEditorNode(LevelObjectType levelObjectType) {
        LevelObjectEditorNode levelObjectEditorNode = levelObjectEditorNodes.Find(x => x.LevelObjectType == levelObjectType);
        return levelObjectEditorNode;
    }

    private static LevelObjectEditorNodesContainer GetInstance() {
        if(instance == null) {
            instance = Object.FindObjectOfType<LevelObjectEditorNodesContainer>();
        }
        return instance;
    }

}
