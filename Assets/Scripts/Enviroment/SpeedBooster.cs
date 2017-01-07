using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBooster : MonoBehaviour {

    [SerializeField]
    private float speedBoost = 4;

    [SerializeField]
    private float speedReturnSpeed = 0.001f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IBoostAble boostAble = collision.GetComponent<IBoostAble>();

        if (boostAble != null)
        {
            boostAble.BoostSpeed(speedBoost, speedReturnSpeed);
        }

        Destroy(gameObject);
    }
}
