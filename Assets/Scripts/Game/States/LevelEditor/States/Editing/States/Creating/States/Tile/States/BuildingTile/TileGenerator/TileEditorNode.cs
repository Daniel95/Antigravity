using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GeneratableTileNode {

    public GameObject Prefab;
    public TileType TileType;
    public bool UserGenerated = true;
    public List<TileCondition> TileConditions;
    public List<TileAction> TileActions;

}
