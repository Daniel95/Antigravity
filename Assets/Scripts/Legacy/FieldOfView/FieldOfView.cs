using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FieldOfView : MonoBehaviour
{
    [SerializeField]
    private bool isStatic = true;

    [SerializeField]
    private float viewRadius = 10;

    [Range(0, 360)]
    [SerializeField]
    private float viewAngle = 360;

    [SerializeField]
    private LayerMask obstacleMask;

    [SerializeField]
    private float meshResolution = 1;

    [SerializeField]
    private int edgeResolveIterations = 4;

	[SerializeField]
    float edgeDstThreshold = 0.5f;

    private Mesh _viewMesh;

    void Start()
    {
        _viewMesh = new Mesh();
        _viewMesh.name = "View Mesh";
        _viewMesh.MarkDynamic();
        GetComponent<MeshFilter>().mesh = _viewMesh;

        if (!isStatic)
        {
            StartCoroutine(UpdateFov());
        }
        else
        {
            DrawFieldOfView();
        }
    }

    private IEnumerator UpdateFov() {
        while (true)
        {
            DrawFieldOfView();

            //null fires after standard update, so in this case has the same result as LateUpdate.
            yield return null;
        }
    }

    /// <summary>
    /// Draws a mesh to visualize the field of view.
    /// </summary>
    private void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector2> viewPoints = new List<Vector2>();
        ViewCastInfo oldViewCast = new ViewCastInfo ();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            
            ViewCastInfo newViewCast = ViewCast(angle);

            if (i > 0) {
				bool edgeDstThresholdExceeded = Mathf.Abs (oldViewCast.Dst - newViewCast.Dst) > edgeDstThreshold;
				if (oldViewCast.Hit != newViewCast.Hit || oldViewCast.Hit && edgeDstThresholdExceeded) {
					EdgeInfo edge = FindEdge (oldViewCast, newViewCast);
					if (edge.PointA != Vector2.zero) {
						viewPoints.Add (edge.PointA);
					}
					if (edge.PointB != Vector2.zero) {
						viewPoints.Add (edge.PointB);
					}
				}
			}

            viewPoints.Add(newViewCast.Point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;

        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        _viewMesh.Clear();
        _viewMesh.vertices = vertices;

        Vector2 [] uvs = new Vector2[vertices.Length];
		for (int i = 0; i < vertices.Length; i++) {
		    uvs[i] = new Vector2(vertices[i].x, vertices[i].y);		
		}
		_viewMesh.uv = uvs;

        _viewMesh.triangles = triangles;
        _viewMesh.RecalculateNormals();
    }

    /// <summary>
    /// Try to find a more precise edge by raycasting between the minViewCast and maxViewCast.
    /// </summary>
    /// <param name="minViewCast"></param>
    /// <param name="maxViewCast"></param>
    /// <returns></returns>
    private EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast) 
    {

		float minAngle = minViewCast.Angle;
		float maxAngle = maxViewCast.Angle;
		Vector2 minPoint = Vector2.zero;
		Vector2 maxPoint = Vector2.zero;

		for (int i = 0; i < edgeResolveIterations; i++) {
			float angle = (minAngle + maxAngle) / 2;
			ViewCastInfo newViewCast = ViewCast (angle);

			bool edgeDstThresholdExceeded = Mathf.Abs (minViewCast.Dst - newViewCast.Dst) > edgeDstThreshold;
			if (newViewCast.Hit == minViewCast.Hit && !edgeDstThresholdExceeded) {
				minAngle = angle;
				minPoint = newViewCast.Point;
			} else {
				maxAngle = angle;
				maxPoint = newViewCast.Point;
			}
		}

		return new EdgeInfo (minPoint, maxPoint);
	}

    private ViewCastInfo ViewCast(float globalAngle)
    {
        Vector2 dir = DirFromAngle(globalAngle, true);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, viewRadius, obstacleMask);

        if (hit)
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }

        return new ViewCastInfo(false, (Vector2)transform.position + dir * viewRadius, viewRadius, globalAngle);
    }
    
    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool Hit;
        public Vector2 Point;
        public float Dst;
        public float Angle;

        public ViewCastInfo(bool hit, Vector2 point, float dst, float angle)
        {
            Hit = hit;
            Point = point;
            Dst = dst;
            Angle = angle;
        }
    }

    public struct EdgeInfo {
		public Vector2 PointA;
		public Vector2 PointB;

		public EdgeInfo(Vector2 pointA, Vector2 pointB) {
			PointA = pointA;
            PointB = pointB;
		}
	}

    public float ViewRadius
    {
        get { return viewRadius; }
    }
}