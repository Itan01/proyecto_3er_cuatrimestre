using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class EnemyVision : MonoBehaviour
{
    [Header("FOV Settings")]
    [Range(1, 360)][SerializeField] private float viewAngle = 90f;
    [Range(1, 180)][SerializeField] private float verticalFOV = 60f;
    [SerializeField] private float viewRadius = 10f;
    [SerializeField] private int horizontalRayCount = 100;
    [SerializeField] private int verticalRayCount = 30;

    [Header("Layer Masks")]
    private LayerMask obstacleMask;
    private LayerMask detectableMask;

    [Header("References")]
    private AbstractEnemy _scriptManager;
    private PlayerManager _player;
    private bool _playerDeath = false;

    [Header("Debug")]
    public bool drawGizmos = true;
    public Color visionColor = new(1, 1, 0, 0.3f);
    private bool _seePlayer = false;

    private Mesh visionMesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    [SerializeField] private Transform _headReference;

    private List<(Vector3 point, bool hitObstacle)> debugPoints = new();

    void Start()
    {
        obstacleMask = LayerManager.Instance.GetLayerMask(EnumLayers.ObstacleMask);
        detectableMask = LayerManager.Instance.GetLayerMask(EnumLayers.ObstacleWithPlayerMask);
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
                case 0: visionColor = new Color(0, 1, 0, 0.1f); break;
                case 2:
                case 3: visionColor = new Color(1, 1, 0, 0.1f); break;
                case 1: visionColor = new Color(1, 0, 0, 0.1f); break;
            }

            meshRenderer.material.color = visionColor;
        }

        debugPoints.Clear();
        Vector3 origin = _headReference ? _headReference.position : transform.position;

        float halfHorizontalFOV = viewAngle / 2f;
        float halfVerticalFOV = verticalFOV / 2f;

        for (int v = 0; v <= verticalRayCount; v++)
        {
            float pitch = Mathf.Lerp(-halfVerticalFOV, halfVerticalFOV, (float)v / verticalRayCount);

            for (int h = 0; h <= horizontalRayCount; h++)
            {
                float yaw = Mathf.Lerp(-halfHorizontalFOV, halfHorizontalFOV, (float)h / horizontalRayCount);
                Quaternion rot = Quaternion.Euler(pitch, yaw, 0);
                Vector3 dir = _headReference.rotation * (rot * Vector3.forward);

                if (Physics.Raycast(origin, dir, out RaycastHit hit, viewRadius, obstacleMask, QueryTriggerInteraction.Ignore))
                {
                    debugPoints.Add((hit.point, true));
                }
                else
                {
                    debugPoints.Add((origin + dir * viewRadius, false));
                }

                if (Application.isPlaying &&
                    Physics.Raycast(origin, dir, out hit, viewRadius, detectableMask,QueryTriggerInteraction.Ignore))
                {
                    if (hit.collider.TryGetComponent<PlayerManager>(out PlayerManager script))
                    {
                        if (_playerDeath ||GameManager.Instance.PlayerReference.GetInvisible()) return;
                        _seePlayer = true;
                        if (_scriptManager.GetMode() != 3 && _scriptManager.GetMode() != 1 && _scriptManager.GetMode() != 6)
                            _scriptManager.SetModeByIndex(3);
                    }
                }
            }
        }

        // MESH PLANO usando raycasts ya hechos (fila del medio)
        List<Vector3> vertices = new() { Vector3.zero };
        List<int> triangles = new();

        int rowLength = horizontalRayCount + 1;
        int startIndex = (verticalRayCount / 2) * rowLength;

        for (int i = 0; i <= horizontalRayCount; i++)
        {
            var (point, _) = debugPoints[startIndex + i];
            Vector3 localPoint = transform.InverseTransformPoint(point);
            vertices.Add(localPoint);

            if (i < horizontalRayCount)
            {
                int index = i * 3;
                triangles.Add(0);
                triangles.Add(i + 1);
                triangles.Add(i + 2);
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
        foreach (var (point, hit) in debugPoints)
        {
            Gizmos.color = hit ? Color.red : Color.green;
            Gizmos.DrawSphere(point, 0.05f);
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