namespace design_patterns_visitor.Localizables
{
	public class Func : IToken
	{
		public required Class? Parent { get; init; }
		public required Accessibility Accessibility { get; init; }
		public required bool IsStatic { get; init; }
		public required Class? ReturnType { get; init; }
		public required string Name { get; init; }
		public required (Class Type, string Name)[] Parameters { get; init; }
		public required IEnumerable<IAction> Actions { get; init; }

		public IEnumerable<string> Generate(ILanguage language)
			=> language.Generate(this);
	}
}
