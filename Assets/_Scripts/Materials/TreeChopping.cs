using UnityEngine;

public class TreeChopping : MonoBehaviour
{
    [SerializeField] private GameObject choppedTree; // Kesilmiþ aðaç prefab'ý
    [SerializeField] private float choppedTreeLifetime = 5f; // Kesilmiþ aðacýn sahnede kalma süresi
    [SerializeField] private AudioClip fallSound; // Düþme sesi
    [SerializeField] private float fallSoundVolume = 0.7f; // Sesin sesi seviyesi

    public void Chop()
    {
        // Kesilmiþ aðaç prefab'ýný oluþtur
        GameObject choppedInstance = Instantiate(choppedTree, transform.position, transform.rotation);

        // Belirli bir süre sonra kesilmiþ aðacý yok et
        Destroy(choppedInstance, choppedTreeLifetime);

        // Düþme sesini oynat
        PlayFallSound();

        // Aðacý TreeManager'dan kaldýr
        TreeManager.Instance.RemoveTree(gameObject);

        // Orijinal aðacý sahneden sil
        Destroy(gameObject);
    }

    void PlayFallSound()
    {
        // Eðer düþme sesi ayarlanmýþsa sahnede oynat
        if (fallSound != null)
        {
            AudioSource.PlayClipAtPoint(fallSound, transform.position, fallSoundVolume);
        }
    }
}
