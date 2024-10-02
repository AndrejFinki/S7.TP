using UnityEngine;
using System.Collections;

public class PlayerQueue : MonoBehaviour
{

    public PlayerState[] players;
    private int current;
    private static PlayerQueue instance;

    public static PlayerQueue getInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        CurrentPlay();
    }

    public int StillPlaying()
    {
        int playing = 0;
        for (int i = 0; i < players.Length; i++)
        {
            if (players [i].CurrentState != PlayerState.States.Finished)
            {
                playing ++;
            }
        }
        return playing;
    }
    
    private void PlayerEnded()
    {
        current++;
        if (current >= players.Length)
        {
            current = 0;
        }
        if (StillPlaying() > 0)
        {
            CurrentPlay();
        }
    }

    private void CurrentPlay()
    {
        if (current < players.Length)
        {
            players [current].StartTurn(PlayerEnded);
        }
    }

    public PlayerState GetCurrentPlayer()
    {
        if (current < players.Length) 
            return players [current];
        else
            return null;
    }

}
