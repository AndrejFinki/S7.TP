using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public Animator animator;

	public void AnimationEnded() {
		Scenes.LoadScene(Scenes.Scene.SCORE);
	}

	public void EndGame() {
		animator.SetTrigger("GameOver");
	}

}
