using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class RobotHand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private Animator animator;

	void Start () {
        animator = GetComponent<Animator> ();
	    if(!Deck.getInstance ().NumberCardsInDeck()) {
            gameObject.SetActive(false);
        }
	}

    public void OnPointerEnter (PointerEventData eventData)
    {
        animator.SetBool ("on", true);
    }
    
    public void OnPointerExit (PointerEventData eventData)
    {
        animator.SetBool ("on", false);
    }

    
}
