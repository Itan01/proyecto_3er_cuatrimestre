using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class EnemyVision : MonoBehaviour
{
    [Header("FOV Settings")]
    [Range(1, 360)][SerializeField] private float viewAngle = 90f;
    [Range(1, 180)][SerializeField] private float verticalFOV = 60f;
    [SerializeField] private float viewRadius = 10f;
    [SerializeField] private int horizontalRayCount = 100;
    [SerializeField] private int verticalRayCount = 30;

    [Header("References")]
    private PlayerManager _player;
    [Header("Debug")]
    public bool drawGizmos = true;

    private Mesh visionMesh;
    [SerializeField] private SO_Layers _layer;
    private MeshFilter meshFilter;
    private MeshRenderer _meshRenderer;
    private Transform _headReference;

    private List<(Vector3 point, bool hitObstacle)> debugPoints = new();

    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        visionMesh = new Mesh { name = "Vision Mesh" };
        meshFilter.mesh = visionMesh;
        _headReference =transform;
        _player = GameManager.Instance.PlayerReference;
    }

    private void LateUpdate()
    {
        DrawFieldOfView();
        transform.forward = _headReference.forward;
    }
    private void DrawFieldOfView()
    {
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

                if (Physics.Raycast(origin, dir, out RaycastHit hit, viewRadius, _layer._everything, QueryTriggerInteraction.Ignore))
                {
                    debugPoints.Add((hit.point, true));
                }
                else
                {
                    debugPoints.Add((origin + dir * viewRadius, false));
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

    public bool CheckIfHasVIsion()
    {
        bool SeePlayer = false;
        if (!OnAngle(GameManager.Instance.PlayerReference.transform.position))
        {
            return SeePlayer;
        }
            
        Vector3 PlayerHead = _player.GetHeadPosition().position;
        Vector3 PlayerHips = _player.GetHipsPosition().position;
        Vector3 PlayerPosition = _player.transform.position;
        if (!Physics.Linecast(_headReference.position,PlayerHead,_layer._obstacles, QueryTriggerInteraction.Ignore) ||
            !Physics.Linecast(_headReference.position, PlayerHips, _layer._obstacles, QueryTriggerInteraction.Ignore) ||
           !Physics.Linecast(_headReference.position, PlayerPosition, _layer._obstacles, QueryTriggerInteraction.Ignore))
        {
            SeePlayer = true;
           // Debug.Log("Estoy Viendo Al Jugador");
        }
        return SeePlayer;
    }

    private bool OnAngle(Vector3 Position)
    {
        var Angle = Vector3.Angle(_headReference.forward, Position - _headReference.position);
        bool State = Angle < viewAngle * 0.5f;
        return State;
    }
    public void SetColorVision(Color Color)
    {
        _meshRenderer.material.SetColor("_Main_Color", Color);
    }
    private void OnDrawGizmosSelected()
    {
        //Vector3 PlayerHead = _player.GetHeadPosition().position;
        //Vector3 PlayerHips = _player.GetHipsPosition().position;
        //Vector3 PlayerPosition = _player.transform.position;
        //if (!Physics.Linecast(_headReference.position, PlayerHead, _layer._obstacles, QueryTriggerInteraction.Ignore))
        //    Gizmos.color = Color.green;
        //else
        //    Gizmos.color = Color.red;
        //Gizmos.DrawLine(_headReference.position, PlayerHead);
        //if (!Physics.Linecast(_headReference.position, PlayerHips, _layer._obstacles, QueryTriggerInteraction.Ignore))
        //    Gizmos.color = Color.green;
        //else
        //    Gizmos.color = Color.red;
        //Gizmos.DrawLine(_headReference.position, PlayerHips);
        //if (!Physics.Linecast(_headReference.position, PlayerPosition, _layer._obstacles, QueryTriggerInteraction.Ignore))
        //    Gizmos.color = Color.green;
        //else
        //    Gizmos.color = Color.red;
        //Gizmos.DrawLine(_headReference.position, PlayerPosition);

        //        if (!drawGizmos) return;


        //        Gizmos.color = visionColor;
        //        Gizmos.DrawWireSphere(transform.position, viewRadius);

        //        Vector3 left = DirFromAngle(-viewAngle / 2, false);
        //        Vector3 right = DirFromAngle(viewAngle / 2, false);

        //        Gizmos.DrawLine(transform.position, transform.position + left * viewRadius);
        //        Gizmos.DrawLine(transform.position, transform.position + right * viewRadius);

        //#if UNITY_EDITOR
        //        foreach (var (point, hit) in debugPoints)
        //        {
        //            Gizmos.color = hit ? Color.red : Color.green;
        //            Gizmos.DrawSphere(point, 0.05f);
        //        }
        //#endif
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
            if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, _layer._everything,QueryTriggerInteraction.Ignore))
            {
                return true;
            }
        }
        return false;
    }
    public void Activate()
    {
        _meshRenderer.enabled= true;
    }
    public void Deactivate() 
    {
        _meshRenderer.enabled = false;
    }
}