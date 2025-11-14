using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource;

    void Awake()
    {
        // Si ya hay uno, destruye este
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject); // La música no se corta al cambiar de escena
        audioSource = GetComponent<AudioSource>();
    }

    public void SetVolume(float vol)
    {
        audioSource.volume = vol;
    }

    public void ChangeMusic(AudioClip newMusic, float volume = 0.5f)
    {
        audioSource.clip = newMusic;
        audioSource.volume = volume;
        audioSource.Play();
    }
}
