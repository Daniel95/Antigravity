using System.Collections.Generic;
using UnityEngine;

interface IHook {

    void Hooked();
    void Canceled();
    void AddAnchor(Vector2 position, Transform parent);

    HookState CurrentHookState { get; }
    GameObject HookProjectileGameObject { get; }
    HookProjectile HookProjectile { get; }
    List<Transform> Anchors { get; }
    LayerMask RayLayers { get; }
    LineRenderer LineRendererComponent { get; }
}
