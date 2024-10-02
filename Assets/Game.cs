using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    [System.Serializable]
    public class GameData
    {
        public int level;
        public string loginId;
        public int place;
        public int correct;
        public int wrong;
        public int tiles;
        public int maxTiles;
        public bool quit;
        public List<int> map;
    }

    [System.Serializable]
    public class LoginData
    {
        public string index;
    }

    [System.Serializable]
    public class CardData
    {
        public string type;
        public string data;
    }

    [System.Serializable]
    public class PlayerData
    {
        public string type;
        public int tilePosition;
    }

    [System.Serializable]
    public class CardPlayData
    {
        public string gameId;
        public float timeToPlay;
        public int cardInGame;
        public List<CardData> cardsInHand;
        public int cardPlayed;
        public bool correct;
        public List<PlayerData> players;
    }

    public const string DB_NAME = "Stats";
    public const string COLLECTION_LOGIN = "Login";
    public const string COLLECTION_GAME = "Game";
    public const string COLLECTION_CARDS = "Cards";

    private static string GAME_ID = "";
    private static string LOGIN_ID = "";
    private static GameData GAME;
    private static LoginData LOGIN_DATA;

    void Awake()
    {	
//		App42API.Initialize ("17bfd128aae8c2cfde78e2373a34bbd54a2e6de3ec8b606b78705cb5387ce85f", "37313009cae973d6a59cca0fc7263babaf2911f7cf396cf70c3a3bb69dd4ff33"); 
    }



    public static void LOGIN(string index)
    {

//		LOGIN_ID = index;
//		LOGIN_DATA = new LoginData ();
//		LOGIN_DATA.index = index;

//		StorageService storageService = App42API.BuildStorageService ();   
//		storageService.InsertJSONDocument (DB_NAME, COLLECTION_LOGIN, JsonUtility.ToJson (LOGIN_DATA), new SaveIndexCallBack ());   

    }

    public static void START_GAME(int level)
    {

//		GAME = new GameData ();
//		GAME.level = level;
//		GAME.loginId = LOGIN_ID;
//
//		StorageService storageService = App42API.BuildStorageService ();   
//		storageService.InsertJSONDocument (DB_NAME, COLLECTION_GAME, JsonUtility.ToJson (GAME), new SaveGameCallBack ());   

    }

    public static void END_GAME(int place, int correct, int wrong, int tiles, int maxTiles, bool quit)
    {

//		GAME.place = place;
//		GAME.correct = correct;
//		GAME.wrong = wrong;
//		GAME.tiles = tiles;
//		GAME.maxTiles = maxTiles;
//		GAME.quit = quit;
//
//
//		List<int> map = new List<int> ();
//		GameMap gameMap = GameMap.getInstance ();
//		for (int i = 0; i < gameMap.Tiles.Count; i++) {
//			map.Add (gameMap.Tiles [i].tileNumber);
//		}
//		GAME.map = map;
//
//		StorageService storageService = App42API.BuildStorageService ();
//		storageService.UpdateDocumentByDocId (DB_NAME, COLLECTION_GAME, GAME_ID, JsonUtility.ToJson (GAME), new SaveGameCallBack ());
    }



    public static void SAVE_CARD(float timeToPlay, int cardInGame, List<CardData> cardsInHand, int cardPlayed, bool correct, List<PlayerData> players)
    {
//		CardPlayData cardPlayedData = new CardPlayData ();
//		cardPlayedData.gameId = GAME_ID;
//		cardPlayedData.timeToPlay = timeToPlay;
//		cardPlayedData.cardInGame = cardInGame;
//		cardPlayedData.cardsInHand = cardsInHand;
//		cardPlayedData.cardPlayed = cardPlayed;
//		cardPlayedData.correct = correct;
//		cardPlayedData.players = players;
//
//		StorageService storageService = App42API.BuildStorageService ();
//		storageService.InsertJSONDocument (DB_NAME, COLLECTION_CARDS, JsonUtility.ToJson (cardPlayedData), new SaveCardCallBack ());
    }

    //  private class SaveCardCallBack : App42CallBack
    //  {
    //    public void OnSuccess(object response)
    //    {
    //    }
    //
    //    public void OnException(System.Exception ex)
    //    {
    //    }
    //
    //  }

    //  private class SaveIndexCallBack : App42CallBack
    //  {
    //    public void OnSuccess(object response)
    //    {
    //			Storage storage = (Storage)response;
    //			IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList ();
    //			for (int i = 0; i < jsonDocList.Count; i++) {
    //				LOGIN_ID = jsonDocList [i].GetDocId ();
    //			}
    //    }
    //
    //    public void OnException(System.Exception ex)
    //    {
    //			Debug.Log (ex.Message);
    //    }
    //
    //  }

    //  private class SaveGameCallBack : App42CallBack
    //  {
    //    public void OnSuccess(object response)
    //    {
    //			Storage storage = (Storage)response;
    //			IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList ();
    //			for (int i = 0; i < jsonDocList.Count; i++) {
    //				GAME_ID = jsonDocList [i].GetDocId ();
    //			}
    //    }
    //
    //    public void OnException(System.Exception ex)
    //    {
    //			Debug.Log (ex.Message);
    //    }
    //
    //  }
	
}


