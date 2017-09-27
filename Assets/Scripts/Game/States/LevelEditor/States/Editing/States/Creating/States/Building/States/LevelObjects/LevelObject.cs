using System;
using UnityEngine;

[Serializable]
public class LevelObject {

    public GameObject GameObject;
    public Transform Transform;
    public LevelObjectType LevelObjectType;

    public void Destroy() {
        if (GameObject != null) {
            ObjectPool.Instance.PoolObject(GameObject); 
        }
    }
}
