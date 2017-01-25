using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectField : MonoBehaviour {

    [SerializeField]
    private LevelFields[] levelFields;

    [SerializeField]
    private Vector2 borderSize;

    private readonly Dictionary<Vector2, LevelNode> _nodesInGrid = new Dictionary<Vector2, LevelNode>();

    [System.Serializable]
    public struct LevelFields
    {
        public GameObject ObjectToBuild;
        public Vector2 FieldPosition;
        public Vector2 FieldSize;
        public Vector2 LevelStartDirection;
        public bool IsHorizontal;
        public int StartCountingValue;
        public bool SpiralCounting;
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
            List<FieldGenerator.Node> nodes = rowGenerator.GenerateField(levelFields[i].FieldSize,  levelFields[i].LevelStartDirection, levelFields[i].SpiralCounting, levelFields[i].StartCountingValue, levelFields[i].IsHorizontal);

            nodes.ForEach(node =>
            {
                Vector2 offset = new Vector2(node.Position.x * levelFields[i].ObjectToBuild.transform.localScale.x, node.Position.y * levelFields[i].ObjectToBuild.transform.localScale.y);

                Vector2 position = new Vector2(levelFields[i].FieldPosition.x * levelFields[i].ObjectToBuild.transform.localScale.x, levelFields[i].FieldPosition.y * levelFields[i].ObjectToBuild.transform.localScale.y) + offset;

                _nodesInGrid.Add(position, new LevelNode(false, false));

                //add the borders
                position += new Vector2(position.x * borderSize.x, position.y * borderSize.y);

                MakeLevelNode(levelFields[i].ObjectToBuild, gameObject, position, node.Counter);
            });
        }
    }

    /// <summary>
    /// Makes a node a node on the specified position.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="counter"></param>
    /// <param name="parent"></param>
    /// <param name="objectToBuild"></param>
    private static void MakeLevelNode(GameObject objectToBuild, GameObject parent, Vector2 position, int counter)
    {
        GameObject node = Instantiate(objectToBuild, position, new Quaternion(0, 0, 0, 0));
        node.transform.parent = parent.transform;
        node.GetComponentInChildren<TextMesh>().text = counter.ToString();
    }

    private void DestroyChildren()
    {
        _nodesInGrid.Clear();

        var children = new List<GameObject>();
        foreach (Transform child in transform)
            children.Add(child.gameObject);
        children.ForEach(Destroy);
    }

    public void DestroyImmediateChildren()
    {
        _nodesInGrid.Clear();

        var children = new List<GameObject>();
        foreach (Transform child in transform)
            children.Add(child.gameObject);
        children.ForEach(DestroyImmediate);
    }

    public struct LevelNode
    {
        public bool Unlocked;
        public bool Finished;

        public LevelNode(bool unlocked, bool finished)
        {
            this.Unlocked = unlocked;
            this.Finished = finished;
        }
    }

    public Dictionary<Vector2, LevelNode> NodesInGrid
    {
        get { return NodesInGrid; }
    }
}
