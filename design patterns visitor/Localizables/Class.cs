namespace design_patterns_visitor.Localizables
{
	public class Class : IToken
	{
		public required Accessibility Accessibility { get; init; }
		public required string Name { get; init; }
		public required IEnumerable<Declaration> Variables { get; init; }
		public IEnumerable<Func> Functions { get; set; } = Enumerable.Empty<Func>();

		public IEnumerable<string> Generate(ILanguage language)
			=> language.Generate(this);
	}
}
