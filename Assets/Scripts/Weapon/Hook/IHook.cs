using System.Collections.Generic;
using UnityEngine;

interface IHook {

    HookState ActiveHookState { get; set; }
    HookState LastHookState { get; set; }
    List<Transform> Anchors { get; }
    LayerMask RayLayers { get; }
    LineRenderer LineRenderer { get; }
    float DirectionSpeedNeutralValue { get; }
    float MinimalHookDistance { get; }
    GameObject Owner { get; }
    Vector2 Destination { get; set; }

    void AddAnchor(Vector2 position, Transform parent);
    void DestroyAnchorAt(int index);
    void DestroyAnchors();
    void ActivateHookRope();
    void DeactivateHookRope();
}
