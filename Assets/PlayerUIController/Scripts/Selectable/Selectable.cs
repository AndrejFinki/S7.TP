using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

namespace TIM.UI
{

	[System.Serializable]
	public class Selected : UnityEvent<Selectable>
	{
	}

	public class Selectable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
	{

		public SelectableGroup selectableGroup;
		public bool canBeSelected = true;
		public string highlightedBool = "Highlighted";
		public string selectedBool = "Selected";
        public bool setFirstOnHighlight = false;
		public Selected selected;


		private Animator animator;
		private int highlightedID;
		private int selectedID;
//        private int savedSiblingIndex;
        private bool stateSelected;
        private bool mouseOnTop;

		void Awake ()
		{
			if (selected == null)
				selected = new Selected ();
			animator = GetComponent<Animator> ();
			highlightedID = Animator.StringToHash (highlightedBool);
			selectedID = Animator.StringToHash (selectedBool);
		}

		void Start ()
		{
//            if (setFirstOnHighlight) {
//                savedSiblingIndex = transform.GetSiblingIndex();
//            }
			if (selectableGroup != null) {
				selectableGroup.RegisterSelectable (this);
			}
		}

		public void OnPointerEnter (PointerEventData eventData)
		{
			animator.SetBool (highlightedID, true);
            mouseOnTop = true;
            if (setFirstOnHighlight && !stateSelected) {
				transform.SetAsLastSibling();
			}
		}

		public void OnPointerExit (PointerEventData eventData)
		{
			animator.SetBool (highlightedID, false);
            mouseOnTop = false;
            if (setFirstOnHighlight && !stateSelected) {
//                transform.SetSiblingIndex(savedSiblingIndex);
				transform.SetAsFirstSibling();
            }
		}

		public void OnPointerClick (PointerEventData eventData)
		{
			if (canBeSelected) {
                stateSelected = true;
				animator.SetBool (selectedID, true);
				selected.Invoke (this);
			}
		}

		public void Deselect ()
		{
            if (stateSelected && setFirstOnHighlight && !mouseOnTop) {
//                transform.SetSiblingIndex(savedSiblingIndex);
				transform.SetAsFirstSibling ();
            }
            stateSelected = false;
			animator.SetBool (selectedID, false);
		}

	}
}