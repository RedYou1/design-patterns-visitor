using design_patterns_visitor.Localizables;
using design_patterns_visitor.Localizables.std;
using Object = design_patterns_visitor.Localizables.Object;

namespace design_patterns_visitor.Languages
{
	public class CSharp : ILanguage
	{
		public string Name { get; } = "C#";

		private string AccessibilityToString(Accessibility accessibility)
			=> Enum.GetName(accessibility)!.ToLower();

		public IEnumerable<string> Generate(CallFunc callFunc)
		{
			List<string> @params = new();
			string? pre = null;
			if (callFunc.For is not null)
			{
				var actions = callFunc.For.Generate(this);
				foreach (string s in actions.SkipLast(1))
					yield return s;
				pre = actions.Last();
			}
			else if (callFunc.Func.Parent is not null &&
				(callFunc.Parent is null || callFunc.Parent != callFunc.Func.Parent))
			{
				pre = callFunc.Func.Parent.Name;
			}

			foreach (IAction action in callFunc.Parameters)
			{
				var actions = action.Generate(this);
				foreach (string s in actions.SkipLast(1))
					yield return s;
				@params.Add(actions.Last());
			}
			yield return $"{(pre is null ? "" : $"{pre}.")}{callFunc.Func.Name}({string.Join(',', @params)});";
		}

		public IEnumerable<string> Generate(Class @class)
		{
			yield return $"{AccessibilityToString(@class.Accessibility)} class {@class.Name} {{";

			foreach (Declaration declaration in @class.Variables)
				foreach (string s in declaration.Generate(this))
					yield return $"\t{s}";

			foreach (Func func in @class.Functions)
				foreach (string s in func.Generate(this))
					yield return $"\t{s}";

			yield return "}";
		}

		public IEnumerable<string> Generate(Const @const)
		{
			yield return @const.Value;
		}

		public IEnumerable<string> Generate(Declaration declaration)
		{
			string? action = null;
			if (declaration.Action is not null)
			{
				var actions = declaration.Action.Generate(this);
				foreach (string s in actions.SkipLast(1))
					yield return s;
				action = actions.Last();
			}
			yield return $"{declaration.Type.Name} {declaration.Name}{(action is null ? "" : $" = {action}")};";
		}

		public IEnumerable<string> Generate(Func func)
		{
			yield return $"{AccessibilityToString(func.Accessibility)} {(
				func.IsStatic ? "static" : "")} {func.ReturnType?.Name ?? "void"} {func.Name}({string.Join(
				", ", func.Parameters.Select((p) => $"{p.Type.Name} {p.Name}"))}){{";
			foreach (IAction action in func.Actions)
				foreach (string s in action.Generate(this))
					yield return $"\t{s}";
			yield return "}";
		}

		public IEnumerable<string> Generate(Object @object)
		{
			yield return @object.Name;
		}

		//std
		public IEnumerable<string> Generate(Print print)
		{
			var t = print.Parameter.Generate(this);
			foreach (string s in t.SkipLast(1))
				yield return s;
			yield return $"Console.WriteLine({t.Last()});";
		}
	}
}
