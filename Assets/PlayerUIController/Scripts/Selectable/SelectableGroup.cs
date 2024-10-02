using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace TIM.UI
{

	public class SelectableGroup : MonoBehaviour
	{

		private List<Selectable> selectables;

		void Awake ()
		{
			selectables = new List<Selectable> ();
		}

		private void Selected (Selectable selectable)
		{
			for (int i = 0; i < selectables.Count; i++) {
				if (selectables [i] != selectable) {
					selectables [i].Deselect ();
				}
			}
		}

		public void RegisterSelectable (Selectable selectable)
		{
			selectables.Add (selectable);
			selectable.selected.AddListener (Selected);
		}

		public void CanBeSelected (bool value)
		{
			for (int i = 0; i < selectables.Count; i++) {
				selectables [i].canBeSelected = value;
			}
		}

		public void DeselectAll ()
		{
			for (int i = 0; i < selectables.Count; i++) {
				selectables [i].Deselect ();
			}
		}

	}

}