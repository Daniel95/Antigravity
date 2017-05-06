﻿using IoCPlus;
using UnityEngine;

public class HookProjectileView : View, IHookProjectile {

    public Transform Transform { get { return transform; } }
    public Transform AttachedTransform { get { return attachedTransform; } set { attachedTransform = value; } }
    public int HookedLayerIndex { get { return hookedLayer; } set { hookedLayer = value; } }
    public int ReachedAnchorsIndex { get { return anchorIndex; } set { anchorIndex = value; } }
    public bool IsMovingTowardsOwner { get { return isMovingTowardsOwner; } set { isMovingTowardsOwner = value; } }

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    private Transform attachedTransform;
    private int hookedLayer;
    private int anchorIndex;
    private bool isMovingTowardsOwner;

    public override void Initialize() {
        hookProjectileRef.Set(this);
    }

    public void SetParent(Transform parent) {
        transform.SetParent(parent, true);
    }
}
