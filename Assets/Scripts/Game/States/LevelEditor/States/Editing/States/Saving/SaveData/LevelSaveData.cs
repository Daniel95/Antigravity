using System.Collections.Generic;
using UnityEngine;

public class LevelSaveData {

    public List<Vector2> StandardTileGridPositions;
    public List<TileSaveData> NonStandardNonUserGeneratedTilesSaveData;
    public List<TileSaveData> NonStandardUserGeneratedTilesSaveData;
    public List<OnGridLevelObjectSaveData> OnGridLevelObjectsSaveData;
    public List<OffGridLevelObjectSaveData> OffGridLevelObjectsSaveData;

}
