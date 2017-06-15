public interface ISelectableLevelField {

    void DestroySelectableLevelFields();
    void GenerateSelectableLevelFields();
    void UnlockAllNeighboursOfFinishedSelectableLevels();
    void ApplySelectableLevelValues();
}
