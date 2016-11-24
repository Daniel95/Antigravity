using UnityEngine;

public class HideSpriteOnAwake : MonoBehaviour {

    void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
