using System.Collections.Generic;
using UnityEngine;

public class RectangulatedTileGridStatus {

    public List<Vector2> LastUpdatedRectangulatedTileGrid = new List<Vector2>();

    public void UpdateRectangulatedTileGrid() {
        LastUpdatedRectangulatedTileGrid.Clear();

        List<Vector2> tileGridPositions = TileGrid.Instance.GetTileGridPositions();
        List<List<Vector2>> listOfRectangles = GridHelper.SortIntoRectangles(tileGridPositions);

        for (int i = 0; i < listOfRectangles.Count; i++) {
            for (int j = 0; j < listOfRectangles[i].Count; j++) {
                LastUpdatedRectangulatedTileGrid.Add(listOfRectangles[i][j]);
            }
        }
    }

}
