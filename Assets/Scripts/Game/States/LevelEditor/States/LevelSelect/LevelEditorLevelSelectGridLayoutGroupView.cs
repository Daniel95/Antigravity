using IoCPlus;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class LevelEditorLevelSelectGridLayoutGroupView : View, ILevelEditorLevelSelectGridLayoutGroup {

    public Transform Transform { get { return transform; } }

    [Inject] private Ref<ILevelEditorLevelSelectGridLayoutGroup> levelEditorLevelSelectGridLayoutGroupRef;

    public override void Initialize() {
        levelEditorLevelSelectGridLayoutGroupRef.Set(this);
    }

}