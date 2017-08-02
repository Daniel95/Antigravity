using System.Collections.Generic;
using UnityEngine;

public class VerticalNeighbourAmountTileCondition : AmountTileCondition {

    [SerializeField] private bool solid = true;

    private const string AMOUNT_TYPE_NAME = "Vertical neighbour";
    private const string SOLID_NAME = "Solid";

    public override bool Check(Vector2 gridPosition) {
        List<Vector2> directNeighbourPositions = TileGrid.GetNeighbourPositions(gridPosition, true, NeighbourType.Direct);
        List<Vector2> verticalNeighbourPositions = directNeighbourPositions.FindAll(x => gridPosition.y == x.y);

        int verticalNeighbourCount;

        if (solid) {
            List<Vector2> solidVerticalNeighbourPositions = verticalNeighbourPositions.FindAll(x => TileGrid.GetTile(x).IsSolid);
            verticalNeighbourCount = solidVerticalNeighbourPositions.Count;
        } else {
            verticalNeighbourCount = verticalNeighbourPositions.Count;
        }

        bool condition = CheckAmount(verticalNeighbourCount);

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