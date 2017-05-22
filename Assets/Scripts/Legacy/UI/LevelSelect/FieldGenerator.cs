using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    /// <summary>
    /// Makes a field of nodes with an adjustable counter.
    /// </summary>
    /// <param name="countingStartDirection"></param>
    /// <param name="spiralCounting"></param>
    /// <param name="startCountingValue"></param>
    /// <param name="horizontalRows"></param>
    /// <param name="fieldSize"></param>
    public List<Node> GenerateField(Vector2 fieldSize, Vector2 countingStartDirection, bool spiralCounting, int startCountingValue, bool horizontalRows)
    {
        List<Node> nodes = new List<Node>();

        //Choose the right Positioning methods (normal or inverted), depending on our counting direction.
        Func<int, int, int> getXPos = GetXPosMethod((int)countingStartDirection.x);
        Func<int, int, int> getYPos = GetYPosMethod((int)countingStartDirection.y);

        Func<int, int, Vector2, int> getCounter = horizontalRows ? (Func<int, int, Vector2, int>)CalcHorizontalCounter : CalcVerticalCounter;

        for (int yIndex = 0; yIndex < fieldSize.y; yIndex++)
        {
            for (int xIndex = 0; xIndex < fieldSize.x; xIndex++)
            {
                Vector2 position = new Vector2(getXPos(xIndex, (int)fieldSize.x), getYPos(yIndex, (int)fieldSize.y));

                int count = startCountingValue + getCounter(xIndex, yIndex, fieldSize) - 1;

                //print(count);

                nodes.Add(new Node(position, count));

                //If we use spiralcounting, and we have vertical rows, switch the YPos Method and counting direction each row
                if (horizontalRows || !spiralCounting)
                    continue;

                countingStartDirection.y *= -1;
                getYPos = GetYPosMethod((int)countingStartDirection.y);
            }

            //If we use spiralcounting, and we have horizontal rows, switch the XPos Method and counting direction each row
            if (!horizontalRows || !spiralCounting)
                continue;

            countingStartDirection.x *= -1;
            getXPos = GetXPosMethod((int)countingStartDirection.x);
        }

        return nodes;
    }

    /// <summary>
    /// Returns the normal or inverted X Pos Method
    /// </summary>
    /// <param name="xCountDir"></param>
    /// <returns></returns>
    private static Func<int, int, int> GetXPosMethod(int xCountDir)
    {
        return xCountDir != 1 ? (Func<int, int, int>)CalcXPos : CalcInvertedXPos;
    }

    /// <summary>
    /// Returns the normal or inverted Y Pos Method
    /// </summary>
    /// <param name="yCountDir"></param>
    /// <returns></returns>
    private static Func<int, int, int> GetYPosMethod(int yCountDir)
    {
        return yCountDir != 1 ? (Func<int, int, int>)CalcYPos : CalcInvertedYPos;
    }

    private static int CalcXPos(int xIndex, int length)
    {
        return xIndex;
    }

    private static int CalcInvertedXPos(int xIndex, int length)
    {
        return length - 1 - xIndex;
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
    /// Calculates a horizontal counter.
    /// </summary>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    private static int CalcHorizontalCounter(int xIndex, int yIndex, Vector2 size)
    {
        return Mathf.RoundToInt(xIndex + yIndex * size.x + 1);
    }

    /// <summary>
    /// Calculates a vertical counter.
    /// </summary>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    private static int CalcVerticalCounter(int xIndex, int yIndex, Vector2 size)
    {
        return Mathf.RoundToInt(yIndex + xIndex * size.y + 1);
    }

    /// <summary>
    /// Used to save the position and counter of the nodes.
    /// </summary>
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