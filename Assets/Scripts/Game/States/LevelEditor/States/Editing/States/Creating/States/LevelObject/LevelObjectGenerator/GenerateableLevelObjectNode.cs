using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GenerateableLevelObjectNode {

    public GameObject Prefab;
    public bool IsSolid = true; 
    public LevelObjectType LevelObjectType;
    public List<LevelObjectInputType> AvailableInputTypes = new List<LevelObjectInputType>() {
        LevelObjectInputType.Translate,
    };

    public LevelObjectInputType GetDefaultLevelObjectInputType() {
        return AvailableInputTypes[0];
    }

}
