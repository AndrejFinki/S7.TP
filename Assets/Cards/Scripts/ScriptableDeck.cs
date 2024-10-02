using UnityEngine;
using System.Collections;

[CreateAssetMenuAttribute]
public class ScriptableDeck : ScriptableObject
{

	[System.Serializable]
	public struct CardInDeck
	{
		
		public Card card;
		public int count;
		
	}

	public bool NumberCardsInDeck;
	public CardInDeck[] deck;

}
