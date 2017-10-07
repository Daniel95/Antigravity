using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GenerateableLevelObjectNode {

    public GameObject Prefab;
    public bool IsSolid = true; 
    public LevelObjectType LevelObjectType;
    public List<LevelObjectTransformType> AvailableInputTypes = new List<LevelObjectTransformType>() {
        LevelObjectTransformType.Translate,
    };

    public LevelObjectTransformType GetDefaultLevelObjectInputType() {
        return AvailableInputTypes[0];
    }

}
