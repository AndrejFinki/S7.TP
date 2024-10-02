namespace TIMMultilanguage
{
	[System.Serializable]
	public class MultilanguageString
	{

		public string id;

		public string GetString ()
		{
			return MultilanguageData.Instance.GetText (id);
		}

	}
}
