using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour
{

	public void Quit ()
	{
		Back ();
	}

	private void Back ()
	{
		if (Application.loadedLevel == (int)Scenes.Scene.START) {
			Application.Quit ();
		}
		if (Application.loadedLevel == (int)Scenes.Scene.GAME) {
			Scenes.LoadScene(Scenes.Scene.START);
		}
		if (Application.loadedLevel == (int)Scenes.Scene.SCORE) {
			Scenes.LoadScene(Scenes.Scene.START);
		}
	}

}
