using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public delegate void TeacherDialogCallback(string answer);
public class TeacherDialog : MonoBehaviour
{

    public Text input;

    private Animator animator;
    private TeacherDialogCallback callback;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Show", false);
    }

    public void ShowDialog(TeacherDialogCallback callback)
    {
        this.callback = callback;
        animator.SetBool("Show", true);
    }

    public void Answer()
    {
        animator.SetBool("Show", false);
        if (callback != null)
        {
            callback(input.text);
        }
    }

    public void Close()
    {
        animator.SetBool("Show", false);
    }

}
