using UnityEngine;

public class GridPositionTileCondition : TileCondition {

    [SerializeField] private GridPositionType gridPositionType;

    public override bool Check(Vector2 gridPosition) {
        bool exists = TileGrid.Instance.ContainsTile(gridPosition);
        bool condition = false;

        switch (gridPositionType) {
            case GridPositionType.Empty:
                condition = !exists;
                break;
            case GridPositionType.Occupied:
                condition = exists;
                break;
            case GridPositionType.UserGenerated:
                bool isUserGenerated = exists && TileGrid.Instance.GetTile(gridPosition).UserGenerated;
                condition = isUserGenerated;
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
