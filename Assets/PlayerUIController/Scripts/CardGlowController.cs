using UnityEngine;
using System.Collections;

public class CardGlowController : MonoBehaviour
{

    public bool glowOn;
    private Animator animator;
    private int onParam;
    private bool on;
    
    void Awake()
    {
        on = glowOn;
        onParam = Animator.StringToHash("On");
        animator = GetComponent<Animator>();
        animator.SetBool(onParam, on);
    }

    void Update()
    {
        if (on != glowOn)
        {
            on = glowOn;
            animator.SetBool(onParam, glowOn);
        }
    }
    
}
