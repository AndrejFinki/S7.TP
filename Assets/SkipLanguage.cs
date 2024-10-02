using UnityEngine;
using System.Collections;

public class SkipLanguage : MonoBehaviour
{

    private static bool languageSelected;

    public Animator animator;

    void Awake()
    {
        if (languageSelected)
        {
            animator.SetTrigger("Start");
        }
    }

    public void LanguageSelect()
    {
        languageSelected = true;
    }
}
