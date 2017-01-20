using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelField : MonoBehaviour
{
    [SerializeField] private GameObject nodePrefab;

    /// <summary>
    /// Makes a field of levels that counts horizontally.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="startDir"></param>
    public void MakeHorizontalField(int width, int height, Vector2 startDir)
    {
        int counter = 0;

        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                counter++;
                MakeLevelNode(new Vector2(x,y), counter);
            }
        }
    }


    /// <summary>
    /// Makes a field of levels that counts vertically.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="startDir"></param>
    public void MakeVerticalField(int width, int height, Vector2 startDir)
    {
        int counter = 0;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                counter++;
                MakeLevelNode(new Vector2(x, y), counter);
            }
        }
    }

    /// <summary>
    /// Makes a node a node on the specified position.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="counter"></param>
    private void MakeLevelNode(Vector2 position, int counter)
    {
        GameObject node = Instantiate(nodePrefab, position, new Quaternion(0, 0, 0, 0)) as GameObject;
        node.GetComponent<Text>().text = counter.ToString();
    }
}
