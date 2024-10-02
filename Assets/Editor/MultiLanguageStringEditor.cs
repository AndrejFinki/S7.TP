using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace TIMMultilanguage
{
	[CustomPropertyDrawer (typeof(MultilanguageString))] 
	public class MultiLanguageStringEditor : PropertyDrawer
	{

		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);

			if (MultilanguageData.Instance != null) {
				List<string> ids = MultilanguageData.Instance.GetIDs ();
				int index = ids.FindIndex (s => s == property.FindPropertyRelative ("id").stringValue);
				index = index == -1 ? 0 : index;
				index = EditorGUI.Popup (position, index, ids.ToArray ());
				property.FindPropertyRelative ("id").stringValue = ids [index];
			} else {
				EditorGUI.LabelField (position, "Missing MultiLanguageManager");
			}
		}

	}
}