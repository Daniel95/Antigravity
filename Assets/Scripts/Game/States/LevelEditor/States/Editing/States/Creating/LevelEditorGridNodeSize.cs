using UnityEngine;

public class LevelEditorGridNodeSize : MonoBehaviour {

    public static LevelEditorGridNodeSize Instance { get { return GetInstance(); } }

    private static LevelEditorGridNodeSize instance;

    public float Size { get { return size; } }

    [SerializeField] private float size;

    public static LevelEditorGridNodeSize GetInstance() {
        if (instance == null) {
            instance = Object.FindObjectOfType<LevelEditorGridNodeSize>();
        }
        return instance;
    }

}
