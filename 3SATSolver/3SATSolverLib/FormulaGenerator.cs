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

        public Formula UnsatisfiableFormula(int variables, int clauseCount)
        {
            List<Clause> clauses = new List<Clause>();
            Clause c = new Clause();
            c.AddLiteral(new Literal(0, true));
            c.AddLiteral(new Literal(0, true));
            c.AddLiteral(new Literal(0, true));
            clauses.Add(c);
            c = new Clause();
            c.AddLiteral(new Literal(0, false));
            c.AddLiteral(new Literal(0, false));
            c.AddLiteral(new Literal(0, false));
            clauses.Add(c);

            for (int i=1;i<variables;++i)
            {
                if (clauses.Count >= clauseCount)
                    break;

                var oldClauses = clauses.ToArray();

                int n;
                n = random.Next(Math.Min(2 * (clauseCount - clauses.Count) / (variables - i), Math.Min(clauseCount - clauses.Count, clauses.Count)));

                // shuffle
                int[] clIds = new int[clauses.Count];
                for (int j = 0; j < clIds.Length; j++)
                    clIds[j] = j;
                for (int j=clIds.Length - 1;j>0;j--)
                {
                    int swapIdx = random.Next(j + 1);
                    var tmp = clIds[j];
                    clIds[j] = clIds[swapIdx];
                    clIds[swapIdx] = tmp;
                }

                bool[] toChange = new bool[clauses.Count];
                for (int j=0;j<n;j++)
                {
                    toChange[clIds[j]] = true;
                }

                clauses.Clear();
                for (int j=0;j<oldClauses.Length;j++)
                {
                    if (!toChange[j])
                    {
                        clauses.Add(oldClauses[j]);
                    }
                    else
                    {
                        Clause c1 = new Clause(), c2 = new Clause();
                        var literals = oldClauses[j].GetLiterals();
                        int duplicatedIdx = random.Next(3);
                        bool firstToFirst = NextBool();
                        int i1 = (duplicatedIdx+1)%3, i2=(duplicatedIdx+2)%3;
                        if (firstToFirst)
                        {
                            c1.AddLiteral(literals[i1]);
                            c2.AddLiteral(literals[i2]);
                        }
                        else
                        {
                            c1.AddLiteral(literals[i2]);
                            c2.AddLiteral(literals[i1]);
                        }
                        c1.AddLiteral(literals[duplicatedIdx]);
                        c2.AddLiteral(literals[duplicatedIdx]);
                        c1.AddLiteral(new Literal(i, false));
                        c2.AddLiteral(new Literal(i, true));
                        clauses.Add(c1);
                        clauses.Add(c2);
                    }
                }
            }
            Formula formula = new Formula(variables);

            foreach (var clause in clauses)
                formula.AddClause(clause);

            // fill with random data
            for (int i = 0; i < clauseCount - clauses.Count; i++)
            {
                Clause clause = new Clause();
                clause.AddLiteral(new Literal(random.Next(variables), NextBool()));
                clause.AddLiteral(new Literal(random.Next(variables), NextBool()));
                clause.AddLiteral(new Literal(random.Next(variables), NextBool()));
                formula.AddClause(clause);
            }

            return formula;
        }
    }
}
