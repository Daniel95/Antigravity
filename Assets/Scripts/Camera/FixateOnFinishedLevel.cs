using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixateOnFinishedLevel : MonoBehaviour {

    private LevelSelectField levelSelectField;

    private void Awake()
    {
        levelSelectField = FindObjectOfType<LevelSelectField>();

        levelSelectField.LevelFinished += FixOnFinishedLevel;
    }

    private void OnDisable()
    {
        levelSelectField.LevelFinished -= FixOnFinishedLevel;
    }

    private void FixOnFinishedLevel(LevelNode levelNode)
    {
        transform.position = new Vector3(levelNode.transform.position.x, levelNode.transform.position.y, transform.position.z);
    }
}
