using _3SATSolverLib;
using System;

class Program
{
    public static void Main(string[] args)
    {
        Formula formula;
        if (args.Length > 0)
        {
            Console.WriteLine($"Reading from file {args[0]}...");
            formula = FormulaReader.ReadFromFile(args[0]);
        }
        else
        {
            int choice = 0;
            while (choice != '1' && choice != '2')
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("\t1. Read formula from file");
                Console.WriteLine("\t2. Type formula manually");
                choice = Console.Read();
                Console.ReadLine();
            }
            
            if (choice == '1')
            {
                Console.Write("Path to file: ");
                string path = Console.ReadLine();
                formula = FormulaReader.ReadFromFile(path);
            }
            else
            {
                Console.WriteLine("Type formula in proper format (variableCount clauseCount <newline> [list of clauses]):");
                formula = FormulaReader.ReadFromConsole();
            }
        }

        formula.Solve();

        SolutionWriter.WriteToConsole(formula);

        {
            int choice = 0;
            while (choice != 'y' && choice != 'Y' && choice != 'n' && choice != 'N')
            {
                Console.Write("Do you want to save solution to file? (Y/N) ");
                choice = Console.Read();
                Console.ReadLine();
            }
            if (choice == 'y' || choice == 'Y')
            {
                Console.WriteLine("Path to file: ");
                string path = Console.ReadLine();
                SolutionWriter.WriteToFile(path, formula);
            }
        }

        Console.WriteLine("Done. Press any key to exit...");
        Console.ReadKey();
    }
}

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