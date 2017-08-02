using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityToolbag;

public class TileGenerator : MonoBehaviour {

    public static TileGenerator Instance { get { return GetInstance(); } }
    public List<TileGeneratorNode> Tiles { get { return tileGeneratorNodes; } }

    private static TileGenerator instance;

    [SerializeField] [Reorderable] private List<TileGeneratorNode> tileGeneratorNodes = new List<TileGeneratorNode>();
    [SerializeField] private bool debugMode = false;

    private List<TileType> solidTileTypes = new List<TileType>();

    public Tile GenerateTile(Vector2 gridPosition) {
        TileGeneratorNode matchingTileGeneratorNode = null;

        if(debugMode) {
            Debug.Log("_____");
            Debug.Log("gridPos : " + gridPosition);
        }
        for (int i = tileGeneratorNodes.Count - 1; i >= 0; i--) {
            TileGeneratorNode tileGeneratorNode = tileGeneratorNodes[i];
            TileCondition falseCondition = tileGeneratorNode.TileConditions.Find(x => !x.Check(gridPosition));

            if (falseCondition == null) {
                matchingTileGeneratorNode = tileGeneratorNode;
                break;
            } else if (debugMode) {
                Debug.Log("false condition for " + tileGeneratorNode.TileType.ToString() + " : " + falseCondition.name);
            }
        }
        if (debugMode) {
            Debug.Log("TileType : " + matchingTileGeneratorNode.TileType);
        }

       Tile tile = GetTile(matchingTileGeneratorNode.Prefab, matchingTileGeneratorNode.TileType, gridPosition, Vector2.zero);
        return tile;
    }

    public bool CheckSolidTileType(TileType tileType) {
        bool isSolid = solidTileTypes.Contains(tileType);
        return isSolid;
    }

    private Tile GetTile(GameObject prefab, TileType tileType, Vector2 gridPosition, Vector2 direction) {
        if(tileType == TileType.Empty) { return new Tile() { TileType = TileType.Empty }; }

        Vector2 tilePosition = TileGrid.GridToTilePosition(gridPosition);

        GameObject tileGameObject = Instantiate(prefab, tilePosition, new Quaternion());
        bool isSolid = CheckSolidTileType(tileType);
        if(isSolid) {
            tileGameObject.name = "Solid " + tileType.ToString() + " " + gridPosition.ToString();
        } else {
            tileGameObject.name = tileType.ToString() + " " + gridPosition.ToString();
        }

        //tileGameObject.transform.forward = direction;

        Tile tile = new Tile() {
            TileType = tileType,
            GameObject = tileGameObject,
            IsSolid = isSolid,
        };

        return tile;
    }

    private void SetTilesBasedOnTileValue() {
        Array enumArray = Enum.GetValues(typeof(TileType));
        int enumCount = enumArray.Length;

        if (tileGeneratorNodes.Count == enumCount) { return; }

        int index = 0;
        IEnumerable<TileType> TileTypeIEnumerable = enumArray.Cast<TileType>();

        foreach (TileType tileType in TileTypeIEnumerable) {
            if (tileGeneratorNodes.Count <= index) {
                tileGeneratorNodes.Insert(index, new TileGeneratorNode() {
                    TileType = tileType,
                });
            } else {
                tileGeneratorNodes[index].TileType = tileType;
            }
            index++;
        }
    }

    private static TileGenerator GetInstance() {
        if (instance == null) {
            instance = GameObject.FindObjectOfType<TileGenerator>();
        }
        return instance;
    }

    private void Reset() {
        SetTilesBasedOnTileValue();
    }

    private void OnValidate() {
        SetTilesBasedOnTileValue();
    }


    private void Awake() {
        float tileWidth = tileGeneratorNodes.Find(x => x.TileType == TileType.Standard).Prefab.transform.localScale.x;
        TileGrid.SetTileSize(tileWidth);

        foreach (TileGeneratorNode tileGeneratorNode in tileGeneratorNodes) {
            if(!tileGeneratorNode.IsSolid) { continue; }
            solidTileTypes.Add(tileGeneratorNode.TileType);
        }
    }
}
