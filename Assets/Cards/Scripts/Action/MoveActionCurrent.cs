using UnityEngine;
using System.Collections;

public class MoveActionCurrent : MoveAction
{

	private ActionCompleted callback;
	private int moves;
	private BasePlayerControll playerControll;
	private PlayerMovement player;
	private Tile aiTile;
	private int aiScore;

	public override void PlayAction (ActionCompleted callback, int move, BasePlayerControll playerControll)
	{

		this.callback = callback;
		this.moves = move;
		this.playerControll = playerControll;
		SetPlayer ();
		playerControll.SelectTile (TileSelected);

	}

	public override int ActionAiScore (int moves)
	{

		CalculateAi (moves);
		return aiScore;

	}

	public override Tile ActionAiTile (int moves)
	{

		CalculateAi (moves);
		return aiTile;

	}

	private void SetPlayer ()
	{

		this.player = PlayerQueue.getInstance ().GetCurrentPlayer ().GetComponent <PlayerMovement> ();

	}

	private void CalculateAi (int moves)
	{

		SetPlayer ();
		GameMap map = GameMap.getInstance ();
		int tile = player.CurrentTile + moves;
		int finalTile = tile;
		if (tile < map.Tiles.Count) {
			Tile currentTile = map.Tiles [tile];
			TileAction currentTileAction = currentTile.GetComponent<TileAction> ();

			if (currentTileAction != null) {
				finalTile = tile + currentTileAction.playerMoves;
			}
			aiTile = map.Tiles [tile];
			aiScore = finalTile - player.CurrentTile;
		} else {
			finalTile = map.Tiles.Count - 1;
			aiTile = map.Tiles [finalTile];
			aiScore = int.MaxValue;
		}

	}

	private bool TileSelected (Tile tile)
	{

		GameMap gameMap = GameMap.getInstance ();

		int tileOrder = gameMap.Tiles.IndexOf (tile);
		int distance = tileOrder - player.CurrentTile;

		if (distance == moves || distance <= moves && tileOrder == gameMap.Tiles.Count - 1) {
			player.GetComponent<PlayerState> ().correct += 1;
			player.Move (distance, MoveComplete);
            playerControll.SelectedCardWillPlay();
            return true;
		} else {
			player.GetComponent<PlayerState> ().mistakes += 1;
            callback (false);
            return false;
//			playerControll.SelectTile (TileSelected);
		}

	}

	private void MoveComplete ()
	{

		if (callback != null) {
            playerControll.SelectedCardPlayed();
			callback (true);
		}

	}

}
