using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TilePrefabTypeContainer : MonoBehaviour {

    public static TilePrefabTypeContainer Instance { get { return GetInstance(); } }
    public List<TilePrefabType> Tiles { get { return tiles; } }

    [SerializeField] private List<TilePrefabType> tiles = new List<TilePrefabType>();

    private static TilePrefabTypeContainer instance;

    public GameObject GetPrefabByTileType(TileType tileType) {
        TilePrefabType tile = tiles.Find(x => x.Type == tileType);

        if(tile == null) {
            Debug.LogWarning("No tile found with type " + tileType.ToString() + ".");
        } else if(tile.Prefab == null) {
            Debug.LogWarning("Tile of type " + tileType.ToString() + " doesn't have an Prefab assigned.");
        }

        return tile.Prefab;
    }

    private void SetTilesBasedOnTileValue() {
        Array values = Enum.GetValues(typeof(TileType));
        int enumContentCount = values.Length;
        if (tiles.Count == enumContentCount) { return; }
        IEnumerable<TileType> TileValuesIEnumerable = values.Cast<TileType>();

        int index = 0;
        foreach (TileType type in TileValuesIEnumerable) {
            if (tiles.Count <= index) {
                tiles.Insert(index, new TilePrefabType() {
                    Type = type,
                });
            } else {
                tiles[index].Type = type;
            }
            index++;
        }
    }

    private static TilePrefabTypeContainer GetInstance() {
        if (instance == null) {
            instance = GameObject.FindObjectOfType<TilePrefabTypeContainer>();
        }
        return instance;
    }

    private void Reset() {
        SetTilesBasedOnTileValue();
    }

    private void OnValidate() {
        SetTilesBasedOnTileValue();
    }
}
