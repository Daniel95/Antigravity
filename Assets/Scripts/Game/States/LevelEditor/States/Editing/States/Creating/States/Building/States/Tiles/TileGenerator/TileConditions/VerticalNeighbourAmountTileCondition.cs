using System.Collections.Generic;
using UnityEngine;

public class VerticalNeighbourAmountTileCondition : GenerateTypeAmountTileCondition {

    private const string AMOUNT_TYPE_NAME = "Vertical neighbour";

    public override bool Check(Vector2 gridPosition) {
        List<Vector2> directNeighbourPositions = TileGrid.Instance.GetNeighbourPositions(gridPosition, true, NeighbourType.Direct);
        List<Vector2> verticalNeighbourPositions = directNeighbourPositions.FindAll(x => gridPosition.y == x.y);

        bool condition = CheckGenerateTypeAmount(verticalNeighbourPositions);

        return condition;
    }

    private void OnValidate() {
        UpdateName(AMOUNT_TYPE_NAME);

    }

    private void Reset() {
        UpdateName(AMOUNT_TYPE_NAME);
    }

}