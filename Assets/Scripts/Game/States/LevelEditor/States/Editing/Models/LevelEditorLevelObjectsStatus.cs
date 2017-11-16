using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorLevelObjectsStatus {

    [Inject] IContext context;

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

        if (generateableLevelObjectNode.CanCollideWithLevelObjects) {
            Rigidbody2D rigidBody = levelObjectGameObject.AddComponent<Rigidbody2D>();
            rigidBody.isKinematic = true;

            CollisionHitDetectionView collisionHitDetectionView = levelObjectGameObject.AddComponent<CollisionHitDetectionView>();
            context.AddView(collisionHitDetectionView, false);

            TriggerHitDetectionView triggerHitDetectionView = levelObjectGameObject.AddComponent<TriggerHitDetectionView>();
            context.AddView(triggerHitDetectionView, false);
        } else if(generateableLevelObjectNode.CanCollideWithLevelObjects) {
            Rigidbody2D rigidBody = levelObjectGameObject.AddComponent<Rigidbody2D>();
            rigidBody.isKinematic = true;

            CollisionHitDetectionView collisionHitDetectionView = levelObjectGameObject.AddComponent<CollisionHitDetectionView>();
            context.AddView(collisionHitDetectionView, false);
        }

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

