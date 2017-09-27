using System.Collections.Generic;
using UnityEngine;

public class DirectionNeighbourAmountTileCondition : GenerateTypeAmountTileCondition {

    private const string AMOUNT_TYPE_NAME = "Direct neighbour";

    public override bool Check(Vector2 gridPosition) {
        List<Vector2> directNeighbourPositions = TileGrid.Instance.GetNeighbourPositions(gridPosition, true, NeighbourType.Direct);

        bool condition = CheckGenerateTypeAmount(directNeighbourPositions);

        return condition;
    }

    private void OnValidate() {
        UpdateName(AMOUNT_TYPE_NAME);
    }

    private void Reset() {
        UpdateName(AMOUNT_TYPE_NAME);
    }

}
