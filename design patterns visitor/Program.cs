// See https://aka.ms/new-console-template for more information

//https://refactoring.guru/fr/design-patterns/visitor
using design_patterns_visitor;
using design_patterns_visitor.Languages;
using design_patterns_visitor.Localizables;
using design_patterns_visitor.Localizables.std;
using Object = design_patterns_visitor.Localizables.Object;

//Classes des langages.
Class String = new Class
{
	Accessibility = Accessibility.Public,
	Name = "string",
	Variables = Array.Empty<Declaration>(),
};

//quelque action fait avec un interface
List<IToken> tokens = new List<IToken>();

Class Printer = new Class
{
	Accessibility = Accessibility.Internal,
	Name = "Printer",
	Variables = Array.Empty<Declaration>()
};
Printer.Functions = new Func[]
	{
		 new()
		 {
			 Parent = Printer,
			 Accessibility = Accessibility.Public,
			 ReturnType = null,
			 IsStatic = true,
			 Name = "Print",
			 Parameters = new (Class Type, string Name)[]
			 {
				 (String, "name")
			 },
			 Actions = new IAction[]
			 {
				 new Print()
				 {
					 Parameter = new Object()
					 {
						 Name = "name"
					 }
				 }
			 }
		 }
	};
tokens.Add(Printer);

Class Main = new Class
{
	Accessibility = Accessibility.Public,
	Name = "Program",
	Variables = Array.Empty<Declaration>(),
};
Main.Functions = new Func[]
	{
		new()
		{
			Parent = Main,
			Accessibility = Accessibility.Public,
			IsStatic = true,
			ReturnType = null,
			Name = "Main",
			Parameters = Array.Empty<(Class Type, string Name)>(),
			Actions = new IAction[]
			{
				new Declaration()
				{
					Type = String,
					Name = "name",
					Action = new Const(){
						Value = "\"Jean-Christophe\""
					}
				},
				new CallFunc()
				{
					Parent = Main,
					Func = Printer.Functions.First(),
					For = null,
					Parameters = new IAction[]
					{
						new Object()
						{
							Name = "name"
						}
					}
				}
			}
		}
	};
tokens.Add(Main);



//Export dans tous les formats
ILanguage[] languages = new ILanguage[] { new CSharp(), new Python() };

foreach (ILanguage language in languages)
{
	Console.WriteLine($"\nLanguage: {language.Name}");

	foreach (IToken token in tokens)
		foreach (string s in token.Generate(language))
			Console.WriteLine(s);
}