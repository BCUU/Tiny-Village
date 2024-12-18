using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public int totalWood = 0; // Toplam odun miktar�
    public int totalStone = 0; // Toplam maden miktar�

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Odun ekleme fonksiyonu
    public void AddWood(int amount)
    {
        totalWood += amount;
        Debug.Log($"Toplam Odun: {totalWood}");
    }

    // Maden ekleme fonksiyonu
    public void AddStone(int amount)
    {
        totalStone += amount;
        Debug.Log($"Toplam Maden: {totalStone}");
    }
}
