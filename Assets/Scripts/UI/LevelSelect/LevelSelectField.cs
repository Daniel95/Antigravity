using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectField : MonoBehaviour {

    [SerializeField]
    private LevelFields[] levelFields;

    [System.Serializable]
    public struct LevelFields
    {
        public Vector2 Size;
        public Vector2 LevelStartDirection;
        public Vector2 BuildPosition;
        public bool IsHorizontal;
    }

    private void Start()
    {
        RowBuilder rowBuilder = GetComponent<RowBuilder>();

        for (int i = 0; i < levelFields.Length; i++)
        {
            rowBuilder.BuildRows(levelFields[i].BuildPosition, levelFields[i].Size, levelFields[i].LevelStartDirection, levelFields[i].IsHorizontal);
        }
    }
}
