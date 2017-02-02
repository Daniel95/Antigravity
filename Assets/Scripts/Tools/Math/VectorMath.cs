using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMath : MonoBehaviour {

    public static Vector2 MultiplyV2(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.x * v2.x, v1.x * v2.y);
    }

    public static Vector2 MultiplyV3(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x * v2.x, v1.x * v2.y, v1.z * v2.z);
    }

    public static Vector2 InvertedMultiplyV2(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.x *- v2.x, v1.x *- v2.y);
    }

    public static Vector2 InvertedMultiplyV3(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x *- v2.x, v1.x *- v2.y, v1.z *- v2.z);
    }

    //returns positive when to the right (local) and negative when to the left
    public static float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        //right vector
        Vector3 right = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(right, up);

        return dir;
    }
}
