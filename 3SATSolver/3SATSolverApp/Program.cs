using _3SATSolverLib;

Formula formula;
if (args.Length > 0)
    formula = FormulaReader.ReadFromFile(args[0]);
else
    formula = FormulaReader.ReadFromConsole();

formula.Solve();

SolutionWriter.WriteToConsole(formula);


// Testy jednostkowe (trzeba upublicznic metody):
/*
Clause clause;

clause = new Clause();
clause.AddLiteral(new Literal(1, false));
clause.AddLiteral(new Literal(2, false));
clause.AddLiteral(new Literal(3, false));

Console.WriteLine(clause.Count); // 3
Console.WriteLine(Formula._setVariableSingleClause(clause.Copy(), Assignment.True, 3).Count); // 0
Console.WriteLine(clause.Count); // 3

clause = new Clause();
clause.AddLiteral(new Literal(1, false));
clause.AddLiteral(new Literal(2, false));
clause.AddLiteral(new Literal(3, false));

Console.WriteLine(clause.Count); // 3
Console.WriteLine(Formula._setVariableSingleClause(clause.Copy(), Assignment.False, 3).Count); // 2
Console.WriteLine(clause.Count); // 3
*/

/*
Clause clause;

clause = new Clause();
clause.AddLiteral(new Literal(1, false));
clause.AddLiteral(new Literal(1, false));
clause.AddLiteral(new Literal(2, false));

Console.WriteLine(clause.Count); // 3
Console.WriteLine(Formula._simplifyClause(clause).Count); // 2
Console.WriteLine(clause.Count); // 2

clause = new Clause();
clause.AddLiteral(new Literal(1, false));
clause.AddLiteral(new Literal(1, false));
clause.AddLiteral(new Literal(1, false));

Console.WriteLine(clause.Count); // 3
Console.WriteLine(Formula._simplifyClause(clause).Count); // 1
Console.WriteLine(clause.Count); // 1

clause = new Clause();
clause.AddLiteral(new Literal(1, false));
clause.AddLiteral(new Literal(1, true));
clause.AddLiteral(new Literal(2, false));

Console.WriteLine(clause.Count); // 3
Console.WriteLine(Formula._simplifyClause(clause).Count); // 0
Console.WriteLine(clause.Count); // 0
*/