using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayOnClick : MonoBehaviour, IPointerClickHandler
{

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (BackgroundMusic.BG_MUSIC_ON)
        {
            audioSource.Play();
        }
    }
}
