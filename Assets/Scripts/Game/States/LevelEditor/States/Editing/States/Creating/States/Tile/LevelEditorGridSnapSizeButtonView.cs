using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelEditorGridSnapSizeButtonView : ButtonListenerView {

    protected override void OnButtonClick() {
        Vector2 currentGridSnapSize = LevelEditorGridSnapSizeStatus.Size;
        List<Vector2> gridSnapSizes = LevelEditorGridSnapSizeLibrary.Instance.GridSnapSizes.Select(x => x.Size).ToList();
        Vector2 nextGridSnapSize = gridSnapSizes.GetNext(currentGridSnapSize, 1);
        LevelEditorGridSnapSizeStatus.Size = nextGridSnapSize;
    }

}
