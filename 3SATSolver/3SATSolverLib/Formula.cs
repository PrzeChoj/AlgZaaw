namespace _3SATSolverLib
{
    public class Formula
    {
        private readonly List<Clause> _clauses = new List<Clause>();
        public int MaxVariableIndex => _clauses.Select(c => c.MaxVariableIndex).Max();
        public int ClauseCount => _clauses.Count;

        public void AddClause(Clause clause) => _clauses.Add(clause);
        public bool RemoveClause(Clause clause) => _clauses.Remove(clause);

        public Assignment[]? Solve()
        {
            Assignment[] result = new Assignment[MaxVariableIndex];
            return AssignSolution(result) ? result : null;
        }

        private bool AssignSolution(Assignment[] assignments)
        {
            throw new NotImplementedException();
        }
    }
}