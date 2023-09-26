namespace design_patterns_visitor.Localizables
{
	public class Object : IAction
	{
		public required string Name { get; init; }

		public IEnumerable<string> Generate(ILanguage language)
			=> language.Generate(this);
	}
}
