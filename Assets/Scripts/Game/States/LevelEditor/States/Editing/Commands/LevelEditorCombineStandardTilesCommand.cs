using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorCombineStandardTilesCommand : Command {

    [Inject] private LevelContainerStatus levelContainerStatus;

    private const string LEVEL_COLLIDER_PATH = "LevelEditor/LevelCollider";
    private const string LEVEL_VISUAL_PATH = "LevelEditor/LevelVisual";

    protected override void Execute() {
        GameObject levelColliderPrefab = Resources.Load<GameObject>(LEVEL_COLLIDER_PATH);
        GameObject levelVisualPrefab = Resources.Load<GameObject>(LEVEL_VISUAL_PATH);

        GameObject levelColliderGameObject = Object.Instantiate(levelColliderPrefab, levelContainerStatus.LevelContainer);

        Dictionary<Vector2, Tile> grid = LevelEditorTileGrid.Instance.TileGrid;

        List<GameObject> gameObjectCollidersToCombine = new List<GameObject>();
        List<GameObject> gameObjectSpriteRenderersToCombine = new List<GameObject>();
        foreach (Tile tile in grid.Values) {
            gameObjectCollidersToCombine.Add(tile.GameObject);
            if (tile.TileType != TileType.Standard) { continue; }
            gameObjectSpriteRenderersToCombine.Add(tile.GameObject);
        }

        List<SpriteRenderer> spriteRenderersToCombine = new List<SpriteRenderer>();
        List<BoxCollider2D> boxCollidersToCombine = new List<BoxCollider2D>();

        foreach (GameObject gameObjectColliderToCombine in gameObjectCollidersToCombine) {
            BoxCollider2D boxColliderToCombine = gameObjectColliderToCombine.GetComponent<BoxCollider2D>();
            if(boxColliderToCombine != null) {
                boxCollidersToCombine.Add(boxColliderToCombine);
            } else {
                gameObjectColliderToCombine.transform.SetParent(levelColliderGameObject.transform);
            }
        }

        foreach (GameObject gameObjectSpriteRendererToCombine in gameObjectSpriteRenderersToCombine) {
            SpriteRenderer spriteRendererToCombine = gameObjectSpriteRendererToCombine.GetComponent<SpriteRenderer>();
            spriteRenderersToCombine.Add(spriteRendererToCombine);
        }

        CompositeCollider2D compositeCollider = levelColliderGameObject.GetComponent<CompositeCollider2D>();
        ColliderHelper.CombineBoxCollidersInCompositeCollider(boxCollidersToCombine, compositeCollider);

        GameObject levelVisualGameObject = Object.Instantiate(levelVisualPrefab, levelColliderGameObject.transform);
        Material material = levelVisualGameObject.GetComponent<Material>();
        SpriteRenderer spriteRenderer = levelVisualGameObject.GetComponent<SpriteRenderer>();

        SpriteHelper.CombineSpritesOfGameObjects(spriteRenderersToCombine, material, spriteRenderer);

        foreach (GameObject gameObjectSpriteRenderer in gameObjectSpriteRenderersToCombine) {
            Object.Destroy(gameObjectSpriteRenderer);
        }
    }
}