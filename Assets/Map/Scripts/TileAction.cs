using UnityEngine;
using System.Collections;

public class TileAction : MonoBehaviour
{

	public int playerMoves;
	public Animator animator;

	public int TileEnter ()
	{
		if (animator != null) {
			animator.SetTrigger ("Action");
		}
		return playerMoves;
	}

}
