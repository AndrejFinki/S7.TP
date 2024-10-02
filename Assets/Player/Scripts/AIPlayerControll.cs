using UnityEngine;
using System.Collections;

public class AIPlayerControll : BasePlayerControll
{

    private Card[] cards;
    private int[] numbers;
    private int selectedCard;

    private IEnumerator TileClicked()
    {

        yield return new WaitForSeconds(0.2f);
        tileSelectedCallback(cards[selectedCard].ActionAiTile());

    }

    private IEnumerator CardClicked()
    {

        yield return new WaitForSeconds(0.3f);
        int maxScore = int.MinValue;
        selectedCard = -1;
        for (int i = 0; i < cards.Length; i++)
        {
            int score = cards[i].ActionAiScore();
            if (score > maxScore)
            {
                maxScore = score;
                selectedCard = i;
            }
        }
        cardSelectedCallback(selectedCard);

    }

    private IEnumerator NumberClicked()
    {
		
        yield return new WaitForSeconds(0.3f);
//		for (int i = 0; i < numbers.Length; i++) {
//			for (int j = 0; j < cards.Length; j++) {
//
//				if (cards[j]
//			}
//			int score = cards[i].ActionAiScore ();
//			if (score > maxScore) {
//				maxScore = score;
//				selectedCard = i;
//			}
//		}
        numberSelectedCallback(numbers[Random.Range(0, numbers.Length)]);
		
    }

    public override void SetSelectableCards(Card[] cards)
    {
        this.cards = cards;
    }

    public override void SelectCard(CardSelected callback)
    {
        cardSelectedCallback = callback;
        StartCoroutine("CardClicked");
    }

    public override void SetSelectableNumbers(int[] numbers)
    {
        this.numbers = numbers;
    }

    public override void SelectNumber(NumberSelected callback)
    {
        numberSelectedCallback = callback;
        StartCoroutine("NumberClicked");
    }

    public override void SelectTile(TileSelected callback)
    {
        tileSelectedCallback = callback;
        StartCoroutine("TileClicked");
    }

    public override void SelectedCardPlayed()
    {

    }

    public override void SelectedCardWillPlay()
    {

    }

}
