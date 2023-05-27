using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System;

namespace _3SATSolverLib
{
    public class Formula
    {
        private readonly List<Clause> _clauses = new List<Clause>();
        public Assignment[]? Solution { get; private set; } = null;
        public bool IsSolved { get; private set; } = false;

        public int MaxVariableIndex => _clauses.Count == 0 ? 0 : _clauses.Select(c => c.MaxVariableIndex).Max();
        public int VariableCount { get; }
        public int ClauseCount => _clauses.Count;

        public void AddClause(Clause clause) => _clauses.Add(clause);
        public bool RemoveClause(Clause clause) => _clauses.Remove(clause);

        public Formula(int variables)
        {
            VariableCount = variables;
        }

        public bool IsSatisfiedBy(Assignment[] assignments)
        {
            foreach (var clause in  _clauses)
            {
                if (!clause.IsSatisfiedBy(assignments))
                    return false;
            }
            return true;
        }

        public Assignment[]? Solve()
        {
            if (IsSolved)
                return Solution;

            Assignment[] result = new Assignment[VariableCount];
            List<Clause> clausesSimplified = _SimplifyClauses(_clauses);
            Solution = AssignSolution(clausesSimplified, result) ? result : null;
            IsSolved = true;

            if (Solution != null)
            {
                for (int i = 0; i < result.Length; ++i)
                    if (result[i] == Assignment.Unassigned)
                        result[i] = Assignment.True;
            }

            return Solution;
        }

        private bool AssignSolution(List<Clause> clauses, Assignment[] assignments)
        {
            if (clauses.Count == 0)
                return true;

            Clause current = _FindSmallestClause(clauses);

            if (current.Count == 0)
                return false;

            var literals = current.GetLiterals();

            switch (literals.Length)
            {
                case 1:
                    { // This single literal has only one possibility to be
                        int i = literals[0].VariableNumber;
                        assignments[i] = literals[0].Negated ? Assignment.False : Assignment.True;
                        var newClauses = _SetVariable(clauses, assignments[i], i);
                        return AssignSolution(newClauses, assignments);
                    }
                case 2:
                    {
                        int i = literals[0].VariableNumber;
                        int j = literals[1].VariableNumber; // It is sure that i != j, 'cause _SimplifyClauses() was called
                        assignments[i] = literals[0].Negated ? Assignment.False : Assignment.True;
                        var newClauses = _SetVariable(clauses, assignments[i], i);
                        if (AssignSolution(newClauses, assignments))
                            return true;

                        assignments[i] = literals[0].Negated ? Assignment.True : Assignment.False;
                        assignments[j] = literals[1].Negated ? Assignment.False : Assignment.True;
                        newClauses = _SetVariable(clauses, assignments[i], i);
                        newClauses = _SetVariable(newClauses, assignments[j], j);
                        return AssignSolution(newClauses, assignments);
                    }
                case 3:
                    {
                        int i = literals[0].VariableNumber;
                        int j = literals[1].VariableNumber;
                        int k = literals[2].VariableNumber; // It is sure that i, j and k are different, 'cause _SimplifyClauses() was called
                        assignments[i] = literals[0].Negated ? Assignment.False : Assignment.True;
                        var newClauses = _SetVariable(clauses, assignments[i], i);
                        if (AssignSolution(newClauses, assignments))
                            return true;

                        assignments[i] = literals[0].Negated ? Assignment.True : Assignment.False;
                        assignments[j] = literals[1].Negated ? Assignment.False : Assignment.True;
                        newClauses = _SetVariable(clauses, assignments[i], i);
                        newClauses = _SetVariable(newClauses, assignments[j], j);
                        if (AssignSolution(newClauses, assignments))
                            return true;

                        assignments[i] = literals[0].Negated ? Assignment.True : Assignment.False;
                        assignments[j] = literals[1].Negated ? Assignment.True : Assignment.False;
                        assignments[k] = literals[2].Negated ? Assignment.False : Assignment.True;
                        newClauses = _SetVariable(clauses, assignments[i], i);
                        newClauses = _SetVariable(newClauses, assignments[j], j);
                        newClauses = _SetVariable(newClauses, assignments[k], k);
                        return AssignSolution(newClauses, assignments);
                    }
                default:
                    throw new ArgumentException("Clause contains more than 3 literals");
            }
        }

        private static Clause _FindSmallestClause(List<Clause> clauses)
        {
            int minCount = int.MaxValue;
            Clause? minClause = null;
            foreach (var c in clauses)
            {
                if (c.Count < minCount)
                {
                    minCount = c.Count;
                    minClause = c;
                }
            }
            return minClause!;
        }

        private static List<Clause> _SetVariable(List<Clause> clauses, Assignment assignment, int indexOfAssignment)
        {
            // Will delete a variable from clauses appropriately:
            // Exp: _setVariables( (x1 or x2 or x3) and (not x2 or not x3 or x4), True, 2) = (not x3 or x4)
            // NOTE: the assignment has to be True or False here

            return clauses.Select(clause => _SetVariableSingleClause(clause.Copy(), assignment, indexOfAssignment)).Where(clauseVariableAssigned => clauseVariableAssigned != null).Select(c => c!).ToList();
        }

        private static Clause? _SetVariableSingleClause(Clause clause, Assignment assignment, int indexOfAssignment)
        {
            // Jesli assignment == Assignment.False, to spelnialnosc clause jest wtw zawiera on literal negated == True.
            // Jesli assignment == Assignment.True,  to spelnialnosc clause jest wtw zawiera on literal negated == False.
            bool clauseIsSatisfied = clause.ContainLiteral(new Literal(indexOfAssignment, assignment == Assignment.False));
            if (clauseIsSatisfied)
            {
                return null; // Then the check for clauseVariableAssigned != null will be false. (clause with 0 elements is required in algorithm!)
            }

            clause.RemoveLiteral(new Literal(indexOfAssignment, assignment == Assignment.True)); // TODO(Upewnij sie, ze clause jest przekazany jako kopia)

            return clause;
        }

        private static List<Clause> _SimplifyClauses(List<Clause> clauses)
        {
            // Will simplify 3 things in every clause:
            // Exp: _simplifyClauses( (x1 or x1 or x2) and (not x1 or not x2 or not x2) and (x1 or x2 or not x1) ) = (x1 or x2) and (not x1 or not x2)

            return clauses.Select(_SimplifyClause).Where(simplifiedClause => simplifiedClause.Count > 0).ToList();
        }

        private static Clause _SimplifyClause(Clause clause)
        {
            clause = clause.Copy();
            if (clause.Count != 3)
            {
                // If the array length is not equal to 3, it doesn't meet the requirement.
                throw new Exception("_simplifyClause() got the clause of length different from 3.");
            }

            var variablesInClause = clause.ListVariables();

            int variableIdOfNonUnique = _AreAllUnique(variablesInClause);
            if (variableIdOfNonUnique == -1) // All are unique
                return clause;

            Literal literalNonNegated = new Literal(variableIdOfNonUnique, false);
            Literal literalNegated = new Literal(variableIdOfNonUnique, true);

            clause.RemoveLiteral(literalNonNegated);
            clause.RemoveLiteral(literalNegated);

            if (clause.Count == 1) // in the original clause there ware both Negated and NonNegated versions
            {
                // Remove the last literal:
                int index = clause.MaxVariableIndex;
                clause.RemoveLiteral(new Literal(index, true));
                clause.RemoveLiteral(new Literal(index, false));
                return clause; // Then the check for clauseVariableAssigned.Count > 0 will be false.
            }

            variablesInClause = clause.ListVariables(); // update
            variableIdOfNonUnique = _AreAllUnique(variablesInClause); // update

            if (variableIdOfNonUnique == -1) // It is already ok
            {
                return clause;
            }

            clause.RemoveLiteral(literalNonNegated);
            clause.RemoveLiteral(literalNegated);

            return clause;
        }

        private static int _AreAllUnique(int[] numbers) // only used in Formula._simplifyClauses()
        {
            // Create a HashSet to store the unique numbers.
            HashSet<int> uniqueNumbers = new HashSet<int>();

            foreach (int number in numbers)
            {
                // If the number is already present in the HashSet, it's not unique.
                if (uniqueNumbers.Contains(number))
                {
                    return number;
                }

                // Add the number to the HashSet.
                uniqueNumbers.Add(number);
            }

            // All numbers are unique.
            return -1;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{VariableCount} {_clauses.Count}\n");
            foreach (var clause in _clauses)
            {
                var literals = clause.GetLiterals();
                sb.Append((literals[0].Negated ? -1 : 1) * (literals[0].VariableNumber + 1));
                for (int i=1;i<literals.Length; i++)
                {
                    sb.Append($" {(literals[i].Negated ? -1 : 1) * (literals[i].VariableNumber + 1)}");
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}