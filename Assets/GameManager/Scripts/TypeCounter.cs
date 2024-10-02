using UnityEngine;
using System.Collections.Generic;

public class TypeCounter : MonoBehaviour
{

    private static TypeCounter instance;
    public static TypeCounter getInstance()
    {
        return instance;
    }

	private GameMap gameMap;
    private Dictionary<int, int> countable;
	
    void Awake()
    {
        instance = this;
        countable = new Dictionary<int, int>();
        Messenger.AddListener<int>(MessengerIDs.PLAYER_MOVED_FROM, PlayerMovedFrom);
        Messenger.AddListener<int>(MessengerIDs.PLAYER_MOVED_TO, PlayerMovedTo);
    }

	void Start()
	{
		gameMap = GameMap.getInstance ();
	}

	void OnDestroy()
	{
		instance = null;
		Messenger.RemoveListener<int>(MessengerIDs.PLAYER_MOVED_FROM, PlayerMovedFrom);
		Messenger.RemoveListener<int>(MessengerIDs.PLAYER_MOVED_TO, PlayerMovedTo);
	}

    private void AddCountable(int type, int count)
    {
        if (!countable.ContainsKey(type))
            countable[type] = 0;
        countable[type] = Mathf.Max(0, countable[type] + count);
    }

    private void PlayerMovedFrom(int tile)
    {
        AddCountable(gameMap.Tiles[tile].tileNumber, -1);
    }

    private void PlayerMovedTo(int tile)
    {
		AddCountable(gameMap.Tiles[tile].tileNumber, 1);
    }

	public int GetCountable(int type)
	{
		if (!countable.ContainsKey(type))
			countable[type] = 0;
		return countable[type];
	}

}
