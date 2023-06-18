using UnityEngine;

public class PatrollingAndChasingEnemy : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private EnemyAnimationsHandler _enemyAnimationsHandler;
    [SerializeField] private UnityEngine.AI.NavMeshAgent _navMeshAgent;
    [SerializeField] private Collider _enemyCollider;
    [SerializeField] private float _speedWalk;
    [SerializeField] private float _speedRun;
    [SerializeField] private float _viewRadius;
    [SerializeField] private float _viewAngle;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private LayerMask _obstacleMask;
    private GameObject _map;
    
    private Vector3 _currentWayPoint;

    private float _distanceToFeelPlayer = 10f;
    private float _timeToFeelPlayer = 3f;

    private float _waitTimeOnPatrolPoint = 2f;
    private float _waitTimeAfterLostPlayer = 5f;
    private float _meshResolution = 1f;
    private int _edgeIterations = 4;

    private int m_СurrentWayPointIndex;

    private Vector3 m_PlayerPosition;
    private float m_WaitTimeOnPatrolPoint;
    private float m_WaitTimeAfterLostPlayer;
    private bool m_IsPatrol;

   

    private void Start()
    {
        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_WaitTimeOnPatrolPoint = _waitTimeOnPatrolPoint;
        m_WaitTimeAfterLostPlayer = _waitTimeAfterLostPlayer;

        m_СurrentWayPointIndex = 0;

        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = _speedWalk;
        Move(_speedWalk);
        
        
        _currentWayPoint = RandomPointGenerator.GetRandomPointOnNavMesh(250f, transform.position);
        _navMeshAgent.SetDestination(_currentWayPoint);
    }

    private void Update()
    {
        EnviromentView();
        
        if (!m_IsPatrol)
        {
            Chasing();
        }
        else
        {
            Patroling();
        }
    }

    private void Chasing()
    {
        if (!_enemy.CanMove)
        {
            _navMeshAgent.isStopped = true;
            return;
        }
        
        Vector3 dirToPlayer = (Player.Instance.transform.position - transform.position).normalized;
        float dstToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);

        
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f, _playerMask);
            
        foreach (Collider collider in colliders)
        {
            _enemyAnimationsHandler.SetEnemyAnimation(EnemyAnimationsHandler.TypesOfAnimations.Attack);
            _enemy.SetCanMove(false);
        }
        
        if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, _obstacleMask))
        {
            Run(_speedRun);
            _navMeshAgent.SetDestination(m_PlayerPosition);
            m_WaitTimeAfterLostPlayer = _waitTimeAfterLostPlayer;
        }
        


        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance && Vector3.Distance(transform.position, _navMeshAgent.destination) <= _navMeshAgent.stoppingDistance)
        {
            Stop();
            if (m_WaitTimeAfterLostPlayer <= 0)
            {
                m_IsPatrol = true;
                Move(_speedWalk);
                m_WaitTimeAfterLostPlayer = _waitTimeAfterLostPlayer;
                /*_currentWayPoint = RandomPointGenerator.Instance.GetRandomPointOnNavMesh(10f, transform.position);*/
                _navMeshAgent.SetDestination(_currentWayPoint);
            }
            else
            {
                /*if (Vector3.Distance(transform.position,
                        Player.Instance.gameObject.transform.position) >= 2.5f)
                    Stop();*/
                m_WaitTimeAfterLostPlayer -= Time.deltaTime;
            }

            Debug.Log("m_WaitTime = " + m_WaitTimeAfterLostPlayer);
        }
    }

    private void Patroling()
    {
        if (Player.Instance == null)
            return;

        _navMeshAgent.SetDestination(_currentWayPoint);
        
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            if (m_WaitTimeOnPatrolPoint <= 0)
            {
                NextPoint();
                Move(_speedWalk);
                m_WaitTimeOnPatrolPoint = _waitTimeOnPatrolPoint;
            }
            else
            {
                Stop();
                m_WaitTimeOnPatrolPoint -= Time.deltaTime;
            }
        }
    }

    public void NextPoint()
    {
        /*_currentWayPoint = RandomPointGenerator.Instance.GetRandomPointOnNavMesh(10f, transform.position);*/
        _navMeshAgent.SetDestination(_currentWayPoint);
    }

    void Stop()
    {
        _enemyAnimationsHandler.SetEnemyAnimation(EnemyAnimationsHandler.TypesOfAnimations.Idle);
        _navMeshAgent.isStopped = true;
        _navMeshAgent.speed = 0;
    }

    void Move(float speed)
    {
        _enemyAnimationsHandler.SetEnemyAnimation(EnemyAnimationsHandler.TypesOfAnimations.Walk);
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = speed;
    }

    private void Run(float speed)
    {
        _enemyAnimationsHandler.SetEnemyAnimation(EnemyAnimationsHandler.TypesOfAnimations.Run);
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = speed;
    }

    private void EnviromentView()
    {
        if(Player.Instance == null)
            return;
        
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, _viewRadius, _playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float dstToPlayer = Vector3.Distance(transform.position, player.position);

            if (Vector3.Angle(transform.forward, dirToPlayer) < _viewAngle / 2)
            {
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, _obstacleMask))
                {
                    m_IsPatrol = false;
                }
            }
            else if (dstToPlayer <= _distanceToFeelPlayer)
            {
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, _obstacleMask))
                {
                    m_IsPatrol = false;
                }
            }

            if (!m_IsPatrol)
            {
                m_PlayerPosition = player.position;
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}
