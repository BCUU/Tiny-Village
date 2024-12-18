using UnityEngine;
using UnityEngine.AI;

public class Villager_Manager : MonoBehaviour
{
    public Transform target; // Hedef Transform
    private NavMeshAgent agent; // NavMesh Agent
    private Animator animator; // Animator bileþeni

    void Start()
    {
        // NavMeshAgent ve Animator bileþenlerini al
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent bileþeni eksik!");
        }

        if (animator == null)
        {
            Debug.LogError("Animator bileþeni eksik!");
        }

        if (target == null)
        {
            Debug.LogWarning("Hedef atanmadý! Lütfen bir hedef (Transform) atayýn.");
        }
        MoveToTarget();
    }

    void Update()
    {
        if (target != null)
        {
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                // Hedefe doðru hareket ederken animasyonu Walking yap
                animator.SetBool("isWalking", true);
            }
            else
            {
                // Hedefe ulaþýnca Idle'a geç
                animator.SetBool("isWalking", false);
            }
        }
    }

    public void MoveToTarget()
    {
        if (target != null)
        {
            Debug.Log("Hedefe gidiliyor: " + target.position);
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
        else
        {
            Debug.LogWarning("Target atanmadý!");
        }
    }
}
