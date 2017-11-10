using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorLevelObjectsStatus {

    public Dictionary<GameObject, LevelObjectType> LevelObjectTypesByGameObject = new Dictionary<GameObject, LevelObjectType>();

    public void DestroyLevelObject(GameObject levelObject) {
        View view = levelObject.GetComponent<View>();

        if (view != null) {
            view.Destroy();
        } else {
            Object.Destroy(levelObject);
        }

        LevelObjectTypesByGameObject.Remove(levelObject);
    }

    public void DestroyAllLevelObjects() {
        foreach (GameObject levelObject in LevelObjectTypesByGameObject.Keys) {
            View view = levelObject.GetComponent<View>();

            if (view != null) {
                view.Destroy();
            } else {
                Object.Destroy(levelObject);
            }

        }

        LevelObjectTypesByGameObject.Clear();
    }

}

