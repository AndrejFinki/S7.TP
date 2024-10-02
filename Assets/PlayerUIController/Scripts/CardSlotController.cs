using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardSlotController : MonoBehaviour {

    public Transform slot;
    public Animator animator;

    private CardUI card;

	public void ChangeCardNow() {
        animator.SetBool("ChangeCard", false);
        var children = new List<GameObject> ();
        foreach (Transform child in slot)
            children.Add (child.gameObject);
        children.ForEach (child => Destroy (child));
        card.transform.SetParent (slot, false);
    }

    public void ChangeCard(CardUI card, bool animated) {
        this.card = card;
        if (animated) {
            animator.SetBool("ChangeCard", true);
        } else {
            ChangeCardNow ();
        }
    }

    public void HideCard() {
        animator.SetBool("Hide", true);
    }
    
}
