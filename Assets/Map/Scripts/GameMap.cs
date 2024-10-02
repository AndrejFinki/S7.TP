using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[ExecuteInEditMode]
public class GameMap : MonoBehaviour
{

	private static GameMap instance;

	public static GameMap getInstance ()
	{
		return instance;
	}

	public float spacing;
	private List<Tile> _Tiles;

	public List<Tile> Tiles {
		get {
			return _Tiles;
		}
	}

	void Awake ()
	{
		instance = this;
		_Tiles = new List<Tile> (GetComponentsInChildren<Tile> ());
		Randomize ();
	}

	private void SwitchTiles (int a, int b)
	{
		Tile temp = _Tiles [a];
		_Tiles [a] = _Tiles [b];
		_Tiles [b] = temp;
	}

	private bool CheckRandom ()
	{
		for (int i = 0; i < _Tiles.Count; i++) {
			Tile current = _Tiles [i];
			if (i > 1) {
				if (current.tileNumber == _Tiles [i - 1].tileNumber &&
				    current.tileNumber == _Tiles [i - 2].tileNumber) {
					return false;
				}
			}
			if (current.tileNumber == 4) {
				if (i == 0 || i == Tiles.Count - 1) {
					return false;
				}
				if (i - 4 >= 0 && (_Tiles [i - 4].tileNumber == 5 || _Tiles [i - 4].tileNumber == 4)) {
					return false;
				}
				if (i - 5 >= 0 && _Tiles [i - 5].tileNumber == 5) {
					return false;
				}
			}
			if (current.tileNumber == 5) {
				if (i == 0 || i == _Tiles.Count - 1) {
					return false;
				}
			}
		}
		return true;
	}

	private void FisherYates ()
	{
		for (int i = 1; i < _Tiles.Count - 1; i++) {
			int j = Random.Range (i, _Tiles.Count - 1);
			SwitchTiles (i, j);
		}
	}

	private void Randomize ()
	{
		FisherYates ();
		while (!CheckRandom ()) {
			FisherYates ();
		}
		for (int i = 0; i < _Tiles.Count; i++) {
			_Tiles [i].transform.SetSiblingIndex (i);
		}
		SetRects ();
	}

	private void SetRects ()
	{
		float currentX = 0;
		float maxHeight = 0;
		for (int i = 0; i < _Tiles.Count; i++) {
			RectTransform tileRect = _Tiles [i].GetComponent<RectTransform> ();
			tileRect.anchoredPosition = new Vector2 (currentX, 0);
			currentX += tileRect.rect.width + spacing;
			if (tileRect.rect.height > maxHeight) {
				maxHeight = tileRect.rect.height;
			}
		}
		RectTransform rectTransform = GetComponent<RectTransform> ();
		rectTransform.sizeDelta = new Vector2 (currentX, maxHeight);
	}

	void OnValidate ()
	{
		if (_Tiles != null) {
			SetRects ();
		}
	}

}
