using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3SATSolverLib
{
    public struct Literal
    {
        public Literal(int variableNumber, bool negated)
        {
            VariableNumber = variableNumber;
            Negated = negated;
        }

        public int VariableNumber { get; }
        public bool Negated { get; }

        internal Literal Copy()
        {
            return new Literal(VariableNumber, Negated);
        }
    }
}
