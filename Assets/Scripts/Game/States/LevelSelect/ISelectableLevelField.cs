using System.Collections.Generic;
using UnityEngine;

public interface ISelectableLevelField {

    Dictionary<Vector2, ISelectableLevel> SelectableLevels { get; }

    ISelectableLevel GetSelectableLevelByLevelNumber(int levelNumber);

    void DestroySelectableLevelFields();
    void GenerateSelectableLevelFields();
    void UnlockAllNeighboursOfFinishedSelectableLevels();
    void ApplySelectableLevelValues();
}
