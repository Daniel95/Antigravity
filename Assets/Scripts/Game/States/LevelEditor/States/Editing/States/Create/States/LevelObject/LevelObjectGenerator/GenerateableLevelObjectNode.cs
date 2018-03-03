using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GenerateableLevelObjectNode {

    public GameObject Prefab;
    public LevelObjectType LevelObjectType;
    public bool CanCollideWithTiles;
    public bool CanCollideWithLevelObjects;
    public List<LevelObjectTransformType> TransformTypes = new List<LevelObjectTransformType>() {
        LevelObjectTransformType.Translate,
    };

    public LevelObjectTransformType GetDefaultLevelObjectInputType() {
        return TransformTypes[0];
    }

}
