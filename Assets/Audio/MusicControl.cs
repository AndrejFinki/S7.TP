using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MusicControl : MonoBehaviour
{

    public Sprite onIcon;
    public Sprite offIcon;
    public Image icon;

    public void Click()
    {
        BackgroundMusic.BG_MUSIC_ON = !BackgroundMusic.BG_MUSIC_ON;
        if (BackgroundMusic.BG_MUSIC_ON)
        {
            icon.sprite = onIcon;
        }
        else
        {
            icon.sprite = offIcon;
        }
    }

    void Start()
    {
        if (BackgroundMusic.BG_MUSIC_ON)
        {
            icon.sprite = onIcon;
        }
        else
        {
            icon.sprite = offIcon;
        }
    }

}
