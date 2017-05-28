using System.Collections.Generic;
using UnityEngine;

interface IHook {

    HookState ActiveHookState { get; }
    HookState LastHookState { get; }
    List<Transform> Anchors { get; }
    List<int> HookableLayers { get; }
    LayerMask RayLayers { get; }
    LineRenderer LineRenderer { get; }
    float DirectionSpeedNeutralValue { get; }
    float MinimalHookDistance { get; }
    GameObject Owner { get; }
    Vector2 Destination { get; set; }

    void SetHookState(HookState hookstate);
    void AddAnchor(Vector2 position, Transform parent);
    void DestroyAnchorAt(int index);
    void DestroyAnchors();
    void ActivateHookRope();
    void DeactivateHookRope();
}
