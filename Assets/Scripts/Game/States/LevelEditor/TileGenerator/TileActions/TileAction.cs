using System;
using UnityEngine;

[Serializable]
public abstract class TileAction : MonoBehaviour {

    public abstract void Do(Vector2 gridPosition);

}
