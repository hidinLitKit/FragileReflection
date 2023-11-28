using FragileReflection;
using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEditor.UIElements;
using UnityEngine;

[ExecuteInEditMode]
public class EnemyController : MonoBehaviour
{
    public Transform[] patrolEndpoints;

    [Header("Enemy settings")]
    [SerializeField] private EnemyConfig config;

    [Header("Enemy detecting zone settings")]
    [SerializeField] private float _height;
    [SerializeField] private float _viewRadius;
    [SerializeField] private float _viewAngle;
    [SerializeField] private Color meshColor;

    [Header("Need component")]
    public Animator animator;
    [SerializeField] private BehaviourTree tree;

    [HideInInspector]
    public Transform player;
    [HideInInspector]
    public bool stuggled = false;

    
    private float _attackDamage;
    private float _attackDistance;
    private float _moveSpeed;
    private float _chaseSpeed;

    private EnemyAttackDetector _attackArea;

    private Mesh _mesh;

    private int scanFrequency = 30;
    private float scanDelay = 5f;

    private Collider[] _colliders = new Collider[10];
    private List<GameObject> _Objects = new List<GameObject>();
    private float _scanInterval;
    private float _scanTimer;

    private void Awake()
    {
        SetConfigSettings();
    }

    private void Start()
    {
        _attackArea = GetComponent<EnemyAttackDetector>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (animator == null)
            Debug.LogWarning("Enemy controller: have not animator");
        if (tree == null)
            Debug.LogWarning("Enemy controller: have not tree");

        tree.blackboard.chaseSpeed = _chaseSpeed;
        tree.blackboard.moveSpeed = _moveSpeed;
        tree.blackboard.attackDistance = _attackDistance;

        _scanInterval = 1.0f / scanFrequency;
    }

    private void Update()
    {
        _scanTimer -= Time.deltaTime;
        if(_scanTimer < 0)
        {
            _scanTimer += _scanInterval;
            Scan();
        }
    }

    private void Scan()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, _viewRadius, _colliders, config.playerLayer, QueryTriggerInteraction.Collide);

        _Objects.Clear();
        for(int i = 0; i<count; i++)
        {
            GameObject obj = _colliders[i].gameObject;
            if(IsInSight(obj))
            {
                DetectPlayer();
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

        if(direction.y < 0 || direction.y > _height)
            return false;

        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if(deltaAngle > _viewAngle)
            return false;

        origin.y += _height / 2;
        dest.y = origin.y;

        if(Physics.Linecast(origin, dest, config.obstacleLayers))
            return false;

        return true;
    }

    public void AttackPlayer()
    {
        if(_attackArea.hasAttacked)
        {
            IDamagable damage = player.gameObject.GetComponent<IDamagable>();
            damage.TakeDamage(_attackDamage);
            Debug.Log($"Take damage {_attackDamage}");
        }
    }

    public bool CanSee()
    {
        return _Objects.Count>0;
    }

    public bool CanAttackPlayer()
    {
        bool canAttack = (_Objects.Count > 0 && (Vector3.Distance(transform.position, _Objects[0].transform.position) <= _attackDistance));
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
        Vector3 bottomLeft = Quaternion.Euler(0, -_viewAngle, 0) * Vector3.forward * _viewRadius;
        Vector3 bottomRight = Quaternion.Euler(0, _viewAngle, 0) * Vector3.forward * _viewRadius;

        Vector3 topCenter = bottomCenter + Vector3.up * _height;
        Vector3 topLeft = bottomLeft + Vector3.up * _height;
        Vector3 topRight = bottomRight + Vector3.up * _height;

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

        float currentAngle = -_viewAngle;
        float deltaAngle = (_viewAngle * 2) / segments;
        for(int i = 0; i< segments; i++)
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * _viewRadius;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * _viewRadius;

            topRight = bottomRight + Vector3.up * _height;
            topLeft = bottomLeft + Vector3.up * _height;

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
        _mesh = CreateWedgeMesh();
        _scanInterval = 1.0f / scanFrequency;
    }

    private void OnDrawGizmos()
    {
       if(_mesh)
       {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(_mesh, transform.position, transform.rotation);
       }
    }

    public void DetectPlayer()
    {
        _Objects.Add(player.gameObject);
        _scanTimer = scanDelay;
    }

    public void Die()
    {
        StartCoroutine("DieTimer");
    }

    private IEnumerator DieTimer()
    {
        animator.SetTrigger("Death");
        if (gameObject.TryGetComponent<BehaviourTreeRunner>(out BehaviourTreeRunner runner))
        {
            runner.enabled = false;
        }
        yield return new WaitForSeconds(config.deathDuration);
        Destroy(gameObject);
    }

    private void SetConfigSettings()
    {
            _attackDamage = config.attackDamage;
            _attackDistance = config.attackDistance;
            _moveSpeed = config.moveSpeed;
            _chaseSpeed = config.chaseSpeed;
    }
}

