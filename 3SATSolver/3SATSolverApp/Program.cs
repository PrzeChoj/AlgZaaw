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
            try
            {
                formula = FormulaReader.ReadFromFile(args[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}. Press any key to quit...");
                Console.ReadKey();
                return;
            }
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
                try
                {
                    formula = FormulaReader.ReadFromFile(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}. Press any key to quit...");
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                Console.WriteLine("Type formula in proper format (variableCount clauseCount <newline> [list of clauses]):");
                try
                {
                    formula = FormulaReader.ReadFromConsole();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}. Press any key to quit...");
                    Console.ReadKey();
                    return;
                }
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
                string? path = null;
                while (path == null || path.Length == 0) 
                    path = Console.ReadLine();
                SolutionWriter.WriteToFile(path, formula);
            }
        }

        Console.WriteLine("Done. Press any key to exit...");
        Console.ReadKey();
    }
}