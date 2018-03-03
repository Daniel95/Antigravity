using System;
using System.Collections.Generic;
using UnityEngine;

public class GridSnapSizeLibrary : MonoBehaviour {

    public static GridSnapSizeLibrary Instance { get { return GetInstance(); } }

    public List<GridSnapSize> GridSnapSizes { get { return gridSnapSizes; } }

    private static GridSnapSizeLibrary instance;

    private const string GRID_SNAP_SIZE_LIBRARY_PATH = "LevelEditor/Libraries/GridSnapSizeLibrary";

    [SerializeField] private List<GridSnapSize> gridSnapSizes;

    public Vector2 GetGridSnapSize(GridSnapSizeType gridSnapSizeType) {
        Vector2 gridSnapSize = gridSnapSizes.Find(x => x.Type == gridSnapSizeType).Size;
        return gridSnapSize;
    }

    private static GridSnapSizeLibrary GetInstance() {
        if(instance == null) {
            instance = Resources.Load<GridSnapSizeLibrary>(GRID_SNAP_SIZE_LIBRARY_PATH);
        }
        return instance;
    }

}

[Serializable]
public class GridSnapSize {

    public Vector2 Size;
    public GridSnapSizeType Type;

}
