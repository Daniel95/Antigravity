using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDirIndicator : MonoBehaviour {

    [SerializeField]
    private Transform indicator;

    [SerializeField]
    private float distance;

    public void SetDragDir(Vector2 dir)
    {
        indicator.position =  new Vector3(transform.position.x + dir.x * distance, transform.position.y + dir.y * distance, transform.position.z - 1);
    }
}
