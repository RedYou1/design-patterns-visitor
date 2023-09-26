using design_patterns_visitor.Localizables;
using design_patterns_visitor.Localizables.std;
using System;
using Object = design_patterns_visitor.Localizables.Object;

namespace design_patterns_visitor.Languages
{
	public class Python : ILanguage
	{
		public string Name { get; } = "Python";

		private string AccessibilityToString(Accessibility accessibility, Type context)
		{
			switch (accessibility, context.Name)
			{
				case (Accessibility.Public, _):
					return "";
				case (Accessibility.Protected, _):
				case (Accessibility.Internal, _):
					return "_";
				case (Accessibility.Private, nameof(Class)):
					return "_";
				case (Accessibility.Private, _):
					return "__";
			}
			throw new NotImplementedException();
		}

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
				string preacc = AccessibilityToString(callFunc.Func.Parent.Accessibility, typeof(Class));
				pre = $"{preacc}{callFunc.Func.Parent.Name}";
			}

			foreach (IAction action in callFunc.Parameters)
			{
				var actions = action.Generate(this);
				foreach (string s in actions.SkipLast(1))
					yield return s;
				@params.Add(actions.Last());
			}
			string acc = AccessibilityToString(callFunc.Func.Accessibility, typeof(Func));
			yield return $"{(pre is null ? "" : $"{pre}.")}{acc}{callFunc.Func.Name}{acc}({string.Join(',', @params)})";
		}

		public IEnumerable<string> Generate(Class @class)
		{
			yield return $"class {AccessibilityToString(@class.Accessibility, typeof(Class))}{@class.Name}:";

			if (!@class.Variables.Any() && !@class.Functions.Any())
			{
				yield return "\tpass";
				yield break;
			}

			foreach (Declaration declaration in @class.Variables)
				foreach (string s in declaration.Generate(this))
					yield return $"\t{s}";
			foreach (Func func in @class.Functions)
				foreach (string s in func.Generate(this))
					yield return $"\t{s}";
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
			yield return $"{declaration.Name}{(action is null ? "" : $" = {action}")}";
		}

		public IEnumerable<string> Generate(Func func)
		{
			var p = func.Parameters.Select((p) => p.Name);
			if (!func.IsStatic)
				p = p.Prepend("self");
			string acc = AccessibilityToString(func.Accessibility, typeof(Func));
			yield return $"def {acc}{func.Name}{acc}({string.Join(
				", ", p)}):";
			if (!func.Actions.Any())
			{
				yield return "\tpass";
				yield break;
			}
			foreach (IAction action in func.Actions)
				foreach (string s in action.Generate(this))
					yield return $"\t{s}";
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
			yield return $"print({t.Last()})";
		}
	}
}
