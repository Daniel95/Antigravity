using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject nodePrefab;

    /// <summary>
    /// Makes a field of nodes with a counter.
    /// </summary>
    /// <param name="size"></param>
    /// <param name="countingStartDirection"></param>
    /// <param name="buildPosition"></param>
    public void BuildRows(Vector2 buildPosition, Vector2 size, Vector2 countingStartDirection, bool horizontalRows)
    {
        var getXPos = countingStartDirection.x != 1 ? (Func<int, int, int>) CalcXPos : CalcInvertedXPos;
        var getYPos = countingStartDirection.y != 1 ? (Func<int, int, int>) CalcYPos : CalcInvertedYPos;

        var getCounter = horizontalRows ? (Func<int, int, Vector2, int>)CalcHorizontalCounter : CalcVerticalCounter;

        int counter = 0;

        for (int yIndex = 0; yIndex < size.y; yIndex++)
        {
            for (int xIndex = 0; xIndex < size.x; xIndex++)
            {
                Vector2 position = new Vector2(getXPos(xIndex, (int)size.x), getYPos(yIndex, (int)size.y));

                counter++;

                MakeLevelNode(buildPosition, position, getCounter(xIndex, yIndex, size));//getCounter(position, size));
            }
        }
    }

    private static int CalcHorizontalCounter(int xIndex, int yIndex, Vector2 size)
    {
        return Mathf.RoundToInt(xIndex + yIndex * size.x + 1);
    }

    private static int CalcVerticalCounter(int xIndex, int yIndex, Vector2 size)
    {
        return Mathf.RoundToInt(yIndex + xIndex * size.y + 1);
    }

    private static int CalcXPos(int xIndex, int length)
    {
        return xIndex;
    }

    private static int CalcInvertedXPos(int xIndex, int length)
    {
        return length - 1- xIndex;
    }

    private static int CalcYPos(int yIndex, int length)
    {
        return yIndex;
    }

    private static int CalcInvertedYPos(int yIndex, int length)
    {
        return length - 1 - yIndex;
    }

    /// <summary>
    /// Makes a node a node on the specified position.
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="counter"></param>
    /// <param name="buildPosition"></param>
    private void MakeLevelNode(Vector2 buildPosition, Vector2 offset, int counter)
    {
        GameObject node = Instantiate(nodePrefab, buildPosition + offset, new Quaternion(0, 0, 0, 0));
        node.GetComponentInChildren<TextMesh>().text = counter.ToString();
    }
}
