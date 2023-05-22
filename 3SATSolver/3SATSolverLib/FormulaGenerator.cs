using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3SATSolverLib
{
    public class FormulaGenerator
    {
        Random random = new Random();

        private bool NextBool()
        {
            return random.Next(2) == 0;
        }

        public Formula RandomFormula(int variables, int clauses)
        {
            Formula formula = new Formula(variables);
            for (int i=0;i<clauses;i++)
            {
                Clause clause = new Clause();
                clause.AddLiteral(new Literal(random.Next(variables), NextBool()));
                clause.AddLiteral(new Literal(random.Next(variables), NextBool()));
                clause.AddLiteral(new Literal(random.Next(variables), NextBool()));
                formula.AddClause(clause);
            }
            return formula;
        }
        
        public Formula SatisfiableFormula(int variables, int clauses)
        {
            Formula formula = new Formula(variables);
            Assignment[] assignments = new Assignment[variables];
            for (int i =0;i<variables;i++)
            {
                assignments[i] = NextBool() ? Assignment.True : Assignment.False;
            }
            for (int i=0;i<clauses;++i)
            {
                Clause clause = new Clause();
                int literalIdx = random.Next(3); // index of satisfied literal
                int variableIdx = random.Next(variables);
                for (int j=0;j<3;++j)
                {
                    if (j == literalIdx)
                        clause.AddLiteral(new Literal(variableIdx, assignments[variableIdx] == Assignment.False));
                    else
                        clause.AddLiteral(new Literal(random.Next(variables), NextBool()));
                }
                formula.AddClause(clause);
            }
            return formula;
        }
    }
}
