using design_patterns_visitor.Localizables;
using design_patterns_visitor.Localizables.std;
using Object = design_patterns_visitor.Localizables.Object;

namespace design_patterns_visitor
{
	public interface ILanguage
	{
		string Name { get; }
		IEnumerable<string> Generate(CallFunc callFunc);
		IEnumerable<string> Generate(Class @class);
		IEnumerable<string> Generate(Const @const);
		IEnumerable<string> Generate(Declaration declaration);
		IEnumerable<string> Generate(Func func);
		IEnumerable<string> Generate(Object @object);

		//std
		IEnumerable<string> Generate(Print print);
	}
}
