using System.Collections.Generic;
using UnityEngine;

public class DirectionNeighbourAmountTileCondition : AmountTileCondition {

    [SerializeField] private bool solid = true;

    private const string AMOUNT_TYPE_NAME = "Direct neighbour";
    private const string SOLID_NAME = "Solid";

    public override bool Check(Vector2 gridPosition) {
        List<Vector2> directNeighbourPositions = TileGrid.GetNeighbourPositions(gridPosition, true, NeighbourType.Direct);

        int directNeighbourCount;

        if (solid) {
            List<Vector2> solidDirectNeighbourPositions = directNeighbourPositions.FindAll(x => TileGrid.GetTile(x).IsSolid);
            directNeighbourCount = solidDirectNeighbourPositions.Count;
        } else {
            directNeighbourCount = directNeighbourPositions.Count;
        }

        bool condition = CheckAmount(directNeighbourCount);

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
