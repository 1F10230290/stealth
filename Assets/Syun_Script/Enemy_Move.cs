using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    
    //状態
    private enum State
    {
        Patrol, //探索
        EnCounter   //追跡
    }
    public Animator Enemyanim;
    bool isRun;
    private State currentState = State.Patrol;
    
    //NavMesh
    private NavMeshAgent agent;
    //追跡対象
    private Transform target;

    // ===== 探索用 =====
    [Header("敵の探索")]
    [SerializeField] private float patrolRadius; //エネミーが動く距離
    [SerializeField] private float patrolInterval; //エネミーが次に動くまでのインターバル時間
    private float patrolTimer;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent が Enemy に付いていません", this);
        }
    }

    void Update()
    {
        if(!agent.isOnNavMesh)
        {
            return;
        }

        switch(currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.EnCounter:
                EnCounter();
                break;
        }
    }

    //探索状態
    void Patrol()
    {
        isRun = agent.velocity.magnitude > 0.1f;
        Enemyanim.SetBool("run", isRun);
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolInterval)
        {
            patrolTimer = 0f;

            Vector3 randomPos = GetRandomNavMeshPosition();
            agent.SetDestination(randomPos);
        }
    }

    //追跡状態
    void EnCounter()
    {
        isRun = true;
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    //Player検知
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            target = null;
            currentState = State.Patrol;
        }
    }

    Vector3 GetRandomNavMeshPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas);

        return hit.position;
    }
}
