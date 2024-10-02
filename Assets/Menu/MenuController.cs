using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{

	private Animator animator;

	void Start ()
	{
		animator = GetComponent<Animator> ();
	}

	public void SetDifficultyMenu (bool t)
	{
		animator.SetBool ("Difficulty", t);
	}
}
