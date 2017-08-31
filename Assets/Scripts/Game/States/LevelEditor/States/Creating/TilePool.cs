using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class TilePool : MonoBehaviour {

    public static TilePool Instance { get { return GetInstance(); } }

    private static TilePool instance;

    private List<GameObject> tileGeneratorNodePrefabs = new List<GameObject>();

    private void Awake() {
        CreateTileEntries();
    }

    private void CreateTileEntries() {
        List<TileGeneratorNode> nonEmptyTileGeneratorNodes = TileGenerator.Instance.TileGeneratorNodes.FindAll(x => x.Prefab != null);
        nonEmptyTileGeneratorNodes.ForEach(x => tileGeneratorNodePrefabs.Add(x.Prefab));

        ObjectPool.Instance.Entries = new ObjectPool.ObjectPoolEntry[nonEmptyTileGeneratorNodes.Count];
        for (int i = 0; i < ObjectPool.Instance.Entries.Length; i++) {
            ObjectPool.Instance.Entries[i] = new ObjectPool.ObjectPoolEntry() {
                Prefab = nonEmptyTileGeneratorNodes[i].Prefab,
                Count = 100,
            };
        }
    }

    private static TilePool GetInstance() {
        if(instance == null) {
            instance = FindObjectOfType<TilePool>();
        }
        return instance;
    }

}
