using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CorrectUI : MonoBehaviour
{
	
	public PlayerState playerState;

	private Text text;
	private int correct;

	void Start ()
	{
		
		text = GetComponent <Text> ();
		correct = int.MinValue;

	}

	void Update ()
	{

		if (correct != playerState.correct) {
			text.text = playerState.correct.ToString ();
			correct = playerState.correct;
		}
		
	}

}
