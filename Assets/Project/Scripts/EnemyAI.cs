using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Управление ИИ противника с автоматической передачей скорости 
/// и триггера атаки в Animator Controller.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("Параметры Преследования")]
    [SerializeField] private Transform playerTarget;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float attackDamage = 15.0f;

    private NavMeshAgent agent;
    private Animator animator;
    private float lastAttackTime;

    private static readonly int SpeedParam = Animator.StringToHash("Speed");
    private static readonly int AttackParam = Animator.StringToHash("Attack");

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        // Автоматический поиск компонента Animator у дочерней 3D-модели
        animator = GetComponentInChildren<Animator>();

        if (playerTarget == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) playerTarget = playerObj.transform;
        }
    }

    private void Update()
    {
        if (playerTarget == null || agent == null) return;
    
        // ПРОВЕРКА: Если агент еще не поставлен на запеченную NavMesh-сетку, пропускаем кадр
        if (!agent.isActiveAndEnabled || !agent.isOnNavMesh) return;
    
        float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);
    
        if (distanceToPlayer <= attackRange)
        {
            Vector3 direction = (playerTarget.position - transform.position).normalized;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10f);
            }
    
            TryAttackPlayer();
        }
        else
        {
            agent.SetDestination(playerTarget.position);
        }
    
        if (animator != null)
        {
            animator.SetFloat(SpeedParam, agent.velocity.magnitude);
        }
    }

    private void TryAttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PlayerStats stats = playerTarget.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.Heal(-attackDamage);

                // Запуск триггера анимации атаки
                if (animator != null)
                {
                    animator.SetTrigger(AttackParam);
                }

                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlayHitSound();
                }

                Debug.Log($"<color=red>[EnemyAI]</color> Противник нанес урон: {attackDamage}");
            }
            lastAttackTime = Time.time;
        }
    }
}