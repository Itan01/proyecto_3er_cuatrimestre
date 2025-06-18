using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class EnemyVision : MonoBehaviour
{
    [Header("FOV Settings")]
    [Range(1, 360)]
    public float viewAngle = 90f;

    public float viewRadius = 10f;
    public int rayCount = 100;

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
    private bool _seePlayer=false;

    private Mesh visionMesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    [SerializeField] private Transform _headReference;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        visionMesh = new Mesh();
        visionMesh.name = "Vision Mesh";
        meshFilter.mesh = visionMesh;

        _scriptManager = GetComponentInParent<AbstractEnemy>();
        _player = GameManager.Instance.PlayerReference;
    }

    void LateUpdate()
    {
        _playerDeath = _player.IsPlayerDeath();
        DrawFieldOfView();
        _scriptManager.WatchingPlayer(_seePlayer);
        _seePlayer = false;
        GameManager.Instance.PlayerReference.SetCaptured(false);
        transform.forward= _headReference.forward;
    }

    void DrawFieldOfView()
    {
        switch (_scriptManager.GetMode())
        {
            case -1:
            case 0:
                visionColor = new Color(0, 1, 0, 0.3f); // verde
                break;
            case 2:
            case 3:
                visionColor = new Color(1, 1, 0, 0.3f); // amarillo
                break;
            case 1:
                visionColor = new Color(1, 0, 0, 0.3f); // rojo
                break;
        }

        if (meshRenderer != null)
        {
            meshRenderer.material.color = visionColor;
        }

        Vector3[] vertices = new Vector3[rayCount + 2];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = Vector3.zero;

        float angleStep = viewAngle / rayCount;
        float angle = -viewAngle / 2;

        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 dir = DirFromAngle(angle, false);
            Vector3 vertex;
            RaycastHit hit;

            Vector3 origin = transform.position;

            if (Physics.Raycast(origin, dir, out hit, viewRadius, obstacleMask))
            {
                vertex = transform.InverseTransformPoint(hit.point);
            }
            else
            {
                vertex = transform.InverseTransformPoint(origin + dir * viewRadius);
            }

            vertices[i + 1] = vertex;


            if (Physics.Raycast(origin, dir, out hit, viewRadius, detectableMask))
            {
                if (hit.collider.TryGetComponent<PlayerManager>(out PlayerManager script))
                {
                    script.SetCaptured(true);
                    _seePlayer = true; 
                    if (_scriptManager.GetMode() != 1 && _scriptManager.GetMode()!=3)
                    {
                        _scriptManager.EnterConfusedState();
                    }
                }
            }

            if (i < rayCount)
            {
                int index = i * 3;
                triangles[index] = 0;
                triangles[index + 1] = i + 1;
                triangles[index + 2] = i + 2;
            }

            angle += angleStep;
        }

        visionMesh.Clear();
        visionMesh.vertices = vertices;
        visionMesh.triangles = triangles;
        visionMesh.RecalculateNormals();
    }

    Vector3 DirFromAngle(float angleDegrees, bool global)
    {
        if (!global)
            angleDegrees += transform.eulerAngles.y;

        float rad = angleDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
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
    }

    public bool IsPlayerVisible()
    {
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
