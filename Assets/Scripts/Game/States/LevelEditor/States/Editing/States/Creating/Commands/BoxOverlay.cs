using UnityEngine;

public class BoxOverlay : MonoBehaviour {

    public static BoxOverlay Instance { get { return GetInstance(); } }

    public bool ShowBoxOverlay { get { return enabled; } set { enabled = value; } }

    private static BoxOverlay instance;

    [SerializeField] private Color color = Color.blue;

    private Material lineMaterial;

    private new bool enabled;

    private Vector2 topRightCorner;
    private Vector2 bottomRightCorner;
    private Vector2 bottomLeftCorner;
    private Vector2 topLeftCorner;
    private float yLength;

    public void UpdateBox(Vector2 bottomLeftCorner, Vector2 topRightCorner) {
        this.bottomLeftCorner = bottomLeftCorner;
        this.topRightCorner = topRightCorner;

        float xLength = topRightCorner.x - bottomLeftCorner.x;

        bottomRightCorner = new Vector2(bottomLeftCorner.x + xLength, bottomLeftCorner.y);
        topLeftCorner = new Vector2(topRightCorner.x - xLength, topRightCorner.y);
    }

    private void CreateLineMaterial() {
        if (!lineMaterial) {
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader) {
                hideFlags = HideFlags.HideAndDontSave
            };
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }

    private void OnPostRender() {
        if (!enabled) { return; }

        CreateLineMaterial();
        lineMaterial.SetPass(0);

        GL.Begin(GL.LINES);
        GL.Color(color);

        //right
        GL.Vertex(topRightCorner);
        GL.Vertex(bottomRightCorner);

        //left
        GL.Vertex(topLeftCorner);
        GL.Vertex(bottomLeftCorner);

        //top
        GL.Vertex(topRightCorner);
        GL.Vertex(topLeftCorner);

        //bottom
        GL.Vertex(bottomRightCorner);
        GL.Vertex(bottomLeftCorner);

        GL.End();
    }

    private static BoxOverlay GetInstance() {
        if (instance == null) {
            instance = FindObjectOfType<BoxOverlay>();
        }
        return instance;
    }

}
