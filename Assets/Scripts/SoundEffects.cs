using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonClick;
    public static SoundEffects Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }
}
