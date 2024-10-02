using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class LoadScene : MonoBehaviour, IPointerClickHandler {

	public int scene;

	public void OnPointerClick (PointerEventData eventData)	{
		if (System.DateTime.Now < new System.DateTime (2015, 9, 15)) {
			Application.LoadLevel (scene);
		}
	}

	public void Load(int scene) {
		Application.LoadLevel (scene);
	}
	
}
