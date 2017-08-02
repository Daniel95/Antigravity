using System.Collections.Generic;
using UnityEngine;

public class NeighbourAmountTileCondition : AmountTileCondition {

    [SerializeField] private bool solid = true;

    private const string AMOUNT_TYPE_NAME = "Neighbour";
    private const string SOLID_NAME = "Solid";

    public override bool Check(Vector2 gridPosition) {
        List<Vector2> neighbourPositions = TileGrid.GetNeighbourPositions(gridPosition, true);

        int neighbourCount;

        if(solid) {
            List<Vector2> solidNeighbourPositions = neighbourPositions.FindAll(x => TileGrid.GetTile(x).IsSolid);
            neighbourCount = solidNeighbourPositions.Count;
        } else {
            neighbourCount = neighbourPositions.Count;
        }

        bool condition = CheckAmount(neighbourCount);

        return condition;
    }

    private void OnValidate() {
        if(solid) {
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
