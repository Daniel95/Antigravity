using UnityEngine;
using System.Collections.Generic;

public class HookModel {
    public enum HookStates { BusyShooting, BusyPullingBack, Active, Inactive }

    public HookStates CurrentHookState = HookStates.Inactive;
    public GameObject HookProjectileGameObject;
    public HookProjectile HookProjectile;
    public List<Transform> Anchors = new List<Transform>();
    public LayerMask RayLayers;
    public LineRenderer LineRendererComponent;

    public static class HookAbleLayers {
        public static int GrappleSurface = LayerMask.NameToLayer("GrappleHook");
        public static int PullSurface = LayerMask.NameToLayer("PullHook");
    }
}
