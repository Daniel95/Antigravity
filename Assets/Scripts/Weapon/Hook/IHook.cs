using System.Collections.Generic;
using UnityEngine;

interface IHook {

    HookState ActiveHookState { get; }
    HookState LastHookState { get; }
    List<Transform> Anchors { get; }
    LayerMask RopeRaycastLayerMask { get; }
    List<int> HookableLayers { get; }
    float DirectionSpeedNeutralValue { get; }
    float MinimalDistanceFromOwner { get; }

    void SetHookState(HookState hookstate);
    void AddAnchor(Vector2 position, Transform parent);
    void DestroyAnchorAt(int index);
    void DestroyAnchors();
    void ActivateHook();
    void DeactivateHook();
}
