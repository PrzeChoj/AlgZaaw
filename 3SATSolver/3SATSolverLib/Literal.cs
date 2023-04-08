using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3SATSolverLib
{
    public struct Literal
    {
        public int VariableNumber { get; }
        public bool Negated { get; }
    }
}
