using System.Collections.Generic;
using UnityEngine;

public class HorizontalNeighbourAmountTileCondition : AmountTileCondition {

    [SerializeField] private bool solid = true;

    private const string AMOUNT_TYPE_NAME = "Horizontal neighbour";
    private const string SOLID_NAME = "Solid";

    public override bool Check(Vector2 gridPosition) {
        List<Vector2> directNeighbourPositions = TileGrid.GetNeighbourPositions(gridPosition, true, NeighbourType.Direct);
        List<Vector2> horizontalNeighbourPositions = directNeighbourPositions.FindAll(x => gridPosition.x == x.x);

        int horizontalNeighbourCount;

        if (solid) {
            List<Vector2> solidHorizontalNeighbourPositions = horizontalNeighbourPositions.FindAll(x => TileGrid.GetTile(x).IsSolid);
            horizontalNeighbourCount = solidHorizontalNeighbourPositions.Count;
        } else {
            horizontalNeighbourCount = horizontalNeighbourPositions.Count;
        }

        bool condition = CheckAmount(horizontalNeighbourCount);

        return condition;
    }

    private void OnValidate() {
        if (solid) {
            UpdateName(SOLID_NAME + " " + AMOUNT_TYPE_NAME);
        } else {
            UpdateName(AMOUNT_TYPE_NAME);
        }
    }

    private void Reset() {
        if (solid) {
            UpdateName(SOLID_NAME + " " + AMOUNT_TYPE_NAME);
        } else {
            UpdateName(AMOUNT_TYPE_NAME);
        }
    }

}
