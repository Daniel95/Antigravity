using UnityEngine;
using System.Collections;

public class Rounding {

    //round the float to the highest, or lowest int, depeding on if the float is negative or positive
    public static int InvertOnNegativeCeil(float _float)
    {
        if (_float > 0)
        {
            return Mathf.CeilToInt(_float);
        }
        else
        {
            return Mathf.FloorToInt(_float);
        }
    }

    //round the float to the highest, or lowest int, depeding on if the float is negative or positive
    public static int InvertOnNegativeRound(float _float)
    {
        if (_float > 0)
        {
            return Mathf.RoundToInt(_float);
        }
        else
        {
            return Mathf.RoundToInt(_float);
        }
    }

    //round the float to the highest, or lowest int, depeding on if the float is negative or positive
    public static int InvertOnNegativeFloor(float _float)
    {
        if (_float > 0)
        {
            return Mathf.FloorToInt(_float);
        }
        else
        {
            return Mathf.FloorToInt(_float);
        }
    }
}
