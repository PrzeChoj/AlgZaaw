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
            int variables = int.Parse(split[0]);
            int clauses = int.Parse(split[1]);

            Formula formula = new Formula();
            for (int i=1;i<=clauses;i++)
            {
                Clause c = new Clause();
                string[] spl = lines[i].Split(' ');
                for (int j=0;j<spl.Length;++j)
                {
                    int num = int.Parse(spl[j]);
                    if (num < 0)
                        c.AddLiteral(new Literal(-num, true));
                    else
                        c.AddLiteral(new Literal(num, false));
                }
                formula.AddClause(c);
            }

            return formula;
        }

        public static Formula ReadFromConsole()
        {
            string? line = Console.ReadLine();

            if (line == null)
                throw new ArgumentException("Empty input");

            string[] split = line.Split(' ');
            int variables = int.Parse(split[0]);
            int clauses = int.Parse(split[1]);

            Formula formula = new Formula();
            for (int i = 1; i <= clauses; i++)
            {
                Clause c = new Clause();
                line = Console.ReadLine();
                if (line == null)
                    throw new ArgumentException("Clause expected");
                string[] spl = line.Split(' ');
                for (int j = 0; j < spl.Length; ++j)
                {
                    int num = int.Parse(spl[j]);
                    if (num < 0)
                        c.AddLiteral(new Literal(-num, true));
                    else
                        c.AddLiteral(new Literal(num, false));
                }
                formula.AddClause(c);
            }

            return formula;
        }
    }
}
