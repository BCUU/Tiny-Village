using UnityEngine;

public class TreeChopping : MonoBehaviour
{
    [SerializeField] private GameObject choppedTree; // Kesilmi� a�a� prefab'�
    [SerializeField] private float choppedTreeLifetime = 5f; // Kesilmi� a�ac�n sahnede kalma s�resi
    [SerializeField] private AudioClip fallSound; // D��me sesi
    [SerializeField] private float fallSoundVolume = 0.7f; // Sesin sesi seviyesi

    public void Chop()
    {
        // Kesilmi� a�a� prefab'�n� olu�tur
        GameObject choppedInstance = Instantiate(choppedTree, transform.position, transform.rotation);

        // Belirli bir s�re sonra kesilmi� a�ac� yok et
        Destroy(choppedInstance, choppedTreeLifetime);

        // D��me sesini oynat
        PlayFallSound();

        // A�ac� TreeManager'dan kald�r
        TreeManager.Instance.RemoveTree(gameObject);

        // Orijinal a�ac� sahneden sil
        Destroy(gameObject);
    }

    void PlayFallSound()
    {
        // E�er d��me sesi ayarlanm��sa sahnede oynat
        if (fallSound != null)
        {
            AudioSource.PlayClipAtPoint(fallSound, transform.position, fallSoundVolume);
        }
    }
}
