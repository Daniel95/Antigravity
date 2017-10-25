﻿using System.Collections.Generic;
using UnityEngine;

public class LevelSaveData {

    public List<Vector2> StandardTileGridPositions;
    public List<TileSaveData> NonStandardTilesSaveData;
    public List<OnGridLevelObjectSaveData> OnGridLevelObjectsSaveData;
    public List<OffGridLevelObjectSaveData> OffGridLevelObjectsSaveData;

}
