using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PiedestalPlace : MonoBehaviour
{

	public static Dictionary<PlayerState.ID, int> playerPosition = new Dictionary<PlayerState.ID, int> ();

	public Vector3[] positions;
	public PlayerState.ID playerID;


	void Start ()
	{
		RectTransform rectTransform = GetComponent<RectTransform> ();
		if (playerPosition.ContainsKey (playerID)) {
			rectTransform.anchoredPosition = positions [playerPosition [playerID]];
		}
	}

}
