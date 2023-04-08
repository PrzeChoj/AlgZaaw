namespace _3SATSolverLib
{
    public class Formula
    {
        List<Clause> clauses = new List<Clause>();
        public int MaxVariableIndex => clauses.Select(c => c.MaxVariableIndex).Max();
        public int ClauseCount => clauses.Count;

        public void AddClause(Clause clause) => clauses.Add(clause);
        public bool RemoveClause(Clause clause) => clauses.Remove(clause);

        public Assignment[]? Solve()
        {
            Assignment[] result = new Assignment[MaxVariableIndex];
            if (AssignSolution(result))
                return result;
            return null;
        }

        public bool AssignSolution(Assignment[] assignments)
        {
            return false;
        }
    }
}