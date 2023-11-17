using FragileReflection;
using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class EnemyController : MonoBehaviour
{
    public Transform[] patrolEndpoints;
    public Animator animator;
    public Transform player;
    public float viewRadius;
    public float viewAngle;
    public float height;
    public float attackDistance;
    public float moveSpeed;
    public float chaseSpeed;
    [SerializeField] private float speedRotation;
    [SerializeField] private BehaviourTree tree;

    Mesh mesh;
    [SerializeField] private Color meshColor;

    public int scanFrequency = 30;
    public LayerMask layers;
    public LayerMask obstacleLayers;

    Collider[] colliders = new Collider[10];
    public List<GameObject> Objects = new List<GameObject>();
    int count;
    float scanInterval;
    float scanTimer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        tree.blackboard.chaseSpeed = chaseSpeed;
        tree.blackboard.moveSpeed = moveSpeed;

        scanInterval = 1.0f / scanFrequency;
    }

    private void Update()
    {
        scanTimer -= Time.deltaTime;
        if(scanTimer < 0)
        {
            scanTimer += scanInterval;
            Scan();
        }

        DetectPlayer(Objects.Count > 0);
        AttackPlayer(Objects.Count > 0 && (Vector3.Distance(transform.position, Objects[0].transform.position) < attackDistance));
    }

    private void Scan()
    {
        count = Physics.OverlapSphereNonAlloc(transform.position, viewRadius, colliders, layers, QueryTriggerInteraction.Collide);

        Objects.Clear();
        for(int i = 0; i<count; i++)
        {
            GameObject obj = colliders[i].gameObject;
            if(IsInSight(obj))
                Objects.Add(obj);
        }
    }

    public bool IsInSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;

        if(direction.y < 0 || direction.y> height)
            return false;

        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if(deltaAngle > viewAngle)
            return false;

        origin.y += height / 2;
        dest.y = origin.y;

        if(Physics.Linecast(origin, dest, obstacleLayers))
            return false;

        return true;
    }

    private void AttackPlayer(bool available)
    {
        //if (available == tree.blackboard.canAttack)
        //    return;

        //Debug.Log("attack " + available);

        ////transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, Objects[0].transform.position, speedRotation * Time.deltaTime, 0f));

        //tree.blackboard.canAttack = available;
        //animator.SetTrigger("Attack");
    }

    public bool CanSee()
    {
        return Objects.Count>0;
    }

    public bool CanAttackPlayer()
    {
        return Objects.Count > 0 && (Vector3.Distance(transform.position, Objects[0].transform.position) < attackDistance);
    }

    private void DetectPlayer(bool available)
    {
        //if (available == tree.blackboard.canSeePlayer)
        //    return;

        //tree.blackboard.canSeePlayer = available;
        //animator.SetBool("Run", available);
    }

    private Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -viewAngle, 0) * Vector3.forward * viewRadius;
        Vector3 bottomRight = Quaternion.Euler(0, viewAngle, 0) * Vector3.forward * viewRadius;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;

        #region meshCalc
        int vert = 0;

        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -viewAngle;
        float deltaAngle = (viewAngle * 2) / segments;
        for(int i = 0; i< segments; i++)
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * viewRadius;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * viewRadius;

            topRight = bottomRight + Vector3.up * height;
            topLeft = bottomLeft + Vector3.up * height;

            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;
        }

        for(int i = 0; i<numVertices; i++)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        #endregion

        return mesh;
    }

    private void OnValidate()
    {
        mesh = CreateWedgeMesh();
        scanInterval = 1.0f / scanFrequency;
    }

    private void OnDrawGizmos()
    {
       if(mesh)
       {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
       }

        Gizmos.color = Color.white;
        foreach (var obj in Objects)
        {
            Gizmos.DrawSphere(obj.transform.position, 0.2f);
        }
    }

}

