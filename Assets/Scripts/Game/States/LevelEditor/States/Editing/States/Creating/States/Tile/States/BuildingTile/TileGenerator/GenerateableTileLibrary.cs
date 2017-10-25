﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityToolbag;

public class GenerateableTileLibrary : MonoBehaviour {

    public static List<GenerateableTileNode> GenerateableTiles { get { return GetInstance().generatableTiles; } }

    private static GenerateableTileLibrary instance;

    [SerializeField] [Reorderable] private List<GenerateableTileNode> generatableTiles = new List<GenerateableTileNode>();

    private const string GENERATABLE_TILE_LIBRARY_PATH = "LevelEditor/Libraries/GenerateableTileLibrary";

    public static GenerateableTileNode GetGeneratableTileNode(TileType tileType) {
        return GetInstance().generatableTiles.Find(x => x.TileType == tileType);
    }

    private static GenerateableTileLibrary GetInstance() {
        if (instance == null) {
            instance = Resources.Load<GenerateableTileLibrary>(GENERATABLE_TILE_LIBRARY_PATH);
        }
        return instance;
    }

    private void SetTilesGeneratorNodesBasedOnTileTypes() {
        Array enumArray = Enum.GetValues(typeof(TileType));
        int enumCount = enumArray.Length;

        if (generatableTiles.Count == enumCount) { return; }

        int index = 0;
        IEnumerable<TileType> TileTypeIEnumerable = enumArray.Cast<TileType>();

        foreach (TileType tileType in TileTypeIEnumerable) {
            if (generatableTiles.Count <= index) {
                generatableTiles.Insert(index, new GenerateableTileNode() {
                    TileType = tileType,
                });
            } else {
                generatableTiles[index].TileType = tileType;
            }
            index++;
        }
    }

    private void Reset() {
        SetTilesGeneratorNodesBasedOnTileTypes();
    }

    private void OnValidate() {
        SetTilesGeneratorNodesBasedOnTileTypes();
    }

}
