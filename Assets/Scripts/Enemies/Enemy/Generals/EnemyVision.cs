using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class EnemyVision : MonoBehaviour
{
    [Header("FOV Settings")]
    [Range(1, 360)] public float viewAngle = 90f; // Horizontal FOV
    [Range(1, 180)] public float verticalFOV = 60f; // Vertical FOV
    public float viewRadius = 10f;
    public int horizontalRayCount = 100;
    public int verticalRayCount = 30;

    [Header("Layer Masks")]
    public LayerMask obstacleMask;
    public LayerMask detectableMask;

    [Header("References")]
    private AbstractEnemy _scriptManager;
    private PlayerManager _player;
    private bool _playerDeath = false;

    [Header("Debug")]
    public bool drawGizmos = true;
    public Color visionColor = new Color(1, 1, 0, 0.3f);
    private bool _seePlayer = false;

    private Mesh visionMesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    [SerializeField] private Transform _headReference;

    private List<(Vector3 point, bool hitObstacle)> debugPoints = new();

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        visionMesh = new Mesh { name = "Vision Mesh" };
        meshFilter.mesh = visionMesh;

        if (Application.isPlaying)
        {
            _scriptManager = GetComponentInParent<AbstractEnemy>();
            _player = GameManager.Instance.PlayerReference;
        }
    }

    void LateUpdate()
    {
        if (!Application.isPlaying) return;

        _playerDeath = _player.IsPlayerDeath();
        DrawFieldOfView();
        _scriptManager.WatchingPlayer(_seePlayer);
        _seePlayer = false;
        GameManager.Instance.PlayerReference.SetCaptured(false);
        transform.forward = _headReference.forward;
    }

#if UNITY_EDITOR
    void Update()
    {
        if (!Application.isPlaying)
        {
            DrawFieldOfView();
        }
    }
#endif

    void DrawFieldOfView()
    {
        if (meshRenderer != null && Application.isPlaying)
        {
            switch (_scriptManager.GetMode())
            {
                case -1:
                case 0: visionColor = new Color(0, 1, 0, 0.3f); break;
                case 2:
                case 3: visionColor = new Color(1, 1, 0, 0.3f); break;
                case 1: visionColor = new Color(1, 0, 0, 0.3f); break;
            }

            meshRenderer.material.color = visionColor;
        }

        debugPoints.Clear();
        List<Vector3> vertices = new();
        List<int> triangles = new();

        vertices.Add(Vector3.zero); // centro del cono (local)
        int vertexIndex = 1;

        float halfHorizontalFOV = viewAngle / 2f;
        float halfVerticalFOV = verticalFOV / 2f;

        Vector3 origin = _headReference ? _headReference.position : transform.position;

        for (int v = 0; v <= verticalRayCount; v++)
        {
            float pitch = Mathf.Lerp(-halfVerticalFOV, halfVerticalFOV, (float)v / verticalRayCount);

            for (int h = 0; h <= horizontalRayCount; h++)
            {
                float yaw = Mathf.Lerp(-halfHorizontalFOV, halfHorizontalFOV, (float)h / horizontalRayCount);

                Quaternion rot = Quaternion.Euler(pitch, yaw, 0);
                Vector3 localDir = rot * Vector3.forward;
                Vector3 worldDir = transform.rotation * localDir;

                Vector3 point;
                RaycastHit hit;

                bool obstacleHit = Physics.Raycast(origin, worldDir, out hit, viewRadius, obstacleMask);
                point = obstacleHit ? hit.point : origin + worldDir * viewRadius;
                debugPoints.Add((point, obstacleHit));

                vertices.Add(transform.InverseTransformPoint(point));

                if (Application.isPlaying && Physics.Raycast(origin, worldDir, out hit, viewRadius, detectableMask))
                {
                    if (hit.collider.TryGetComponent<PlayerManager>(out PlayerManager script))
                    {
                        script.SetCaptured(true);
                        _seePlayer = true;
                        if (_scriptManager.GetMode() != 3 && _scriptManager.GetMode() != 1)
                        {
                            _scriptManager.EnterConfusedState();
                        }
                    }
                }

                if (v > 0 && h > 0)
                {
                    int a = vertexIndex - horizontalRayCount - 2;
                    int b = vertexIndex - horizontalRayCount - 1;
                    int c = vertexIndex;
                    int d = vertexIndex - 1;

                    triangles.Add(a);
                    triangles.Add(b);
                    triangles.Add(c);

                    triangles.Add(a);
                    triangles.Add(c);
                    triangles.Add(d);
                }

                vertexIndex++;
            }
        }

        visionMesh.Clear();
        visionMesh.SetVertices(vertices);
        visionMesh.SetTriangles(triangles, 0);
        visionMesh.RecalculateNormals();
    }

    void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        Gizmos.color = visionColor;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 left = DirFromAngle(-viewAngle / 2, false);
        Vector3 right = DirFromAngle(viewAngle / 2, false);

        Gizmos.DrawLine(transform.position, transform.position + left * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + right * viewRadius);

#if UNITY_EDITOR
        if (debugPoints.Count > 0)
        {
            foreach (var (point, hit) in debugPoints)
            {
                Gizmos.color = hit ? Color.red : Color.green;
                Gizmos.DrawSphere(point, 0.05f);
            }
        }
#endif
    }

    Vector3 DirFromAngle(float angleDegrees, bool global)
    {
        if (!global)
            angleDegrees += transform.eulerAngles.y;

        float rad = angleDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
    }

    public bool IsPlayerVisible()
    {
        if (_player == null) return false;

        Vector3 dirToPlayer = (_player.transform.position - transform.position).normalized;
        float dstToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2 && dstToPlayer < viewRadius)
        {
            if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
            {
                return true;
            }
        }
        return false;
    }
}
