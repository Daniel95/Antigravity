using IoCPlus;
using UnityEngine;

public class LevelObjectZoneView : LevelObjectView, ILevelObjectZone {

    [Inject] private Refs<ILevelObjectZone> levelObjectZoneRefs;

    [SerializeField] private float standardColorEditorAlpha = 0.75f;

    private const string STANDARD_COLOR_PROPERTY_NAME = "_StandardColor";
    private const string OVERLAP_COLOR_PROPERTY_NAME = "_OverlapColor";

    private Material material;
    private Color overlapColor;
    private Color editorStandardColor;
    private Color inGamestandardColor;
    private SpriteRenderer spriteRenderer;

    public override void Initialize() {
        base.Initialize();
        levelObjectZoneRefs.Add(this);
    }

    public override void Dispose() {
        base.Dispose();
        levelObjectZoneRefs.Remove(this);
    }

    public void EnableStandardColorEditorAlpha(bool enable) {
        Color standardColor;
        if(enable) {
            standardColor = editorStandardColor;
        } else {
            standardColor = inGamestandardColor;
        }
        material.SetColor(STANDARD_COLOR_PROPERTY_NAME, standardColor);
    }

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        material = GetComponent<Renderer>().material;

        overlapColor = material.GetColor(OVERLAP_COLOR_PROPERTY_NAME);
        editorStandardColor = overlapColor;
        editorStandardColor.a = standardColorEditorAlpha;
        inGamestandardColor = material.GetColor(STANDARD_COLOR_PROPERTY_NAME);
    }

}
