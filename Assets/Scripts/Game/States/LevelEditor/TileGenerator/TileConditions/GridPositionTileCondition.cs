﻿using UnityEngine;

public class GridPositionTileCondition : TileCondition {

    [SerializeField] private GridPositionType gridPositionType;

    public override bool Check(Vector2 gridPosition) {
        bool exists = TileGrid.ContainsPosition(gridPosition);
        bool condition = false;

        switch (gridPositionType) {
            case GridPositionType.Empty:
                condition = !exists;
                break;
            case GridPositionType.Occupied:
                condition = exists;
                break;
            case GridPositionType.UserGenerated:
                bool isSolid = exists && TileGrid.GetTile(gridPosition).UserGenerated;
                condition = isSolid;
                break;
        }

        return condition;
    }

    private void UpdateName() {
        string conditionName = "Grid position is " + gridPositionType.ToString();

        if (name != conditionName) {
            name = conditionName;
        }
    }

    private void OnValidate() {
        UpdateName();
    }

    private void Reset() {
        UpdateName();
    }

}