using UnityEngine;

public class MineResource : MonoBehaviour
{
    public void Mine()
    {
        Debug.Log("Maden topland�!");
        Destroy(gameObject); // Madeni sahneden kald�r
    }
}
