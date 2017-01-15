using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreLayerCollisions : MonoBehaviour {

    [SerializeField]
    private string[] layersToIgnore;

    private void Awake()
    {
        for (int i = 0; i < layersToIgnore.Length; i++)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer(layersToIgnore[i]), true);
        }
    }
}
