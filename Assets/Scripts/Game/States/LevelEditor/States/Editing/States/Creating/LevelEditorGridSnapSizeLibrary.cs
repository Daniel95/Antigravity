using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorGridSnapSizeLibrary : MonoBehaviour {

    public static LevelEditorGridSnapSizeLibrary Instance { get { return GetInstance(); } }

    public List<GridSnapSize> GridSnapSizes { get { return gridSnapSizes; } }

    private static LevelEditorGridSnapSizeLibrary instance;

    private const string GRID_SNAP_SIZE_LIBRARY_PATH = "LevelEditor/Libraries/GridSnapSizeLibrary";

    [SerializeField] private List<GridSnapSize> gridSnapSizes;

    public Vector2 GetGridSnapSize(GridSnapSizeType gridSnapSizeType) {
        Vector2 gridSnapSize = gridSnapSizes.Find(x => x.Type == gridSnapSizeType).Size;
        return gridSnapSize;
    }

    private static LevelEditorGridSnapSizeLibrary GetInstance() {
        if(instance == null) {
            instance = Resources.Load<LevelEditorGridSnapSizeLibrary>(GRID_SNAP_SIZE_LIBRARY_PATH);
        }
        return instance;
    }

}

[Serializable]
public class GridSnapSize {

    public Vector2 Size;
    public GridSnapSizeType Type;

}
