using UnityEngine;
using System.Collections.Generic;

public class MoveActionAll : MoveAction
{

	private ActionCompleted callback;
	private int movingPlayers;
    private BasePlayerControll playerControll;

	public override void PlayAction (ActionCompleted callback, int move, BasePlayerControll playerControll)
	{
        this.playerControll = playerControll;
		this.callback = callback;
        playerControll.SelectedCardWillPlay();
		PlayerState[] players = PlayerQueue.getInstance ().players;
		movingPlayers = PlayerQueue.getInstance ().StillPlaying ();
		for (int i = 0; i < players.Length; i++) {
			PlayerMovement movement = players [i].GetComponent<PlayerMovement> ();
			if (players [i].CurrentState != PlayerState.States.Finished) {
				movement.Move (move, MoveComplete);
			}
		}

	}

	public override int ActionAiScore (int moves)
	{

		PlayerMovement currentPlayer = PlayerQueue.getInstance ().GetCurrentPlayer ().GetComponent<PlayerMovement> ();
		List<PlayerMovement> playersAhead = new List<PlayerMovement> ();

		PlayerState[] players = PlayerQueue.getInstance ().players;
		for (int i = 0; i < players.Length; i++) {
			PlayerMovement movement = players [i].GetComponent<PlayerMovement> ();
			if (movement != currentPlayer && movement.CurrentTile > currentPlayer.CurrentTile
			    && players [i].CurrentState != PlayerState.States.Finished) {
				playersAhead.Add (movement);
			}
		}

		int score = 0;
		GameMap map = GameMap.getInstance ();
		for (int i = 0; i < playersAhead.Count; i++) {
			PlayerMovement player = playersAhead [i];
			int tile = player.CurrentTile + moves;
			tile = Mathf.Max (0, tile);
			tile = Mathf.Min (map.Tiles.Count - 1, tile);
			Tile currentTile = map.Tiles [tile];
			TileAction currentTileAction = currentTile.GetComponent<TileAction> ();

			if (currentTileAction != null) {
				tile = tile + currentTileAction.playerMoves;
			}
			score -= tile - player.CurrentTile;
		}

		int nextTile = currentPlayer.CurrentTile + moves;
		nextTile = Mathf.Max (0, nextTile);
		nextTile = Mathf.Min (map.Tiles.Count - 1, nextTile);
		Tile tmpTile = map.Tiles [nextTile];
		TileAction tmpTileAction = tmpTile.GetComponent<TileAction> ();

		if (tmpTileAction != null) {
			nextTile = nextTile + tmpTileAction.playerMoves;
		}

		score += (nextTile - currentPlayer.CurrentTile) * playersAhead.Count;

		return score;

	}

	public override Tile ActionAiTile (int moves)
	{

		return null;

	}

	private void MoveComplete ()
	{

		movingPlayers--;
		if (movingPlayers == 0 && callback != null) {
            playerControll.SelectedCardPlayed();
			callback (true);
		}

	}

}
