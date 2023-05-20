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

        public bool ContainLiteral(Literal literal) => _literals.Contains(literal); // TODO(Upewnic sie, ze dziala to na porownaniach zawartosci, a nie indeksow w pamieci)

        public Clause Copy()
        {
            Clause copyClause = new Clause();
            foreach (Literal literal in _literals)
            {
                copyClause.AddLiteral(literal.Copy());
            }
            return copyClause;
        }

        internal int[] ListVariables() // only used in Formula._simplifyClauses(); can be called on list of length 3 or 2
        {
            int[] outList;
            if (_literals.Count == 3)
            {
                outList = new int[3];

                for (int i = 0; i < 3; i++)
                {
                    outList[i] = _literals[i].VariableNumber;
                }
            }
            else if (_literals.Count == 2)
            {
                outList = new int[2];

                for (int i = 0; i < 2; i++)
                {
                    outList[i] = _literals[i].VariableNumber;
                }
            }
            else
            {
                throw new Exception("ListVariables was called on list of length different from 3 and 2.");
            }
            
            return outList;
        }
    }
}
