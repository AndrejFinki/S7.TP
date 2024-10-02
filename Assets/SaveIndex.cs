using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class SaveIndex : MonoBehaviour
{
		
	public Text text;

	public void Enter ()
	{
		if (text.text != "") {
			Game.LOGIN (text.text);
			Scenes.LoadScene (Scenes.Scene.START);
		}
	}
}