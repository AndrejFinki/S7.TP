using UnityEngine;
using System.Collections;
using TIMMultilanguage;

public class CardUIData : MonoBehaviour
{

	public CardUI uiPrefab;
	public Sprite cardBorder;
	public Color cardTextColor;
	public MultilanguageString cardText;
	public string tileType;
	public string ifSign;
	public string ifValue;
	public string thenValue;
	public string elseValue;

	public CardUI CreateUIInstance ()
	{

		CardUI cardUi = Instantiate (uiPrefab).GetComponent<CardUI> ();

		if (cardUi.cardBorder != null) {
			cardUi.cardBorder.sprite = cardBorder;
		}
		if (cardUi.cardText != null) {
			cardUi.cardText.color = cardTextColor;
			cardUi.cardText.text = string.Format (cardText.GetString (), tileType, ifSign, ifValue, thenValue, elseValue);
		}

		return cardUi;

	}

	public override string ToString ()
	{
		return string.Format ("[CardData: tileType={0}, ifSign={1}, ifValue={2}, thenValue={3}, elseValue={4}]", tileType, ifSign, ifValue, thenValue, elseValue);
	}
	

}
