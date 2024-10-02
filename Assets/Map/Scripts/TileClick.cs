using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent (typeof(Tile))]
public class TileClick : MonoBehaviour
{

	private Tile tile;

	void Awake ()
	{

		tile = GetComponent<Tile> ();

	}

	public void OnClick ()
	{
		Messenger.Broadcast<Tile> (MessengerIDs.TILE_CLICKED, tile);
	}

}
