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

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Literal l = (Literal)obj;
            return VariableNumber == l.VariableNumber && Negated == l.Negated;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(VariableNumber, Negated);
        }
    }
}
