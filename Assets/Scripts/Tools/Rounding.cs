using UnityEngine;
using System.Collections;

public class Rounding {

    /// <summary>
    /// Round the float to the highest, or lowest int, depeding on if the float is negative or positive.
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public static int InvertOnNegativeCeil(float f)
    {
        return f > 0 ? Mathf.CeilToInt(f) : Mathf.FloorToInt(f);
    }
}
