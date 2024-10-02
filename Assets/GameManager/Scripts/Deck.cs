using UnityEngine;
using System.Collections;

public class Deck : MonoBehaviour {

	public static ScriptableDeck deck;
	public ScriptableDeck defaultDeck;

	private ScriptableDeck.CardInDeck[] cards;
	private bool numberCards;

	private static Deck instance;
	public static Deck getInstance () {
		return instance;
	}

	void Awake () {

		instance = this;
		if (deck == null) {
			deck = defaultDeck;
		}
		cards = (ScriptableDeck.CardInDeck[]) deck.deck.Clone ();
		numberCards = deck.NumberCardsInDeck;
	}

	public bool NumberCardsInDeck () {

		return numberCards;

	}

	public Card DrawCard () {

		int totalWeight = 0;
		for (int i = 0; i < cards.Length; i++) {
			totalWeight += cards[i].count;
		}

		Card draw = null;
		int rand = Random.Range(0, totalWeight);
		for (int i = 0; i < cards.Length; i++) {
			if (rand < cards[i].count) {
				cards[i].count--;
				totalWeight--;
				if (totalWeight == 0) {
					cards = (ScriptableDeck.CardInDeck[]) deck.deck.Clone ();
				}
				draw = cards[i].card;
				break;
			}
			rand -= cards[i].count;
		}

		return draw;

	}
	
}
