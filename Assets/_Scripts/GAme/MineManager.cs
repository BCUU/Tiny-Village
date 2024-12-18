using System.Collections.Generic;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    public static MineManager Instance;
    public List<GameObject> mines = new List<GameObject>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        foreach (GameObject mine in GameObject.FindGameObjectsWithTag("Mine"))
        {
            mines.Add(mine);
        }
    }

    public GameObject GetNearestMine(Vector3 position)
    {
        GameObject nearestMine = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject mine in mines)
        {
            if (mine != null)
            {
                float distance = Vector3.Distance(position, mine.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestMine = mine;
                }
            }
        }
        return nearestMine;
    }

    public void RemoveMine(GameObject mine)
    {
        if (mines.Contains(mine)) mines.Remove(mine);
    }
}
