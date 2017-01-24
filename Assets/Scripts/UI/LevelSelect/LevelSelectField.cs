using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectField : MonoBehaviour {

    [SerializeField]
    private LevelFields[] levelFields;

    [SerializeField]
    private Vector2 borderSize;

    [System.Serializable]
    public struct LevelFields
    {
        public GameObject ObjectToBuild;
        public Vector2 FieldSize;
        public Vector2 LevelStartDirection;
        public Vector2 BuildPosition;
        public bool IsHorizontal;
        public int StartCountingValue;
        public bool spiralCounting;
    }

    private void Start()
    {
        DestroyChildren();
        BuildLevelSelectFields();
    }

    public void BuildLevelSelectFields()
    {
        FieldGenerator rowGenerator = GetComponent<FieldGenerator>();

        for (int i = 0; i < levelFields.Length; i++)
        {
            List<FieldGenerator.Node> nodes = rowGenerator.GenerateField(levelFields[i].FieldSize, levelFields[i].ObjectToBuild.transform.localScale, borderSize, levelFields[i].LevelStartDirection, levelFields[i].spiralCounting, levelFields[i].StartCountingValue, levelFields[i].IsHorizontal);

            nodes.ForEach(node => { MakeLevelNode(levelFields[i].ObjectToBuild, gameObject, levelFields[i].BuildPosition, node.Position, node.Counter); });
        }
    }

    /// <summary>
    /// Makes a node a node on the specified position.
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="counter"></param>
    /// <param name="parent"></param>
    /// <param name="buildPosition"></param>
    /// <param name="objectToBuild"></param>
    private void MakeLevelNode(GameObject objectToBuild, GameObject parent, Vector2 buildPosition, Vector2 offset, int counter)
    {
        GameObject node = Instantiate(objectToBuild, buildPosition + offset, new Quaternion(0, 0, 0, 0));
        node.transform.parent = parent.transform;
        node.GetComponentInChildren<TextMesh>().text = counter.ToString();
    }

    private void DestroyChildren()
    {
        var children = new List<GameObject>();
        foreach (Transform child in transform)
            children.Add(child.gameObject);
        children.ForEach(Destroy);
    }

    public void DestroyImmediateChildren()
    {
        var children = new List<GameObject>();
        foreach (Transform child in transform)
            children.Add(child.gameObject);
        children.ForEach(DestroyImmediate);
    }
}
