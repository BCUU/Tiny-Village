using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public static TreeManager Instance;
    public List<GameObject> trees = new List<GameObject>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        foreach (GameObject tree in GameObject.FindGameObjectsWithTag("Tree"))
        {
            trees.Add(tree);
        }
    }

    public GameObject GetNearestTree(Vector3 position)
    {
        GameObject nearestTree = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject tree in trees)
        {
            if (tree != null) // A�ac�n hala sahnede oldu�undan emin ol
            {
                float distance = Vector3.Distance(position, tree.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestTree = tree;
                }
            }
        }
        return nearestTree;
    }

    public void RemoveTree(GameObject tree)
    {
        if (trees.Contains(tree)) trees.Remove(tree);
    }
}
