using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Popup : MonoBehaviour {

	public Animator animator;
	public Image image;
	public Text text;
	public Sprite correctImage;
	public Sprite wrongImage;
	public TIMMultilanguage.MultilanguageString correctString;
	public TIMMultilanguage.MultilanguageString wrongString;

	public void ShowPopup (bool correct) {
		if (correct) {
			image.sprite = correctImage;
			text.text = correctString.GetString();
		} else {
			image.sprite = wrongImage;
			text.text = wrongString.GetString();
		}
		animator.SetTrigger("Popup");
	}
}
