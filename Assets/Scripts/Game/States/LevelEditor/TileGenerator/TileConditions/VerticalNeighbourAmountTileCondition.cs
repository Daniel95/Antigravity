using System.Collections.Generic;
using UnityEngine;

public class VerticalNeighbourAmountTileCondition : AmountTileCondition {

    private const string AMOUNT_TYPE_NAME = "Vertical neighbour";

    public override bool Check(Vector2 gridPosition, GeneratePhase generatePhase) {
        List<Vector2> directNeighbourPositions = TileGrid.GetNeighbourPositions(gridPosition, true, NeighbourType.Direct);
        List<Vector2> verticalNeighbourPositions = directNeighbourPositions.FindAll(x => gridPosition.y == x.y);

        bool condition = CheckAmount(verticalNeighbourPositions.Count);
        return condition;
    }

    private void OnValidate() {
        UpdateName(AMOUNT_TYPE_NAME);
    }

    private void Reset() {
        UpdateName(AMOUNT_TYPE_NAME);
    }

}
