using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using LumenWorks.Framework.IO.Csv;

namespace TIMMultilanguage
{

	public class MultilanguageData : ScriptableObject
	{

	
		private const string multilanguagePath = "TIMMultilanguage/Resources";
		const string multilanguageAssetName = "Multilanguage";
		const string multilanguageAssetExtension = ".asset";

		private static MultilanguageData instance;

		public static MultilanguageData Instance {
			get {
				if (instance == null) {
					instance = Resources.Load (multilanguageAssetName) as MultilanguageData;
					#if UNITY_EDITOR
					if (instance == null) {
						// If not found, autocreate the asset object.
						instance = CreateInstance<MultilanguageData> ();
						string properPath = Path.Combine (Application.dataPath, multilanguagePath);
						if (!Directory.Exists (properPath)) {
							AssetDatabase.CreateFolder ("Assets/TIMMultilanguage", "Resources");
						}
						string fullPath = Path.Combine (Path.Combine ("Assets", multilanguagePath),
							                  multilanguageAssetName + multilanguageAssetExtension);
						AssetDatabase.CreateAsset (instance, fullPath);
					}
					#endif
				}
				return instance;
			}
		}

		#if UNITY_EDITOR
		[MenuItem ("Window/TIMMultilanguage/Edit Settings")]
		public static void Edit ()
		{
			Selection.activeObject = Instance;
		}
		#endif

		public delegate void LanguageChanged ();

		public event LanguageChanged OnLanguageChange;

		public TextAsset textAsset;

		private string loadedText;
		private List<string> languages;
		private List<string> ids;
		private Dictionary<string, Dictionary<string, string>> text;

		private const string PP_TAG = "MULTILANGUAGE_LANGUAGE_SET";
		private string currentLanguage;

		void OnEnable ()
		{
			LoadText ();
		}

		private bool AlreadyLoadedAsset ()
		{
			return loadedText != null && loadedText == textAsset.text && text != null;
		}

		private void LoadText ()
		{

			if (AlreadyLoadedAsset ())
				return;
		
			loadedText = textAsset.text;
			text = new Dictionary<string, Dictionary<string, string>> ();
			ids = new List<string> ();
			languages = new List<string> ();

			using (CsvReader csv = new CsvReader (new StringReader (loadedText), true)) {
				int fieldCount = csv.FieldCount;
				string[] headers = csv.GetFieldHeaders ();
				languages.AddRange (headers);
				languages.RemoveAt (0);
				while (csv.ReadNextRecord ()) {
					text [csv [0]] = new Dictionary<string, string> ();
					ids.Add (csv [0]);
					for (int i = 1; i < fieldCount; i++)
						text [csv [0]] [headers [i]] = csv [i];
				}
			}

			currentLanguage = PlayerPrefs.GetString (PP_TAG);
			if (currentLanguage == null || !languages.Contains (currentLanguage))
				currentLanguage = languages [0];
		}

		public void SetLanguage (string language)
		{
			if (languages.Contains (language)) {
				currentLanguage = language;
				PlayerPrefs.SetString (PP_TAG, language);
				if (OnLanguageChange != null) {
					OnLanguageChange ();
				}
			}
		}

		public string GetText (string id, string language)
		{
			LoadText ();
			if (language != null && languages.Contains (language)) {
				if (text.ContainsKey(id)) {
					return text [id] [language];
				} else {
					Debug.LogError("Multi language: " + id + "not found");
				}
			}
			return null;
		}

		public string GetText (string id)
		{
			return GetText (id, currentLanguage);
		}

		public List<string> GetLanguages ()
		{
			LoadText ();
			return languages;
		}

		public List<string> GetIDs ()
		{
			LoadText ();
			return ids;
		}
	
	}
}
