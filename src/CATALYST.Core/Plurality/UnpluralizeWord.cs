namespace CATALYST.Core.Plurality
{
	public interface IUnpluralizeWord
	{
		string Execute(string word);
	}

	public class UnpluralizeWord : IUnpluralizeWord
	{
		public string Execute(string word)
		{
			if (string.IsNullOrEmpty(word)) return string.Empty;

			if (word.EndsWith("ies")) return word.Substring(0, word.Length - 3) + "y";

			if (word.EndsWith("s")) return word.Substring(0, word.Length - 1);

			return word;
		}
	}
}
