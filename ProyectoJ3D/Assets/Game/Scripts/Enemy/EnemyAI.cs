using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float sightRange = 25f;
    public float attackRange = 2.2f;
    public float attackCooldown = 1.2f;

    private NavMeshAgent agent;
    private Animator animator;
    private float nextAttackTime = 0f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!player) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= attackRange)
        {
            AttackBehaviour();
        }
        else if (dist <= sightRange)
        {
            ChaseBehaviour();
        }
        else
        {
            IdleBehaviour();
        }

        // opcional: parámetro de velocidad para caminata
        if (animator)
            animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    void ChaseBehaviour()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
        animator.SetBool("IsAttacking", false);
    }

    void AttackBehaviour()
    {
        agent.isStopped = true;

        // mirar al jugador
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude > 0.001f)
        {
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 10f);
        }

        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;

            // cambia a la animación de ataque
            animator.SetBool("IsAttacking", true);
            animator.SetTrigger("Attack"); // Trigger en el Animator
        }
    }

    void IdleBehaviour()
    {
        agent.isStopped = true;
        animator.SetBool("IsAttacking", false);
    }
}
