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
    [SerializeField] private EnemyAttackDetector attackArea;

    public Transform[] patrolEndpoints;
    public Animator animator;
    public Transform player;
    public float attackDamage;
    public float viewRadius;
    public float viewAngle;
    public float height;
    public float attackDistance;
    public float moveSpeed;
    public float chaseSpeed;
    public bool stuggled = false;
    [SerializeField] private float speedRotation;
    [SerializeField] private BehaviourTree tree;

    Mesh mesh;
    [SerializeField] private Color meshColor;

    public int scanFrequency = 30;
    public float scanDelay = 5f;
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
        tree.blackboard.attackDistance = attackDistance;

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
    }

    private void Scan()
    {
        count = Physics.OverlapSphereNonAlloc(transform.position, viewRadius, colliders, layers, QueryTriggerInteraction.Collide);

        Objects.Clear();
        for(int i = 0; i<count; i++)
        {
            GameObject obj = colliders[i].gameObject;
            if(IsInSight(obj))
            {
                Objects.Add(obj);
                scanTimer = scanDelay;
            }
                
        }
    }

    public bool IsStuggled()
    {
        return stuggled;
    }

    public bool IsInSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;

        if(direction.y < 0 || direction.y > height)
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

    public void AttackPlayer()
    {
        if(attackArea.hasAttacked)
        {
            IDamagable damage = player.gameObject.GetComponent<IDamagable>();
            damage.TakeDamage(attackDamage);
            Debug.Log($"Take damage {attackDamage}");
        }
    }

    public bool CanSee()
    {
        return Objects.Count>0;
    }

    public bool CanAttackPlayer()
    {
        bool canAttack = (Objects.Count > 0 && (Vector3.Distance(transform.position, Objects[0].transform.position) < attackDistance));
        return canAttack;
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
    }

    public void DetectPlayer()
    {
        Objects.Add(player.gameObject);
        scanTimer = scanDelay;
    }

    public void Die()
    {
        StartCoroutine("DieTimer");
    }

    private IEnumerator DieTimer()
    {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}

