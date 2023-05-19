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
        
        private static List<Clause> _setVariables(List<Clause> clauses, Assignment assignment, int indexOdAssignment)
        {
            throw new NotImplementedException();
        }

        private static List<Clause> _simplifyClauses(List<Clause> clauses)
        {
            throw new NotImplementedException();
        }
    }
}