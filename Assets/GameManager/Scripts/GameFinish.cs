using UnityEngine;
using System.Collections;

public class GameFinish : MonoBehaviour
{

    public GameOver game;
    public AudioClip gameOver;
    public AudioClip finish;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Messenger.AddListener<PlayerState>(MessengerIDs.PLAYER_GAME_FINISHED, PlayerFinished);
    }

    private void PlayerFinished(PlayerState player)
    {
        PlayerQueue playerQueue = PlayerQueue.getInstance();
        PlayerMovement[] playerMovement = new PlayerMovement[playerQueue.players.Length];
        for (int i = 0; i < playerQueue.players.Length; i++)
        {
            playerMovement[i] = playerQueue.players[i].GetComponent<PlayerMovement>();
        }
        for (int i = 0; i < playerMovement.Length - 1; i++)
        {
            for (int j = i + 1; j < playerMovement.Length; j++)
            {
                if (playerMovement[j].CurrentTile > playerMovement[i].CurrentTile)
                {
                    PlayerMovement t = playerMovement[i];
                    playerMovement[i] = playerMovement[j];
                    playerMovement[j] = t;
                }
            }
        }
        for (int i = 0; i < playerMovement.Length; i++)
        {
            PlayerState playerState = playerMovement[i].GetComponent<PlayerState>();
            if (playerState.playerControll is UIPlayerControll)
            {
                Score.place = i + 1;
                if (Score.place == 1)
                {
                    audioSource.clip = finish;
                }
                else
                {
                    audioSource.clip = gameOver;
                }
                Score.correct = playerState.correct;
                Score.wrong = playerState.mistakes;
                Score.maxTiles = 60;
                Score.tiles = playerMovement[i].CurrentTile - 1;
                if (BackgroundMusic.BG_MUSIC_ON)
                {
                    audioSource.Play();
                }
            }
        }
        game.EndGame();
    }

    void OnDestroy()
    {
        Messenger.RemoveListener<PlayerState>(MessengerIDs.PLAYER_GAME_FINISHED, PlayerFinished);
    }

}
