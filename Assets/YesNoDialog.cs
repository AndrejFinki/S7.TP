using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public delegate void YesNoDialogCallback(bool answer);
public class YesNoDialog : MonoBehaviour
{

    public Text messageField;

    private Animator animator;
    private YesNoDialogCallback callback;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Show", false);
    }

    public void ShowDialog(string message, YesNoDialogCallback callback)
    {
        if (messageField != null)
        {
            messageField.text = message;
        }
        this.callback = callback;
        animator.SetBool("Show", true);
    }

    public void HideDialog()
    {
        animator.SetBool("Show", false);
    }

    public void Answer(bool answer)
    {
        animator.SetBool("Show", false);
        if (callback != null)
        {
            callback(answer);
        }
    }

}
