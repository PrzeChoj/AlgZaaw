using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3SATSolverLib
{
    public class Clause
    {
        private readonly List<Literal> _literals = new List<Literal>();

        public int MaxVariableIndex => _literals.Select(l => l.VariableNumber).Max();

        public int Count => _literals.Count;

        public void AddLiteral(Literal literal) => _literals.Add(literal);
        public bool RemoveLiteral(Literal literal) => _literals.Remove(literal);
    }
}
