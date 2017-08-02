using System.Collections.Generic;
using UnityEngine;

public class NeighbourAmountTileCondition : AmountTileCondition {

    private const string AMOUNT_TYPE_NAME = "Neighbour";

    public override bool Check(Vector2 gridPosition, GeneratePhase generatePhase) {
        List<Vector2> neighbourPositions = TileGrid.GetNeighbourPositions(gridPosition, true);

        bool condition = CheckAmount(neighbourPositions.Count);

        return condition;
    }

    private void OnValidate() {
        UpdateName(AMOUNT_TYPE_NAME);
    }

    private void Reset() {
        UpdateName(AMOUNT_TYPE_NAME);
    }

}
