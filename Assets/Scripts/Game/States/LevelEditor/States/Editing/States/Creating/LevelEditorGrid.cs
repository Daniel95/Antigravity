using System.Collections.Generic;
using UnityEngine;

public class LevelEditorGridPositions : MonoBehaviour {

    public static HashSet<Vector2> GridPositions { get { return gridPositions; } }

    private static HashSet<Vector2> gridPositions = new HashSet<Vector2>();

    public bool Contains(Vector2 gridPosition) {
        return gridPositions.Contains(gridPosition);
    }

    public virtual void Clear() {
        gridPositions.Clear();
    }

    protected virtual void Add(Vector2 gridPosition) {
        gridPositions.Add(gridPosition);
    }

    protected virtual void Remove(Vector2 gridPosition) {
        gridPositions.Remove(gridPosition);
    }

    public Vector2 GetDirectionToGridPositions(Vector2 gridPosition, List<Vector2> gridPositions) {
        Vector2 combinedOffsets = new Vector2();

        foreach (Vector2 directNeighbourPosition in gridPositions) {
            Vector2 offsetToNeighbour = directNeighbourPosition - gridPosition;
            combinedOffsets += offsetToNeighbour;
        }

        Vector2 neighbourDirection = VectorHelper.InvertOnNegativeCeil(combinedOffsets);

        return neighbourDirection;
    }

    public List<Vector2> GetNeighbourPositions(Vector2 gridPosition, bool existing, NeighbourType neighbourType = NeighbourType.All, int maxNeighbourOffset = 1) {
        List<Vector2> neighbourPositions = new List<Vector2>();

        switch (neighbourType) {
            case NeighbourType.All:
                neighbourPositions = GetAllNeighbourPositions(gridPosition, existing, maxNeighbourOffset);
                break;
            case NeighbourType.Direct:
                neighbourPositions = GetDirectNeighbourPositions(gridPosition, existing, maxNeighbourOffset);
                break;
            case NeighbourType.Indirect:
                neighbourPositions = GetIndirectNeighbourPositions(gridPosition, existing, maxNeighbourOffset);
                break;
        }

        return neighbourPositions;
    }

    protected List<Vector2> GetAllNeighbourPositions(Vector2 gridPosition, bool existing, int maxNeighbourOffset = 1) {
        List<Vector2> neighbourPositions = new List<Vector2>();

        for (int x = (int)gridPosition.x - maxNeighbourOffset; x <= gridPosition.x + maxNeighbourOffset; x++) {
            for (int y = (int)gridPosition.y - maxNeighbourOffset; y <= gridPosition.y + maxNeighbourOffset; y++) {
                Vector2 neighbourPosition = new Vector2(x, y);
                if(neighbourPosition == gridPosition) { continue; }
                if(existing && !gridPositions.Contains(neighbourPosition)) { continue; }

                neighbourPositions.Add(neighbourPosition);
            }
        }

        return neighbourPositions;
    }

    protected List<Vector2> GetDirectNeighbourPositions(Vector2 gridPosition, bool existing, int maxNeighbourOffset = 1) {
        List<Vector2> directNeighbourPositions = new List<Vector2>();

        for (int x = (int)gridPosition.x - maxNeighbourOffset; x <= gridPosition.x + maxNeighbourOffset; x++) {
            Vector2 neighbourPosition = new Vector2(x, gridPosition.y);
            if (neighbourPosition == gridPosition) { continue; }
            if (existing && !gridPositions.Contains(neighbourPosition)) { continue; }

            directNeighbourPositions.Add(neighbourPosition);
        }

        for (int y = (int)gridPosition.y - maxNeighbourOffset; y <= gridPosition.y + maxNeighbourOffset; y++) {
            Vector2 neighbourPosition = new Vector2(gridPosition.x, y);
            if (neighbourPosition == gridPosition) { continue; }
            if (existing && !gridPositions.Contains(neighbourPosition)) { continue; }

            directNeighbourPositions.Add(neighbourPosition);
        }

        return directNeighbourPositions;
    }

    protected List<Vector2> GetIndirectNeighbourPositions(Vector2 gridPosition, bool existing, int maxNeighbourOffset = 1) {
        List<Vector2> neighbourPositions = GetAllNeighbourPositions(gridPosition, existing, maxNeighbourOffset);
        List<Vector2> indirectNeighbourPositions = neighbourPositions.FindAll(x => gridPosition.x != x.x && gridPosition.y != x.y);

        return indirectNeighbourPositions;
    }

}
