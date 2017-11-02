using IoCPlus;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HookProjectileView : View, IHookProjectile {

    public float DistanceFromOwner { get { return distanceFromOwner; } set { distanceFromOwner = value; } }
    public Transform Transform { get { return transform; } }
    public List<GameObject> CollidingGameObjects { get { return collidingGameObjects; } set { collidingGameObjects = value; } }
    public List<int> CollidingLayers {
        get {
            if (collidingGameObjects.Count <= 0) { return new List<int>(); }
            return CollidingGameObjects.Select(x => x.layer).ToList();
        }
    }

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    private Collider2D myCollider;
    private List<GameObject> collidingGameObjects = new List<GameObject>();
    private float distanceFromOwner;

    public override void Initialize() {
        hookProjectileRef.Set(this);
        gameObject.SetActive(false);
    }

    public void SetParent(Transform parent) {
        transform.SetParent(parent, true);
    }

    public void SetPosition(Vector2 position) {
        transform.position = position;
    }

    public void SetColliderEnabled(bool enabled) {
        myCollider.enabled = enabled;
    }

    public void DestroyProjectile() {
        Destroy();
    }

    public void ActivateHookProjectile(Vector2 startPosition) {
        transform.position = startPosition;
        gameObject.SetActive(true);
    }

    public void DeactivateHookProjectile() {
        gameObject.SetActive(false);
    }

    private void Awake() {
        myCollider = GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.isTrigger) { return; }

    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.isTrigger) { return; }

    }

}
