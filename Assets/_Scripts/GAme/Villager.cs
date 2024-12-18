using UnityEngine;
using UnityEngine.AI;

public enum TaskType { None, ChopWood, Mine }

public class Villager : MonoBehaviour
{
    public TaskType currentTask = TaskType.None; // Mevcut g�rev t�r�
    private NavMeshAgent agent;
    private Animator animator;

    [SerializeField] private GameObject axeTool;    // Balta arac�
    [SerializeField] private GameObject pickaxeTool; // Kazma arac�

    [Header("Audio Settings")]
    [SerializeField] private AudioClip chopWoodSound; // A�a� kesme sesi
    [SerializeField] private AudioClip mineSound;     // Maden toplama sesi
    private AudioSource audioSource;

    private float taskDuration = 5f; // G�rev s�resi
    private float taskTimer;         // Zamanlay�c�
    private bool isPerformingTask = false; // G�rev aktif mi?
    private Transform currentTarget; // Hedef

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource ekle
        audioSource.loop = true; // Ses d�ng�de �alacak
        DeactivateAllTools();
    }

    public bool IsAvailable()
    {
        return currentTask == TaskType.None; // Bo�ta m�?
    }

    public void AssignTask(TaskType taskType, Transform target)
    {
        currentTask = taskType;
        currentTarget = target;
        isPerformingTask = false;
        agent.SetDestination(target.position);
        animator.SetBool("isWalking", true);
        Debug.Log($"{gameObject.name} {taskType} g�revine do�ru yola ��kt�.");
    }

    void Update()
    {
        if (currentTask != TaskType.None)
        {
            if (!isPerformingTask && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                StartTaskAnimation();
            }

            if (isPerformingTask)
            {
                taskTimer -= Time.deltaTime;
                if (taskTimer <= 0)
                {
                    CompleteTask();
                }
            }
        }
    }

    void StartTaskAnimation()
    {
        isPerformingTask = true;
        animator.SetBool("isWalking", false);

        // G�reve g�re animasyon ve ses ayarlar�
        switch (currentTask)
        {
            case TaskType.ChopWood:
                animator.SetInteger("TaskType", 1);
                if (axeTool != null) axeTool.SetActive(true);
                PlaySound(chopWoodSound);
                break;

            case TaskType.Mine:
                animator.SetInteger("TaskType", 2);
                if (pickaxeTool != null) pickaxeTool.SetActive(true);
                PlaySound(mineSound);
                break;
        }

        taskTimer = taskDuration;
    }

    void CompleteTask()
    {
        // Mevcut hedefi tamamla
        if (currentTask == TaskType.ChopWood && currentTarget != null)
        {
            TreeChopping tree = currentTarget.GetComponent<TreeChopping>();
            if (tree != null) tree.Chop();
        }
        else if (currentTask == TaskType.Mine && currentTarget != null)
        {
            MineResource mine = currentTarget.GetComponent<MineResource>();
            if (mine != null) mine.Mine();
        }

        // Yeni hedef kontrol�
        GameObject nextTarget = null;
        if (currentTask == TaskType.ChopWood)
            nextTarget = TreeManager.Instance.GetNearestTree(transform.position);
        else if (currentTask == TaskType.Mine)
            nextTarget = MineManager.Instance.GetNearestMine(transform.position);

        if (nextTarget != null)
        {
            AssignTask(currentTask, nextTarget.transform);
        }
        else
        {
            currentTask = TaskType.None;
            animator.SetInteger("TaskType", 0);
            DeactivateAllTools();
            StopSound();
            Debug.Log($"{gameObject.name} Idle'a ge�ti.");
        }

        isPerformingTask = false;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    void StopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    void DeactivateAllTools()
    {
        if (axeTool != null) axeTool.SetActive(false);
        if (pickaxeTool != null) pickaxeTool.SetActive(false);
    }
}
