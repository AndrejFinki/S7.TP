using UnityEngine;
using System.Collections;

public class QuitButton : MonoBehaviour
{

	public QuitGame quitGame;
	public YesNoDialog yesNoDialog;
	public TIMMultilanguage.MultilanguageString quitMessage;

	public void ButtonClicked ()
	{
		yesNoDialog.ShowDialog (quitMessage.GetString (), DialogResult);
	}

	private void DialogResult (bool answer)
	{
		if (answer) {
			PlayerQueue playerQueue = PlayerQueue.getInstance ();
			PlayerMovement[] playerMovement = new PlayerMovement[playerQueue.players.Length];
			for (int i = 0; i < playerQueue.players.Length; i++) {
				playerMovement [i] = playerQueue.players [i].GetComponent<PlayerMovement> ();
			}
			for (int i = 0; i < playerMovement.Length - 1; i++) {
				for (int j = i + 1; j < playerMovement.Length; j++) {
					if (playerMovement [j].CurrentTile > playerMovement [i].CurrentTile) {
						PlayerMovement t = playerMovement [i];
						playerMovement [i] = playerMovement [j];
						playerMovement [j] = t;
					}
				}
			}
			for (int i = 0; i < playerMovement.Length; i++) {
				PlayerState playerState = playerMovement [i].GetComponent<PlayerState> ();
				if (playerState.playerControll is UIPlayerControll) {
					Score.place = i + 1;
					Score.correct = playerState.correct;
					Score.wrong = playerState.mistakes;
					Score.maxTiles = 60;
					Score.tiles = playerMovement [i].CurrentTile - 1;
						
					Game.END_GAME (Score.place, Score.correct, Score.wrong, Score.tiles, Score.maxTiles, true);
						
				}
			}
			quitGame.Quit ();
		}
	}

}
