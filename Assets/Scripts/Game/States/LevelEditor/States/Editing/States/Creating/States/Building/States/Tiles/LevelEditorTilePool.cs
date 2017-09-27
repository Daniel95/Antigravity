using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class LevelEditorTilePool : MonoBehaviour {

    public static LevelEditorTilePool Instance { get { return GetInstance(); } }

    private static LevelEditorTilePool instance;

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

    private static LevelEditorTilePool GetInstance() {
        if(instance == null) {
            instance = FindObjectOfType<LevelEditorTilePool>();
        }
        return instance;
    }

}
