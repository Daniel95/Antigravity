using System.Collections.Generic;
using UnityEngine;

public class DirectionNeighbourAmountTileCondition : AmountTileCondition {

    private const string AMOUNT_TYPE_NAME = "Direct neighbour";

    public override bool Check(Vector2 gridPosition, GeneratePhase generatePhase) {
        List<Vector2> directNeighbourPositions = TileGrid.GetNeighbourPositions(gridPosition, true, NeighbourType.Direct);

        bool condition = CheckAmount(directNeighbourPositions.Count);
        return condition;
    }

    private void OnValidate() {
        UpdateName(AMOUNT_TYPE_NAME);
    }

    private void Reset() {
        UpdateName(AMOUNT_TYPE_NAME);
    }

}
