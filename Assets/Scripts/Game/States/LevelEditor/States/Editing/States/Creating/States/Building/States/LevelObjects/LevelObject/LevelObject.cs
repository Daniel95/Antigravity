using System;
using UnityEngine;

[Serializable]
public class LevelObject {

    public Transform Transform { get { return GameObject.transform; } }

    public GameObject GameObject;
    public LevelObjectType LevelObjectType;

    public void Destroy() {
        if (GameObject != null) {
            ObjectPool.Instance.PoolObject(GameObject); 
        }
    }
}
