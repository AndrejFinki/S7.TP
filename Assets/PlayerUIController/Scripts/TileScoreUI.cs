using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TileScoreUI : MonoBehaviour {

	public int tileType;

	private string formatedText;
	private Text text;
	private TypeCounter counter;
	
	void Start () {
		
		text = GetComponent <Text> ();
		counter = TypeCounter.getInstance ();
		formatedText = text.text;

	}
	
	void Update () {
		
		text.text = string.Format (formatedText, counter.GetCountable (tileType));
		
	}

}
