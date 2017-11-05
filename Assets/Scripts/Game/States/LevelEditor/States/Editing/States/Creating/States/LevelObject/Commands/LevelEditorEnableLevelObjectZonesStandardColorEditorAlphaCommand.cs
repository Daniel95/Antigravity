using IoCPlus;
using UnityEngine;

public class LevelEditorEnableLevelObjectZonesStandardColorEditorAlphaCommand : Command<bool> {

    [Inject] private Refs<ILevelObjectZone> levelObjectZoneRefs;

    protected override void Execute(bool enable) {
        foreach (ILevelObjectZone levelObjectZone in levelObjectZoneRefs) {
            levelObjectZone.EnableStandardColorEditorAlpha(enable);
        }
    }

}
