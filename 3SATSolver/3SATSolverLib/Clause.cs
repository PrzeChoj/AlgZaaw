using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3SATSolverLib
{
    public class Clause
    {
        List<Literal> literals = new List<Literal>();

        public int MaxVariableIndex => literals.Select(l => l.VariableNumber).Max();

        public int Count => literals.Count;

        public void AddLiteral(Literal literal) => literals.Add(literal);
        public bool RemoveLiteral(Literal literal) => literals.Remove(literal);
    }
}
