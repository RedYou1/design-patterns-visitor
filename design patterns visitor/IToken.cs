namespace design_patterns_visitor
{
	public interface IToken
	{
		IEnumerable<string> Generate(ILanguage language);
	}
}
