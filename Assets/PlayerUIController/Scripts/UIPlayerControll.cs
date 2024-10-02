using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TIMMultilanguage;

//using Parse;

public class UIPlayerControll : BasePlayerControll
{

    public Popup popup;
    public TIM.UI.SelectableGroup cardsSelectables;
    public TIM.UI.SelectableGroup tilesSelectables;
    public CardSlotController[] slots;
    public Text[] numberSlots;
    public Animator numbersAnimator;
    public Animator guideText;
    public CameraMethodSwitch cameraFollow;

    public AudioClip correct;
    public AudioClip wrong;
    public AudioClip intro;

    private bool selectCard;
    private bool selectTile;
    private bool selectNumber;

    private int selectedCard;
    private AudioSource audioSource;

    private Card[] cards;
    private float time;
    private int cardCount;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        selectedCard = int.MinValue;
        Messenger.AddListener<Tile>(MessengerIDs.TILE_CLICKED, TileClicked);

    }

    void Update()
    {
        time += Time.deltaTime;
    }

    public void TileClicked(Tile tile)
    {
        tilesSelectables.CanBeSelected(false);
        if (!selectTile)
            return;
        cameraFollow.SetCameraMode(true);
        selectTile = false;
        bool correct = tileSelectedCallback(tile);
        cardCount++;

        List<Game.CardData> hand = new List<Game.CardData>();
        for (int i = 0; i < cards.Length; i++)
        {
            Game.CardData card = new Game.CardData();
            card.type = cards[selectedCard].type;
            card.data = cards[i].GetComponent<CardUIData>().ToString();
            hand.Add(card);
        }

        List<Game.PlayerData> players = new List<Game.PlayerData>();
        PlayerQueue playerQueue = PlayerQueue.getInstance();
        for (int i = 0; i < playerQueue.players.Length; i++)
        {
            Game.PlayerData player = new Game.PlayerData();
            player.tilePosition = playerQueue.players[i].GetComponent<PlayerMovement>().currentTile;
            if (playerQueue.players[i] == playerQueue.GetCurrentPlayer())
            {
                player.type = "player";
            }
            else
            {
                player.type = "bot";
            }
            players.Add(player);
        }

        Game.SAVE_CARD(time, cardCount, hand, selectedCard, correct, players);


        if (!correct)
        {
            audioSource.clip = wrong;
            if (BackgroundMusic.BG_MUSIC_ON)
            {
                audioSource.Play();
            }
            selectedCard = -1;
            cardsSelectables.DeselectAll();
            tilesSelectables.DeselectAll();

        }
        else
        {
            audioSource.clip = this.correct;
            if (BackgroundMusic.BG_MUSIC_ON)
            {
                audioSource.Play();
            }
        }
        popup.ShowPopup(correct);
    }

    public void CardClicked(int card)
    {
        if (!selectCard && !selectTile)
        {
            return;
        }
        else
        {
            selectedCard = card;
            selectCard = false;
            cardSelectedCallback(card);
        }

    }

    public void NumberClicked(int number)
    {

        if (!selectNumber)
        {
            return;
        }
        numbersAnimator.SetBool("NumbersOn", false);
        selectNumber = false;
        numberSelectedCallback(int.Parse(numberSlots[number].text));

    }

    public override void SetSelectableCards(Card[] cards)
    {
        time = 0;
        this.cards = cards;
        for (int i = 0; i < cards.Length && i < slots.Length; i++)
        {
            if (selectedCard == int.MinValue)
            {
                CardUI card = cards[i].GetComponent<CardUIData>().CreateUIInstance();
                slots[i].ChangeCard(card, false);
            }
            if (selectedCard == i)
            {
                CardUI card = cards[i].GetComponent<CardUIData>().CreateUIInstance();
                slots[i].ChangeCard(card, true);
            }
        }
        selectedCard = -1;
        cardsSelectables.DeselectAll();
        tilesSelectables.DeselectAll();

    }

    public override void SelectCard(CardSelected callback)
    {

        audioSource.clip = intro;
        if (BackgroundMusic.BG_MUSIC_ON)
        {
            audioSource.Play();
        }
        guideText.SetTrigger("Show");
        cameraFollow.SetCameraMode(false);
        cardSelectedCallback = callback;
        selectCard = true;

    }

    public override void SetSelectableNumbers(int[] numbers)
    {

        for (int i = 0; i < numberSlots.Length; i++)
        {
            numberSlots[i].text = numbers[i].ToString();
        }

    }

    public override void SelectNumber(NumberSelected callback)
    {

        numbersAnimator.SetBool("NumbersOn", true);
        numberSelectedCallback = callback;
        selectNumber = true;

    }

    public override void SelectTile(TileSelected callback)
    {
        cameraFollow.SetCameraMode(false);
        tileSelectedCallback = callback;
        selectTile = true;
        tilesSelectables.DeselectAll();
        tilesSelectables.CanBeSelected(true);
    }

    public override void SelectedCardWillPlay()
    {
        selectCard = false;
        selectTile = false;
        slots[selectedCard].HideCard();
    }

    public override void SelectedCardPlayed()
    {
        tilesSelectables.DeselectAll();
    }

    void OnDestroy()
    {

        Messenger.RemoveListener<Tile>(MessengerIDs.TILE_CLICKED, TileClicked);

    }

}
