using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileGeneratorNode {

    public GameObject Prefab;
    public TileType TileType;
    public GeneratePhase GeneratePhase;
    public List<TileCondition> TileConditions;

}
