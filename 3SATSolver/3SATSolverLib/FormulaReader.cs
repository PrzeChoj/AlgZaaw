using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3SATSolverLib
{
    public static class FormulaReader
    {
        public static Formula ReadFromFile(string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            if (lines.Length == 0)
                throw new ArgumentException("Empty input");

            string[] split = lines[0].Split(' ');
            if(split.Length != 2)
                throw new ArgumentException("Wrong first line");
            int variables = int.Parse(split[0]);
            int clauses = int.Parse(split[1]);

            Formula formula = new Formula();
            for (int i = 1; i <= clauses; i++)
            {
                Clause c = new Clause();
                string[] spl = lines[i].Split(' ');
                if(spl.Length != 3)
                    throw new ArgumentException($"Wrong Clause {i}");
                for (int j = 0; j < spl.Length; ++j)
                {
                    int num = int.Parse(spl[j]);
                    c.AddLiteral(new Literal(Math.Abs(num), num < 0));
                }
                formula.AddClause(c);
            }

            return formula;
        }

        public static Formula ReadFromConsole()
        {
            // The first line says how big will be th input
            string? line = Console.ReadLine();

            if (line == null)
                throw new ArgumentException("Empty input");

            string[] split = line.Split(' ');
            if(split.Length != 2)
                throw new ArgumentException("Wrong first line");
            int variables = int.Parse(split[0]);
            int clauses = int.Parse(split[1]);

            // The rest of the lines:
            Formula formula = new Formula();
            for (int i = 1; i <= clauses; i++)
            {
                Clause c = new Clause();
                line = Console.ReadLine();
                if (line == null)
                    throw new ArgumentException("Clause expected");
                string[] spl = line.Split(' ');
                if(spl.Length != 3)
                    throw new ArgumentException($"Wrong Clause {i}");
                for (int j = 0; j < spl.Length; ++j)
                {
                    int num = int.Parse(spl[j]);
                    c.AddLiteral(new Literal(Math.Abs(num), num < 0));
                }
                formula.AddClause(c);
            }

            return formula;
        }
    }

    public static class SolutionWriter
    {
        public static void WriteToFile(string filename, Formula formula)
        {
            throw new NotImplementedException();
        }

        public static void WriteToConsole(Formula formula)
        {
            throw new NotImplementedException();
        }
    }
}
