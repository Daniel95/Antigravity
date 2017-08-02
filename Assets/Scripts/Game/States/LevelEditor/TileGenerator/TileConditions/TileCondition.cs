using System;
using UnityEngine;

[Serializable]
public abstract class TileCondition : MonoBehaviour {

    public abstract bool Check(Vector2 gridPosition, GeneratePhase generatePhase);

}
