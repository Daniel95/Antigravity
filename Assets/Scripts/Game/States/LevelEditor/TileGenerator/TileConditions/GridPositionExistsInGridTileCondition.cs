using UnityEngine;

public class GridPositionExistsInGridTileCondition : TileCondition {

    [SerializeField] private bool isOccupied;

    public override bool Check(Vector2 gridPosition, GeneratePhase generatePhase) {
        bool condition = TileGrid.ContainsPosition(gridPosition) == isOccupied;
        return condition;
    }

    private void UpdateName() {
        string conditionName = "Grid position";

        if(isOccupied) {
            conditionName += " exists in grid";
        } else {
            conditionName += " does not exist in grid";
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
