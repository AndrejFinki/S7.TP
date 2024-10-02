using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MistakesUI : MonoBehaviour
{
	
	public PlayerState playerState;

	private Text text;
	private int mistakes;

	void Start ()
	{
		
		text = GetComponent <Text> ();
		mistakes = int.MinValue;

	}

	void Update ()
	{

		if (mistakes != playerState.mistakes) {
			text.text = playerState.mistakes.ToString ();
			mistakes = playerState.mistakes;
		}
		
	}

}
