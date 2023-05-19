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
        
        private static List<Clause> _setVariable(List<Clause> clauses, Assignment assignment, int indexOdAssignment)
        {
            // Will delete a variable from clauses appropriately:
            // Exp: _setVariables( (x1 or x2 or x3) and (not x2 or not x3 or x4), True, 2) = (not x3 or x4)

            return clauses.Select(clause => _setVariableSingle(clause, assignment, indexOdAssignment)).Where(clauseVariableAssigned => clauseVariableAssigned.Count > 0).ToList();
        }

        private static Clause _setVariableSingle(Clause clause, Assignment assignment, int indexOdAssignment)
        {
            throw new NotImplementedException();
        }
        
        private static List<Clause> _simplifyClauses(List<Clause> clauses)
        {
            // Will simplify 3 things in every clause:
            // Exp: _simplifyClauses( (x1 or x1 or x2) and (not x1 or not x2 or not x2) and (x1 or x2 or not x1) ) = (x1  or x2) and (not x1 or not x2)

            return clauses.Select(_simplifyClause).Where(simplifiedClause => simplifiedClause.Count > 0).ToList();
        }

        private static Clause _simplifyClause(Clause clause)
        {
            throw new NotImplementedException();
        }
    }
}