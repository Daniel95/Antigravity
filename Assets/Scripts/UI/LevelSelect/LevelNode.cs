using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNode : MonoBehaviour {

    private SpriteRenderer _spriteRenderer;

    private LoadInput _loadSceneOnClick;

    private LevelLoader _levelLoader;

    public LevelNodeStatus Status { get; set; }

    public int LevelNumber { get; private set; }

    private void GetReferences()
    {
        _levelLoader = FindObjectOfType(typeof(LevelLoader)) as LevelLoader;
        _loadSceneOnClick = GetComponent<LoadInput>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void GetLevelNodeValues(LevelNodeStatus status, int levelNumber)
    {
        this.Status = status;
        this.LevelNumber = levelNumber;
    }

    /// <summary>
    /// Apply the status of this node.
    /// </summary>
    /// <param name="status"></param>
    /// <param name="levelNumber"></param>
    public void ActivateLevelNode()
    {
        GetReferences();

        if (_levelLoader.LevelExists(LevelNumber))
        {
            if (Status != 0)
            {
                _loadSceneOnClick.LevelIndex = LevelNumber;
                _loadSceneOnClick.Unlocked = true;
            }

            _spriteRenderer.color = LevelStatusAppearances.LevelStatusColors[(int)Status];
        }
        else
        {
            _loadSceneOnClick.Unlocked = false;

            _spriteRenderer.color = LevelStatusAppearances.LevelStatusColors[3];
        }

    }
}
