using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TIMMultilanguage
{
	[System.Serializable]
	[RequireComponent (typeof(Text))]
	public class MultilanguageText : MonoBehaviour
	{

		public MultilanguageString text;

		private Text uiText;

		void Awake ()
		{
			MultilanguageData.Instance.OnLanguageChange += LanguageChanged;
			LanguageChanged ();
		}

		public void LanguageChanged ()
		{
			if (uiText == null)
				uiText = GetComponent<Text> ();
			uiText.text = text.GetString ();
		}

		void OnValidate ()
		{
			LanguageChanged ();
		}

		void OnDestroy ()
		{
			if (MultilanguageData.Instance != null)
				MultilanguageData.Instance.OnLanguageChange -= LanguageChanged;
		}

	}
}