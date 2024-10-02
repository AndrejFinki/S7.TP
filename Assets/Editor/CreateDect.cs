using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateDect {

	[MenuItem ("Deck/Create")]
	public static void Create () 
	{
		ScriptableDeck deck = AssetDatabase.LoadAssetAtPath<ScriptableDeck>("Assets/Cards/Decks/Level4.asset");
		string[] cards = AssetDatabase.FindAssets("", new string[] {"Assets/Cards/Prefabs/MoveX","Assets/Cards/Prefabs/IfXMoveYElseMoveZ","Assets/Cards/Prefabs/ForXMoveY"});
		List<ScriptableDeck.CardInDeck> c = new List<ScriptableDeck.CardInDeck> ();
		deck.deck = new ScriptableDeck.CardInDeck[cards.Length];
		for (int i = 0; i < cards.Length; i++) {
			Card card = AssetDatabase.LoadAssetAtPath<Card>(AssetDatabase.GUIDToAssetPath(cards[i]));
			if (card != null) {
				ScriptableDeck.CardInDeck cc = new ScriptableDeck.CardInDeck ();
				cc.card = card;
				cc.count = 1;
				c.Add(cc);
			}
		}
		deck.deck = c.ToArray ();
//		for (int i = 0; i < deck.deck.Length; i++) {
//			deck.deck[i].count = 1;
//		}
	}
}
