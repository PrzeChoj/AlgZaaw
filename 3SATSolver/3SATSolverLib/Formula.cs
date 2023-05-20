namespace _3SATSolverLib
{
    public class Formula
    {
        private readonly List<Clause> _clauses = new List<Clause>();
        public Assignment[]? Solution { get; private set; } = null;
        public bool IsSolved { get; private set; } = false;
        
        public int MaxVariableIndex => _clauses.Select(c => c.MaxVariableIndex).Max();
        public int ClauseCount => _clauses.Count;

        public void AddClause(Clause clause) => _clauses.Add(clause);
        public bool RemoveClause(Clause clause) => _clauses.Remove(clause);

        public Assignment[]? Solve()
        {
            if (IsSolved)
                return Solution;
            
            Assignment[] result = new Assignment[MaxVariableIndex];
            List<Clause> clausesSimplified = _simplifyClauses(_clauses);
            Solution = AssignSolution(clausesSimplified, result) ? result : null;
            IsSolved = true;
            return Solution;
        }

        private bool AssignSolution(List<Clause> clauses, Assignment[] assignments)
        {
            // TODO()

            for (int i = 0; i < MaxVariableIndex; i++)
            {
                assignments[i] = Assignment.True;
            }

            return true;
        }
        
        private static List<Clause> _setVariable(List<Clause> clauses, Assignment assignment, int indexOfAssignment)
        {
            // Will delete a variable from clauses appropriately:
            // Exp: _setVariables( (x1 or x2 or x3) and (not x2 or not x3 or x4), True, 2) = (not x3 or x4)
            // NOTE: the assignment has to be True or False here

            return clauses.Select(clause => _setVariableSingleClause(clause.Copy(), assignment, indexOfAssignment)).Where(clauseVariableAssigned => clauseVariableAssigned.Count > 0).ToList();
        }

        private static Clause _setVariableSingleClause(Clause clause, Assignment assignment, int indexOfAssignment)
        {
            // Jesli assignment == Assignment.False, to spelnialnosc clause jest wtw zawiera on literal negated == True.
            // Jesli assignment == Assignment.True,  to spelnialnosc clause jest wtw zawiera on literal negated == False.
            bool clauseIsSatisfied = clause.ContainLiteral(new Literal(indexOfAssignment, assignment == Assignment.False));
            if (clauseIsSatisfied)
            {
                return new Clause(); // Then the check for clauseVariableAssigned.Count > 0 will be false.
            }

            clause.RemoveLiteral(new Literal(indexOfAssignment, assignment == Assignment.True)); // TODO(Upewnij sie, ze clause jest przekazany jako kopia)

            return clause;
        }
        
        private List<Clause> _simplifyClauses(List<Clause> clauses)
        {
            // Will simplify 3 things in every clause:
            // Exp: _simplifyClauses( (x1 or x1 or x2) and (not x1 or not x2 or not x2) and (x1 or x2 or not x1) ) = (x1 or x2) and (not x1 or not x2)

            return clauses.Select(_simplifyClause).Where(simplifiedClause => simplifiedClause.Count > 0).ToList();
        }

        private static Clause _simplifyClause(Clause clause)
        {
            // Note: The clause is not a copy, but we want it that way
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
                clause.RemoveLiteral(new Literal(clause.MaxVariableIndex, true));
                clause.RemoveLiteral(new Literal(clause.MaxVariableIndex, false));
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
    }
}