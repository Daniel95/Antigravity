using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectField : MonoBehaviour {

    [SerializeField]
    private LevelFields[] levelFields;

    [SerializeField]
    private Vector2 borderSize;

    [SerializeField]
    private bool levelsUnlocked;

    private readonly Dictionary<Vector2, LevelNode> _levelNodes = new Dictionary<Vector2, LevelNode>();

    public Action<LevelNode> LevelFinished;

    [Serializable]
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

        BuildFields();
    }

    public void BuildFields()
    {
        GenerateLevelSelectFields();
        UnlockFirstLevel();
        CheckLevelFinished();

        //For debugging
        if (levelsUnlocked)
            UnlockAllLevels();

        ActivateLevelNodes();
    }

    /// <summary>
    /// For each levelFields uses FieldGenerator to generate a field of nodes, places them in the right position with considering the scale and border.
    /// Also adds it to the dictionary _nodesInGrid, and finally spawns the chosen object.
    /// </summary>
    public void GenerateLevelSelectFields()
    {
        FieldGenerator fieldGenerator = GetComponent<FieldGenerator>();

        for (int i = 0; i < levelFields.Length; i++)
        {
            //Generate the rows
            List<FieldGenerator.Node> nodes = fieldGenerator.GenerateField(levelFields[i].FieldSize, levelFields[i].LevelStartDirection, levelFields[i].SpiralCounting, levelFields[i].StartCountingValue, levelFields[i].IsHorizontal);

            nodes.ForEach(node =>
            {
                Vector2 gridPosition = node.Position + levelFields[i].FieldPosition;

                Vector2 offset = new Vector2(node.Position.x * levelFields[i].ObjectToBuild.transform.localScale.x, node.Position.y * levelFields[i].ObjectToBuild.transform.localScale.y);

                Vector2 scaledPosition = new Vector2(levelFields[i].FieldPosition.x * levelFields[i].ObjectToBuild.transform.localScale.x, levelFields[i].FieldPosition.y * levelFields[i].ObjectToBuild.transform.localScale.y) + offset;

                Vector2 worldPosition = scaledPosition + new Vector2(scaledPosition.x * borderSize.x, scaledPosition.y * borderSize.y);

                LevelNodeStatus status = (LevelNodeStatus)LevelStatusPlayerPrefs.GetLevelStatus(node.Counter);

                LevelNode levelNode = GenerateLevelNode(levelFields[i].ObjectToBuild, gameObject, worldPosition, node.Counter, status);

                _levelNodes.Add(gridPosition, levelNode);
            });
        }
    }

    /// <summary>
    /// Makes a node a node on the specified position.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="counter"></param>
    /// <param name="parent"></param>
    /// <param name="objectToBuild"></param>
    /// <param name="status"></param>
    private static LevelNode GenerateLevelNode(GameObject objectToBuild, GameObject parent, Vector2 worldPosition, int counter, LevelNodeStatus status)
    {
        GameObject node = Instantiate(objectToBuild, worldPosition, new Quaternion(0, 0, 0, 0));
        node.transform.parent = parent.transform;
        node.GetComponentInChildren<TextMesh>().text = counter.ToString();

        LevelNode levelNode = node.GetComponent<LevelNode>();
        levelNode.GetLevelNodeValues(status, counter);

        return levelNode;
    }

    private void UnlockFirstLevel()
    {
        //first level starts unlocked if it was locked
        if (LevelStatusPlayerPrefs.GetLevelStatus(1) != 0)
            return;

        LevelStatusPlayerPrefs.SetLevelStatus(1, 1);

        GetLevelNodesDataByNumber(1).Value.Status = LevelNodeStatus.Unlocked;
    }

    /// <summary>
    /// Checks if we finished a level, activate the SetLevelFinished method.
    /// </summary>
    private void CheckLevelFinished()
    {
        GameObject keeperGameObject = GameObject.FindGameObjectWithTag(Tags.LevelFinishedKeeper);

        if (keeperGameObject)
        {
            FinishedLevelKeeper finishedLevelKeeper = keeperGameObject.GetComponent<FinishedLevelKeeper>();

            LevelLoader levelLoader = GetComponent<LevelLoader>();

            if (levelLoader.LevelNames.Contains(finishedLevelKeeper.LevelName))
            {
                int levelNumber = levelLoader.LevelNames.FindIndex(lvlName => lvlName == finishedLevelKeeper.LevelName) + 1;

                SetLevelFinished(levelNumber);
            }
        }

        LevelStatusPlayerPrefs.SaveLevelStatuses();
    }

    private void ActivateLevelNodes()
    {
        foreach (var node in _levelNodes)
        {
            node.Value.ActivateLevelNode();
        }
    }

    private void SetLevelFinished(int levelNumber)
    {
        KeyValuePair<Vector2, LevelNode> nodeKeyValuePair = GetLevelNodesDataByNumber(levelNumber);

        nodeKeyValuePair.Value.Status = LevelNodeStatus.Finished;
        UnlockNeighbours(nodeKeyValuePair.Key);

        LevelStatusPlayerPrefs.SetLevelStatus(levelNumber, (int)LevelNodeStatus.Finished);

        if (LevelFinished != null)
            LevelFinished(nodeKeyValuePair.Value);
    }

    public KeyValuePair<Vector2, LevelNode> GetLevelNodesDataByNumber(int levelNumber)
    {
        foreach (var levelNode in _levelNodes)
        {
            if (levelNode.Value.LevelNumber != levelNumber)
                continue;

            return levelNode;
        }

        return new KeyValuePair<Vector2, LevelNode>();
    }

    /// <summary>
    /// Unlock all neighbours of the specified position if they are still locked
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private void UnlockNeighbours(Vector2 position)
    {
        for (int x = (int)position.x - 1; x <= position.x + 1; x += 2)
        {
            Vector2 neighbourPosition = new Vector2(x, position.y);

            //if our neighbour doens't exist, or its status is the same or higher then the status we would increase it to, skip this iteration
            if (!_levelNodes.ContainsKey(neighbourPosition) || _levelNodes[neighbourPosition].Status != LevelNodeStatus.Locked)
                continue;

            _levelNodes[neighbourPosition].Status = LevelNodeStatus.Unlocked;
            LevelStatusPlayerPrefs.SetLevelStatus(_levelNodes[neighbourPosition].LevelNumber, (int)LevelNodeStatus.Unlocked);
        }

        for (int y = (int)position.y - 1; y <= position.y + 1; y += 2)
        {
            Vector2 neighbourPosition = new Vector2(position.x, y);

            //if our neighbour doens't exist, or its status is the same or higher then the status we would increase it to, skip this iteration
            if (!_levelNodes.ContainsKey(neighbourPosition) || _levelNodes[neighbourPosition].Status != LevelNodeStatus.Locked)
                continue;

            _levelNodes[neighbourPosition].Status = LevelNodeStatus.Unlocked;
            LevelStatusPlayerPrefs.SetLevelStatus(_levelNodes[neighbourPosition].LevelNumber, (int)LevelNodeStatus.Unlocked);
        }
    }

    private void UnlockAllLevels()
    {
        foreach (var keyValuePair in _levelNodes)
        {
            keyValuePair.Value.Status = LevelNodeStatus.Finished;
        }
    }

    private void DestroyChildren()
    {
        _levelNodes.Clear();

        var children = new List<GameObject>();
        foreach (Transform child in transform)
            children.Add(child.gameObject);
        children.ForEach(Destroy);
    }

    public void DestroyImmediateChildren()
    {
        _levelNodes.Clear();

        var children = new List<GameObject>();
        foreach (Transform child in transform)
            children.Add(child.gameObject);
        children.ForEach(DestroyImmediate);
    }

    public Dictionary<Vector2, LevelNode> LevelNodes
    {
        get { return _levelNodes; }
    }
}

public enum LevelNodeStatus
{
    Locked = 0,
    Unlocked = 1,
    Finished = 2,
}