using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectField : MonoBehaviour {

    [SerializeField]
    private LevelFields[] levelFields;

    [SerializeField]
    private Vector2 borderSize;

    private readonly Dictionary<Vector2, LevelNode> _levelNodes = new Dictionary<Vector2, LevelNode>();

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

    private KeyValuePair<Vector2, LevelNode> GetLevelNodesDataByNumber(int levelNumber)
    {
        foreach (var levelNode in _levelNodes)
        {
            if (levelNode.Value.LevelNumber != levelNumber)
                continue;

            return levelNode;
        }

        return new KeyValuePair<Vector2, LevelNode>();
    }

    private void CheckLevelFinished()
    {
        GameObject keeperGameObject = GameObject.FindGameObjectWithTag(Tags.LevelFinishedKeeper);

        LevelLoader levelLoader = GetComponent<LevelLoader>();

        if (keeperGameObject)
        {
            FinishedLevelKeeper finishedLevelKeeper = keeperGameObject.GetComponent<FinishedLevelKeeper>();

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
        KeyValuePair<Vector2, LevelNode> nodeValuePair = GetLevelNodesDataByNumber(levelNumber);

        nodeValuePair.Value.Status = LevelNodeStatus.Finished;
        UnlockNeighbours(nodeValuePair.Key);

        LevelStatusPlayerPrefs.SetLevelStatus(levelNumber, (int)LevelNodeStatus.Finished);
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

    /// <summary>
    /// Get the status of a node.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private LevelNodeStatus CheckNeighboursStatuses(Vector2 position)
    {
        int index = 0;

        for (int x = (int)position.x - 1; x <= position.x + 1; x += 2)
        {
            Vector2 neighbourPosition = new Vector2(x, position.y);

            //if our neighbour doens't exist, or its status is the same or lower then our status, skip this iteration
            if (!_levelNodes.ContainsKey(neighbourPosition) || index >= (int)_levelNodes[neighbourPosition].Status)
                continue;

            //if the status is the highest achievable status, return it and stop this method
            if ((int)_levelNodes[neighbourPosition].Status >= Enum.GetNames(typeof(LevelNodeStatus)).Length - 1)
                return _levelNodes[neighbourPosition].Status;

            index = (int)_levelNodes[neighbourPosition].Status;
        }

        for (int y = (int)position.y - 1; y <= position.y + 1; y += 2)
        {
            Vector2 neighbourPosition = new Vector2(position.x, y);

            //if our neighbour doens't exist, or its status is the same or lower then our status, skip this iteration
            if (!_levelNodes.ContainsKey(neighbourPosition) || index >= (int)_levelNodes[neighbourPosition].Status)
                continue;

            //if the status is the highest achievable status, return it and stop this method
            if ((int)_levelNodes[neighbourPosition].Status >= Enum.GetNames(typeof(LevelNodeStatus)).Length - 1)
                return _levelNodes[neighbourPosition].Status;

            index = (int)_levelNodes[neighbourPosition].Status;
        }

        return (LevelNodeStatus)index;
    }

    /// <summary>
    /// Set the increase status of our neighbours only if it's current status is lower than the new status.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    private void IncreaseNeighboursStatuses(Vector2 position, LevelNodeStatus status)
    {
        for (int x = (int)position.x - 1; x <= position.x + 1; x += 2)
        {
            Vector2 neighbourPosition = new Vector2(x, position.y);

            //if our neighbour doens't exist, or its status is the same or higher then the status we would increase it to, skip this iteration
            if (!_levelNodes.ContainsKey(neighbourPosition) || (int)_levelNodes[neighbourPosition].Status >= (int)status)
                continue;

            _levelNodes[neighbourPosition].Status = status;
            LevelStatusPlayerPrefs.SetLevelStatus(_levelNodes[neighbourPosition].LevelNumber, (int)_levelNodes[neighbourPosition].Status);
        }

        for (int y = (int)position.y - 1; y <= position.y + 1; y += 2)
        {
            Vector2 neighbourPosition = new Vector2(position.x, y);

            //if our neighbour doens't exist, or its status is the same or higher then the status we would increase it to, skip this iteration
            if (!_levelNodes.ContainsKey(neighbourPosition) || (int)_levelNodes[neighbourPosition].Status >= (int)status)
                continue;

            _levelNodes[neighbourPosition].Status = status;
            LevelStatusPlayerPrefs.SetLevelStatus(_levelNodes[neighbourPosition].LevelNumber, (int)_levelNodes[neighbourPosition].Status);
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
}

public enum LevelNodeStatus
{
    Locked = 0,
    Unlocked = 1,
    Finished = 2,
}