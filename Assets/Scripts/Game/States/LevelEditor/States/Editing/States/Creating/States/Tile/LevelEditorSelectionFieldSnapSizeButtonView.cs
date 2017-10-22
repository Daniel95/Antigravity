using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorSelectionFieldSnapSizeButtonView : ButtonListenerView {

    [SerializeField] private List<int> snapSizes;

    private int snapSizeIndex;

    public override void Initialize() {
        base.Initialize();
        UpdateSnapSize();
    }

    protected override void OnButtonClick() {
        snapSizeIndex++;
        UpdateSnapSize();
    }

    private void UpdateSnapSize() {
        int snapSize = snapSizes[snapSizeIndex % snapSizes.Count];
        LevelEditorSelectionFieldSnapSizeStatus.Size = new Vector2(snapSize, snapSize);
    }

}
