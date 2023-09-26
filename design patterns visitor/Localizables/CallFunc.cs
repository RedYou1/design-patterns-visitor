namespace design_patterns_visitor.Localizables
{
	public class CallFunc : IAction
	{
		public required Class? Parent { get; init; }
		public required Func Func { get; init; }
		public required IAction? For { get; init; }
		public required IEnumerable<IAction> Parameters { get; init; }

		public IEnumerable<string> Generate(ILanguage language)
			=> language.Generate(this);
	}
}
