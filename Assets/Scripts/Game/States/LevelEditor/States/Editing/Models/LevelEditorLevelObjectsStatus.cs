using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorLevelObjectsStatus {

    public Dictionary<GameObject, LevelObjectType> LevelObjectTypesByGameObject = new Dictionary<GameObject, LevelObjectType>();

    public GameObject InstantiateLevelObject(GenerateableLevelObjectNode generateableLevelObjectNode, Vector2 position, IContext context) {
        GameObject prefab = generateableLevelObjectNode.Prefab;
        View view = prefab.GetComponent<View>();

        GameObject levelObjectGameObject;

        if (view != null) {
            levelObjectGameObject = context.InstantiateView(view).gameObject;
            levelObjectGameObject.transform.position = position;
        } else {
            levelObjectGameObject = Object.Instantiate(generateableLevelObjectNode.Prefab, position, new Quaternion());
        }

        Collider2D levelObjectCollider = levelObjectGameObject.GetComponent<Collider2D>();
        if(levelObjectCollider != null) {
            levelObjectCollider.isTrigger = false;
        }

        levelObjectGameObject.layer = LayerMask.NameToLayer(Layers.LevelEditorLevelObject);

        return levelObjectGameObject;
    }

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

