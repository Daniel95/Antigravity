using UnityEngine;

public class LevelEditorGridNodeSize : MonoBehaviour {

    public static LevelEditorGridNodeSize Instance { get { return GetInstance(); } }

    private static LevelEditorGridNodeSize instance;

    public float NodeSize { get { return size; } }

    [SerializeField] private float size;

    private const string GRID_NODE_SIZE_PREFAB_PATH = "LevelEditor/Libraries/GridNodeSizeLibrary";

    public static LevelEditorGridNodeSize GetInstance() {
        if (instance == null) {
            instance = Resources.Load<LevelEditorGridNodeSize>(GRID_NODE_SIZE_PREFAB_PATH);
        }
        return instance;
    }

}
