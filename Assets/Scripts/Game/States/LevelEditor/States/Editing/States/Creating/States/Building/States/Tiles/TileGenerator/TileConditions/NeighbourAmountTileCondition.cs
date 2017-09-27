using System.Collections.Generic;
using UnityEngine;

public class NeighbourAmountTileCondition : GenerateTypeAmountTileCondition {

    private const string AMOUNT_TYPE_NAME = "Neighbour";

    public override bool Check(Vector2 gridPosition) {
        List<Vector2> neighbourPositions = TileGrid.Instance.GetNeighbourPositions(gridPosition, true);

        bool condition = CheckGenerateTypeAmount(neighbourPositions);

        return condition;
    }

    private void OnValidate() {
        UpdateName(AMOUNT_TYPE_NAME);

    }

    private void Reset() {
        UpdateName(AMOUNT_TYPE_NAME);
    }

}
