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
            Solution = AssignSolution(result) ? result : null;
            IsSolved = true;
            return Solution;
        }

        private bool AssignSolution(Assignment[] assignments)
        {
            throw new NotImplementedException();
        }
    }
}