using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMath : MonoBehaviour {

    //returns positive when to the right (local) and negative when to the left
    public static float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        //right vector
        Vector3 right = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(right, up);

        return dir;
    }
}
