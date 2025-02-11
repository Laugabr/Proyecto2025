using UnityEngine;
using UnityEngine.AI;

public class AIChase : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
     
    [SerializeField] Transform player;

    [SerializeField] LayerMask whatIsGround, whatIsPlayer;

    //Modo Patrullando

    [SerializeField] Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] float walkPointRange;

    //Modo ataque 
    [SerializeField] float attackCooldown;
    bool alreadyAttacked;
    float health = 5f;  //vida

    //Estados
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    

    void Awake()
    {
        player = GameObject.Find("PlayerTest").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }
        if(!playerInAttackRange && playerInSightRange)
        {
            ChasePlayer();
        }

        if(playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }

    }
    private void Patrolling()
    {
        if (!walkPointSet) 
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

    Vector3 distanceToWalkPoint = transform.position - walkPoint; //nos fijamos cuanta es la distancia hasta el walk point

    if (distanceToWalkPoint.magnitude < 1f)
    {
        // ya llegamos al destino, que la ia busque otro
        walkPointSet = false; 
    }

    }

    private void SearchWalkPoint()
    {
        //calculamos un punto random dentro del rango de caminata
        float randomY = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        //elige dentro de esos puntos
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) //nos fijamos que si lo pueda caminar
        {
            walkPointSet = true;
            //si lo puede caminar, nuestro walk point esta seteado
        }

    }

    private void ChasePlayer()
    {
        //si olo persigue, que su destino sea el jugador jaja
        agent.SetDestination(player.position); 
    }

    private void AttackPlayer()
    {
        //asegurarnos que el enemigo se deje de mover
        agent.SetDestination(transform.position);

        if(!alreadyAttacked)
        {

            ///aca iria el ataque del enemigo
            ///
            ///

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke (nameof(DestroyEnemy), .5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


}
