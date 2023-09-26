namespace design_patterns_visitor.Localizables
{
	public class Declaration : IAction
	{
		public required Class Type { get; init; }
		public required string Name { get; init; }
		public required IAction? Action { get; init; }

		public IEnumerable<string> Generate(ILanguage language)
			=> language.Generate(this);
	}
}
