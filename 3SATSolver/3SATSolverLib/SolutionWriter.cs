using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3SATSolverLib
{
    public static class SolutionWriter
    {
        public static void WriteToFile(string filename, Formula formula)
        {
            string? toPrint = GetSolution(formula);

            if (toPrint == null)
                return;

            File.WriteAllText(filename, toPrint);
        }

        public static void WriteToConsole(Formula formula)
        {
            string? toPrint = GetSolution(formula);

            if (toPrint == null)
                return;

            Console.WriteLine(toPrint);
        }

        private static string? GetSolution(Formula formula)
        {
            if (!formula.IsSolved)
            {
                Console.WriteLine("Cannot save the unsolved formula!");
                return null;
            }

            if (formula.Solution == null)
            {
                return "NO";
            }

            string[] solutions = new string[formula.MaxVariableIndex];

            for (int i = 0; i < formula.MaxVariableIndex; i++)
            {
                solutions[i] = i.ToString() + "=" + formula.Solution[i];
            }

            string solution = string.Join(", ", solutions);

            return string.Join("\n", "YES", solution);
        }
    }
}
