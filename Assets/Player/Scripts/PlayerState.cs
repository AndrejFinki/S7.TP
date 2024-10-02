using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void TurnEnd ();

[RequireComponent (typeof(PlayerMovement))]
public class PlayerState : MonoBehaviour
{

	public enum States
	{
		Sleep,
		Playcard,
		SelectNumber,
		Finished
	}

	public enum ID
	{
		Wilber,
		GNU,
		Kit
	}

	public ID id;
	public int handSize;
	public BasePlayerControll playerControll;
	public int bin;
	public int mistakes;
	public int correct;

	public States CurrentState {
		get { return state; }
	}


	private Card[] cards;
	private TurnEnd turnEndCallback;
	private States state;
	private PlayerMovement movement;
	private int selectedCard;

	void Awake ()
	{

		movement = GetComponent<PlayerMovement> ();
		state = States.Sleep;
		cards = new Card[handSize];

	}

	void Start ()
	{

		for (int i = 0; i < handSize; i++) {
			cards [i] = Deck.getInstance ().DrawCard ();
		}
		playerControll.SetSelectableCards (cards);

	}

	private void CardSelected (int selectedCard)
	{

		if (state == States.Playcard) {
			this.selectedCard = selectedCard;
			cards [selectedCard].Play (CardPlayed, playerControll);
		}

	}

	private void CardPlayed (Card cardPlayed, bool successful)
	{

		if (state == States.Playcard) {
            state = States.Sleep;
            if (!successful) {
                turnEndCallback ();
            } else {
                cards [selectedCard] = Deck.getInstance ().DrawCard ();
//                playerControll.SetSelectableCards (cards);
                if (movement.CurrentTile == GameMap.getInstance ().Tiles.Count - 1) {
                    state = States.Finished;
                    Messenger.Broadcast<PlayerState> (MessengerIDs.PLAYER_GAME_FINISHED, this);
                    turnEndCallback ();
                } else {
                    if (Deck.getInstance ().NumberCardsInDeck ()) {
                        state = States.SelectNumber;
                        int[] number = new int[3];
                        List<int> numbersSource = new List<int> () { 1, 2, 3, 4, 5 };
                        for (int i = 0; i < number.Length; i++) {
                            int rand = Random.Range (0, numbersSource.Count); 
                            number [i] = numbersSource [rand];
                            numbersSource.RemoveAt (rand);
                        }
                        playerControll.SetSelectableNumbers (number);
                        playerControll.SelectNumber (NumberSelected);
                    } else {
                        turnEndCallback ();
                    }
                }
            }
			

		}

	}

	private void NumberSelected (int number)
	{

		if (state == States.SelectNumber) {
			bin = number;
			state = States.Sleep;
			turnEndCallback ();
		}

	}

	public void StartTurn (TurnEnd endTurn)
	{

		if (state == States.Sleep) {
			Messenger.Broadcast<PlayerState> (MessengerIDs.PLAYER_START_TURN, this);
			state = States.Playcard;
			turnEndCallback = endTurn;
			playerControll.SetSelectableCards (cards);
			playerControll.SelectCard (CardSelected);
		}
		if (state == States.Finished) {
			if (turnEndCallback != null) {
				turnEndCallback ();
			}
		}

	}
	
}
