using System.Collections.Generic;
using UnityEngine;

public interface ISelectableLevelField {

    Dictionary<Vector2, ISelectableLevel> SelectableLevels { get; }

    void DestroySelectableLevelFields();
    void GenerateSelectableLevelFields();
    void UnlockAllNeighboursOfFinishedSelectableLevels();
    void ApplySelectableLevelValues();
}
