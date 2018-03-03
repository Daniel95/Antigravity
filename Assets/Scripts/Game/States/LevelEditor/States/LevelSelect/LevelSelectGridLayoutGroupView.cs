using IoCPlus;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class LevelSelectGridLayoutGroupView : View, ILevelSelectGridLayoutGroup {

    public Transform Transform { get { return transform; } }

    [Inject] private Ref<ILevelSelectGridLayoutGroup> levelSelectGridLayoutGroupRef;

    public override void Initialize() {
        levelSelectGridLayoutGroupRef.Set(this);
    }

}