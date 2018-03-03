using UnityEngine;

public class LevelEditorGridNodeSizeLibrary : MonoBehaviour {

    public static LevelEditorGridNodeSizeLibrary Instance { get { return GetInstance(); } }

    private static LevelEditorGridNodeSizeLibrary instance;

    public float NodeSize { get { return size; } }

    [SerializeField] private float size;

    private const string GRID_NODE_SIZE_PREFAB_PATH = "LevelEditor/Libraries/GridNodeSizeLibrary";

    public static LevelEditorGridNodeSizeLibrary GetInstance() {
        if (instance == null) {
            instance = Resources.Load<LevelEditorGridNodeSizeLibrary>(GRID_NODE_SIZE_PREFAB_PATH);
        }
        return instance;
    }

}
