using IoCPlus;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelEditorCombineStandardTilesCommand : Command {

    [Inject] private LevelContainerStatus levelContainerStatus;

    private const string LEVEL_COLLIDER_PATH = "LevelEditor/LevelCollider";
    private const string LEVEL_VISUAL_PATH = "LevelEditor/LevelVisual";

    protected override void Execute() {
        GameObject levelColliderPrefab = Resources.Load<GameObject>(LEVEL_COLLIDER_PATH);
        GameObject levelColliderGameObject = Object.Instantiate(levelColliderPrefab, levelContainerStatus.LevelContainer);

        CombineColliders(levelColliderGameObject);
        SplitLevelIntoRectanglesAndCombineSprites(levelColliderGameObject);
    }

    private void SplitLevelIntoRectanglesAndCombineSprites(GameObject parent) {
        GameObject levelVisualPrefab = Resources.Load<GameObject>(LEVEL_VISUAL_PATH);

        Dictionary<Vector2, GameObject> standardTileGrid = new Dictionary<Vector2, GameObject>();
        foreach (KeyValuePair<Vector2, Tile> gridValue in LevelEditorTileGrid.Instance.TileGrid) {
            if (gridValue.Value.TileType != TileType.Standard) { continue; }
            standardTileGrid.Add(gridValue.Key, gridValue.Value.GameObject);
        }

        List<List<Vector2>> rectangles = GridHelper.SplitIntoRectangles(standardTileGrid.Keys.ToList());

        foreach (List<Vector2> rectangle in rectangles) {
            List<SpriteRenderer> spriteRenderersToCombine = new List<SpriteRenderer>();
            foreach (Vector2 rectanglePosition in rectangle) {
                SpriteRenderer spriteRenderer = standardTileGrid[rectanglePosition].GetComponent<SpriteRenderer>();
                spriteRenderersToCombine.Add(spriteRenderer);
            }

            CombineSprites(levelVisualPrefab, parent, spriteRenderersToCombine);
        }

        foreach (GameObject standardTileGameObject in standardTileGrid.Values) {
            Object.Destroy(standardTileGameObject);
        }
    }

    private static void CombineSprites(GameObject levelVisualPrefab, GameObject parent, List<SpriteRenderer> spriteRenderers) {
        GameObject levelVisualGameObject = Object.Instantiate(levelVisualPrefab, parent.transform);
        Material material = levelVisualGameObject.GetComponent<Material>();
        SpriteRenderer spriteRenderer = levelVisualGameObject.GetComponent<SpriteRenderer>();

        SpriteHelper.CombineSpritesOfGameObjects(spriteRenderers, material, spriteRenderer);
    }

    private static void CombineColliders(GameObject parent) {
        List<GameObject> gameObjectCollidersToCombine = LevelEditorTileGrid.Instance.TileGrid.Values.Select(x => x.GameObject).ToList();

        List<BoxCollider2D> boxCollidersToCombine = new List<BoxCollider2D>();
        foreach (GameObject gameObjectColliderToCombine in gameObjectCollidersToCombine) {
            BoxCollider2D boxColliderToCombine = gameObjectColliderToCombine.GetComponent<BoxCollider2D>();
            if (boxColliderToCombine != null) {
                boxCollidersToCombine.Add(boxColliderToCombine);
            } else {
                gameObjectColliderToCombine.transform.SetParent(parent.transform);
            }
        }

        CompositeCollider2D compositeCollider = parent.GetComponent<CompositeCollider2D>();
        ColliderHelper.CombineBoxCollidersInCompositeCollider(boxCollidersToCombine, compositeCollider);
    }

}