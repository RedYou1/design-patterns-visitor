namespace design_patterns_visitor.Localizables.std
{
	public class Print : IAction
	{
		public required IAction Parameter { get; init; }

		public IEnumerable<string> Generate(ILanguage language)
			=> language.Generate(this);
	}
}
