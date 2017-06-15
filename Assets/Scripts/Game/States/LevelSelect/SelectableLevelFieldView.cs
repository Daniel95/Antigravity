using IoCPlus;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectableLevelFieldView : View, ISelectableLevelField {

    [Serializable]
    public struct LevelNodeField {
        public GameObject ObjectToBuild;
        public Vector2 FieldPosition;
        public Vector2 FieldSize;
        public Vector2 LevelStartDirection;
        public bool IsHorizontal;
        public int StartCountingValue;
        public bool SpiralCounting;
    }

    public Dictionary<Vector2, ISelectableLevel> SelectableLevels { get { return selectableLevels; } }

    [SerializeField] private LevelNodeField[] levelNodeFields;
    [SerializeField] private Vector2 gridSpacing;

    [Inject] private Ref<ISelectableLevelField> selectableLevelFieldRef;
    [Inject] private Ref<GameStateModel> gameStateModel;

    private readonly Dictionary<Vector2, ISelectableLevel> selectableLevels = new Dictionary<Vector2, ISelectableLevel>();

    private FieldGenerator fieldGenerator;

    public override void Initialize() {
        selectableLevelFieldRef.Set(this);
    }

    public void GenerateSelectableLevelFields() {
        if(fieldGenerator == null) {
            fieldGenerator = GetComponent<FieldGenerator>();
        }

        for (int i = 0; i < levelNodeFields.Length; i++) {
            List<FieldGenerator.Node> nodes = fieldGenerator.GenerateField(levelNodeFields[i].FieldSize, levelNodeFields[i].LevelStartDirection, levelNodeFields[i].SpiralCounting, levelNodeFields[i].StartCountingValue, levelNodeFields[i].IsHorizontal);

            foreach (FieldGenerator.Node node in nodes) {
                Vector2 gridPosition = node.Position + levelNodeFields[i].FieldPosition;
                Vector2 nodePositionOffset = new Vector2(node.Position.x * levelNodeFields[i].ObjectToBuild.transform.localScale.x, node.Position.y * levelNodeFields[i].ObjectToBuild.transform.localScale.y);
                Vector2 fieldPositionOffset = new Vector2(levelNodeFields[i].FieldPosition.x * levelNodeFields[i].ObjectToBuild.transform.localScale.x, levelNodeFields[i].FieldPosition.y * levelNodeFields[i].ObjectToBuild.transform.localScale.y);

                Vector2 positionOffset = nodePositionOffset + fieldPositionOffset;

                Vector2 gridSpacingOffset = new Vector2(positionOffset.x * gridSpacing.x, positionOffset.y * gridSpacing.y);

                Vector2 nodePosition = positionOffset + gridSpacingOffset;

                LevelProgressState status = gameStateModel.Get().CompletedLevels.Contains(node.Counter) ? LevelProgressState.Finished : LevelProgressState.Locked;

                ISelectableLevel levelNode = InstantiateSelectableLevel(levelNodeFields[i].ObjectToBuild, gameObject, nodePosition, node.Counter, status);

                selectableLevels.Add(gridPosition, levelNode);
            }
        }
    }

    public void DestroySelectableLevelFields() {
        selectableLevels.Clear();
        gameObject.DestroyAllChildren();
    }

    public void DestroyImmediateSelectableLevelFields() {
        selectableLevels.Clear();

        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in transform) {
            children.Add(child.gameObject);
        }
        children.ForEach(DestroyImmediate);
    }

    public void UnlockAllNeighboursOfFinishedSelectableLevels() {
        foreach (Vector2 selectableLevelGridPosition in selectableLevels.Keys) {
            UnlockNeighboursOfSelectableLevel(selectableLevelGridPosition);
        }
    }

    public void ApplySelectableLevelValues() {
        foreach (ISelectableLevel selectableLevel in selectableLevels.Values) {
            selectableLevel.ApplyLevelNumber();
            selectableLevel.ApplyLevelProgressState();
        }
    }

    private static ISelectableLevel InstantiateSelectableLevel(GameObject objectToBuild, GameObject parent, Vector2 worldPosition, int counter, LevelProgressState status) {
        GameObject node = Instantiate(objectToBuild, worldPosition, new Quaternion(0, 0, 0, 0));
        node.transform.parent = parent.transform;

        ISelectableLevel levelNode = node.GetComponent<ISelectableLevel>();
        levelNode.Instantiate(status, counter);

        return levelNode;
    }

    private void UnlockNeighboursOfSelectableLevel(Vector2 gridPosition) {
        for (int x = (int)gridPosition.x - 1; x <= gridPosition.x + 1; x += 2) {
            Vector2 neighbourPosition = new Vector2(x, gridPosition.y);

            bool neighbourExists = selectableLevels.ContainsKey(neighbourPosition);
            if (!neighbourExists) { continue; }

            selectableLevels[neighbourPosition].IncreaseLevelProgressStateTo(LevelProgressState.Unlocked);
        }

        for (int y = (int)gridPosition.y - 1; y <= gridPosition.y + 1; y += 2) {
            Vector2 neighbourPosition = new Vector2(gridPosition.x, y);

            bool neighbourExists = selectableLevels.ContainsKey(neighbourPosition);
            if (!neighbourExists) { continue; }

            selectableLevels[neighbourPosition].IncreaseLevelProgressStateTo(LevelProgressState.Unlocked);
        }
    }

    private void Awake() {
        fieldGenerator = GetComponent<FieldGenerator>();
    }
}