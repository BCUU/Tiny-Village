using UnityEngine;
using UnityEngine.AI;

public class Villager_Manager : MonoBehaviour
{
    public Transform target; // Hedef Transform
    private NavMeshAgent agent; // NavMesh Agent
    private Animator animator; // Animator bile�eni

    void Start()
    {
        // NavMeshAgent ve Animator bile�enlerini al
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent bile�eni eksik!");
        }

        if (animator == null)
        {
            Debug.LogError("Animator bile�eni eksik!");
        }

        if (target == null)
        {
            Debug.LogWarning("Hedef atanmad�! L�tfen bir hedef (Transform) atay�n.");
        }
        MoveToTarget();
    }

    void Update()
    {
        if (target != null)
        {
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                // Hedefe do�ru hareket ederken animasyonu Walking yap
                animator.SetBool("isWalking", true);
            }
            else
            {
                // Hedefe ula��nca Idle'a ge�
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
            Debug.LogWarning("Target atanmad�!");
        }
    }
}
