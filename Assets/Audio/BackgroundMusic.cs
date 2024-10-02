using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour
{

    public static bool BG_MUSIC_ON = true;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (BG_MUSIC_ON && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        if (!BG_MUSIC_ON && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
