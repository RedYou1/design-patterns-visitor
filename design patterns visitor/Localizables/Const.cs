namespace design_patterns_visitor.Localizables
{
	public class Const : IAction
	{
		public required string Value { get; init; }

		public IEnumerable<string> Generate(ILanguage language)
			=> language.Generate(this);
	}
}
