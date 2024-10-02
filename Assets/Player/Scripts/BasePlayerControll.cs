using UnityEngine;
using System.Collections;

public delegate void CardSelected (int card);
public delegate bool TileSelected (Tile tile);
public delegate void NumberSelected (int tile);

public abstract class BasePlayerControll : MonoBehaviour {

	protected TileSelected tileSelectedCallback;
	protected CardSelected cardSelectedCallback;
	protected NumberSelected numberSelectedCallback;
	
	public abstract void SetSelectableCards (Card[] cards);
	public abstract void SelectCard (CardSelected callback);
	public abstract void SetSelectableNumbers (int[] numbers);
	public abstract void SelectNumber (NumberSelected callback);
	public abstract void SelectTile (TileSelected callback);
    public abstract void SelectedCardWillPlay ();
    public abstract void SelectedCardPlayed ();

}
