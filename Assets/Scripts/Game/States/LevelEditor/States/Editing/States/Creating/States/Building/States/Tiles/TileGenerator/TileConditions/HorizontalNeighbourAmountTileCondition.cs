using System.Collections.Generic;
using UnityEngine;

public class HorizontalNeighbourAmountTileCondition : GenerateTypeAmountTileCondition {

    private const string AMOUNT_TYPE_NAME = "Horizontal neighbour";

    public override bool Check(Vector2 gridPosition) {
        List<Vector2> directNeighbourPositions = LevelEditorTileGrid.Instance.GetNeighbourPositions(gridPosition, true, NeighbourType.Direct);
        List<Vector2> horizontalNeighbourPositions = directNeighbourPositions.FindAll(x => gridPosition.x == x.x);

        bool condition = CheckGenerateTypeAmount(horizontalNeighbourPositions);

        return condition;
    }

    private void OnValidate() {
        UpdateName(AMOUNT_TYPE_NAME);
    }

    private void Reset() {
        UpdateName(AMOUNT_TYPE_NAME);
    }

}
