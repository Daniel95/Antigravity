using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelPath {

    public List<Vector2> PathWorldPositions {
        get {
            List<Vector2> pathWorldPositions = PathStraightWorldPositions.Concat(PathCornerWorldPositions).ToList();
            return pathWorldPositions;
        }
    }

    public List<Vector2> PathStraightWorldPositions;
    public List<Vector2> PathCornerWorldPositions;

}
