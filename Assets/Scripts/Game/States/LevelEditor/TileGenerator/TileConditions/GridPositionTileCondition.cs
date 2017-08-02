using UnityEngine;

public class GridPositionTileCondition : TileCondition {

    [SerializeField] private GridPositionTileConditionType gridPositionTileConditionType;

    public override bool Check(Vector2 gridPosition) {
        bool exists = TileGrid.ContainsPosition(gridPosition);
        bool condition = false;

        switch (gridPositionTileConditionType) {
            case GridPositionTileConditionType.Empty:
                condition = !exists;
                break;
            case GridPositionTileConditionType.Occupied:
                condition = exists;
                break;
            case GridPositionTileConditionType.Solid:
                bool isSolid = exists && TileGrid.GetTile(gridPosition).IsSolid;
                condition = isSolid;
                break;
        }

        return condition;
    }

    private void UpdateName() {
        string conditionName = "";

        switch (gridPositionTileConditionType) {
            case GridPositionTileConditionType.Empty:
                conditionName = "Grid position is empty";
                break;
            case GridPositionTileConditionType.Occupied:
                conditionName = "Grid position is occupied";
                break;
            case GridPositionTileConditionType.Solid:
                conditionName = "Grid position is solid";
                break;
        }

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
