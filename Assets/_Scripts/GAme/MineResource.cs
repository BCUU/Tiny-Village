using UnityEngine;

public class MineResource : MonoBehaviour
{
    public void Mine()
    {
        Debug.Log("Maden toplandý!");
        Destroy(gameObject); // Madeni sahneden kaldýr
    }
}
