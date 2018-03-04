using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridSnapSizeButtonView : ButtonListenerView {

    protected override void OnButtonClick() {
        Vector2 currentGridSnapSize = GridSnapSizeStatusView.Size;
        List<Vector2> gridSnapSizes = GridSnapSizeLibrary.Instance.GridSnapSizes.Select(x => x.Size).ToList();
        Vector2 nextGridSnapSize = gridSnapSizes.GetNext(currentGridSnapSize, 1);
        GridSnapSizeStatusView.Size = nextGridSnapSize;
    }

}
