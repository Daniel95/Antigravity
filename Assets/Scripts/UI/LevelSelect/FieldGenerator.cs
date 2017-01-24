using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldGenerator : MonoBehaviour
{
    /// <summary>
    /// Makes a field of nodes with a counter.
    /// </summary>
    /// <param name="nodeSize"></param>
    /// <param name="borderSize"></param>
    /// <param name="countingStartDirection"></param>
    /// <param name="spiralCounting"></param>
    /// <param name="startCountingValue"></param>
    /// <param name="horizontalRows"></param>
    /// <param name="fieldSize"></param>
    public List<Node> GenerateField(Vector2 fieldSize, Vector2 nodeSize, Vector2 borderSize, Vector2 countingStartDirection, bool spiralCounting, int startCountingValue, bool horizontalRows)
    {
        List<Node> nodes = new List<Node>();

        Func<int, int, int> getXPos = GetXPosMethod((int)countingStartDirection.x);
        Func<int, int, int> getYPos = GetYPosMethod((int)countingStartDirection.y);

        var getCounter = horizontalRows ? (Func<int, int, Vector2, int>)CalcHorizontalCounter : CalcVerticalCounter;

        for (int yIndex = 0; yIndex < fieldSize.y; yIndex++)
        {
            for (int xIndex = 0; xIndex < fieldSize.x; xIndex++)
            {
                Vector2 position = new Vector2(getXPos(xIndex, (int)fieldSize.x) * nodeSize.x, getYPos(yIndex, (int)fieldSize.y) * nodeSize.y);
                //add the borders
                position += new Vector2(position.x * borderSize.x, position.y * borderSize.y);

                int count = startCountingValue + getCounter(xIndex, yIndex, fieldSize);

                nodes.Add(new Node(position, count));

                if (!horizontalRows && spiralCounting)
                {
                    countingStartDirection.y *= -1;
                    getYPos = GetYPosMethod((int)countingStartDirection.y);
                }
            }

            if (horizontalRows && spiralCounting)
            {
                countingStartDirection.x *= -1;
                getXPos = GetXPosMethod((int)countingStartDirection.x);
            }
        }

        return nodes;
    }

    private static Func<int, int, int> GetXPosMethod(int xCountDir)
    {
        return xCountDir != 1 ? (Func<int, int, int>)CalcXPos : CalcInvertedXPos;
    }

    private static Func<int, int, int> GetYPosMethod(int yCountDir)
    {
        return yCountDir != 1 ? (Func<int, int, int>)CalcYPos : CalcInvertedYPos;
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

    public struct Node
    {
        public Vector2 Position;
        public int Counter;

        public Node(Vector2 position, int counter)
        {
            this.Position = position;
            this.Counter = counter;
        }
    }
}