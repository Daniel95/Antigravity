using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDirIndicator : MonoBehaviour {

    [SerializeField]
    private Transform indicator;

    [SerializeField]
    private float distance;

    public void SetDragDir(Vector2 _dir)
    {
        indicator.position =  new Vector3(transform.position.x + _dir.x * distance, transform.position.y + _dir.y * distance, transform.position.z - 1);
    }
}
