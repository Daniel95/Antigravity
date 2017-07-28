using System;
using UnityEngine;

[Serializable]
public class Tile {

    public Vector2 Forward { get { return GameObject.transform.forward; } set { GameObject.transform.forward = value; } }

    public TileType Type;
    public GameObject GameObject;

    public void Destroy() {
        GameObject.DestroyImmediate(GameObject);
    }

}