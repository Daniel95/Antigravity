using System.Collections.Generic;
using UnityEngine;

interface IHook {

    void AddAnchor(Vector2 position, Transform parent);
    void ActivateHookProjectile(Vector2 spawnPosition);
    void DeactivateHookProjectile();
    void ActivateHookRope();
    void DeactivateHookRope();
    void DestroyAnchors();

    HookState ActiveHookState { get; set; }
    HookState LastHookState { get; set; }
    GameObject HookProjectileGameObject { get; }
    List<Transform> Anchors { get; }
    LayerMask RayLayers { get; }
    LineRenderer LineRendererComponent { get; }
    float DirectionSpeedNeutralValue { get; }
    float MinimalHookDistance { get; }
    GameObject Owner { get; }
}
