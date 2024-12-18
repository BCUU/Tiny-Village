using System.Collections.Generic;
using UnityEngine;

public class VillagerManager : MonoBehaviour
{
    public static VillagerManager Instance;
    public List<Villager> villagers = new List<Villager>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) AssignTaskToVillager(TaskType.ChopWood);
        if (Input.GetKeyDown(KeyCode.M)) AssignTaskToVillager(TaskType.Mine);
    }

    public void AssignTaskToVillager()
    {
        foreach (Villager villager in villagers)
        {
            if (villager.IsAvailable())
            {
                GameObject nextTarget = TreeManager.Instance.GetNearestTree(villager.transform.position);
                if (nextTarget == null) nextTarget = MineManager.Instance.GetNearestMine(villager.transform.position);

                if (nextTarget != null)
                {
                    villager.AssignTask(nextTarget.CompareTag("Tree") ? TaskType.ChopWood : TaskType.Mine, nextTarget.transform);
                    return;
                }
            }
        }

        Debug.Log("Yeni hedef bulunamadý.");
    }

    public void AssignTaskToVillager(TaskType taskType)
    {
        foreach (Villager villager in villagers)
        {
            if (villager.IsAvailable())
            {
                GameObject target = taskType == TaskType.ChopWood
                    ? TreeManager.Instance.GetNearestTree(villager.transform.position)
                    : MineManager.Instance.GetNearestMine(villager.transform.position);

                if (target != null)
                {
                    villager.AssignTask(taskType, target.transform);
                    return;
                }
            }
        }

        Debug.Log($"Görev atanacak {taskType} hedefi bulunamadý!");
    }
}
