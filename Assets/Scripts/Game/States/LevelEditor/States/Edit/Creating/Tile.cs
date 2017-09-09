using System;
using UnityEngine;

[Serializable]
public class Tile {

    public Vector2 Forward { get { return GameObject.transform.forward; } set { GameObject.transform.forward = value; } }

    public TileType TileType;
    public GameObject GameObject;
    public bool UserGenerated;

    public void SetDirection(Vector2 direction) {
        float angle = Mathf.Atan2(direction.y, direction.y) * Mathf.Rad2Deg;
        GameObject.transform.rotation = Quaternion.AngleAxis(angle - 45, Vector3.forward);
    }

    public void SetAngle(float angle) {
        GameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Destroy() {
        if(GameObject != null) {
            ObjectPool.Instance.PoolObject(GameObject);
        }
    }

}