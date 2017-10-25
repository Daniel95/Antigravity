using UnityEngine;

public class LevelEditorGridNodeSize : MonoBehaviour {

    public static LevelEditorGridNodeSize Instance { get { return GetInstance(); } }

    private static LevelEditorGridNodeSize instance;

    public float NodeSize { get { return size; } }

    [SerializeField] private float size;

    public static LevelEditorGridNodeSize GetInstance() {
        if (instance == null) {
            instance = FindObjectOfType<LevelEditorGridNodeSize>();
        }
        return instance;
    }

}
