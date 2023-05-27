using _3SATSolverLib;
using System.Diagnostics;
using System.Text;

FormulaGenerator fg = new FormulaGenerator();
StringBuilder sb = new StringBuilder();

void TestSATGroup(int vars)
{
    int clauseFactor = 1;
    TestSAT(vars, vars * clauseFactor);
    clauseFactor = 5;
    TestSAT(vars, vars * clauseFactor);
    clauseFactor = 10;
    TestSAT(vars, vars * clauseFactor);
    clauseFactor = 50;
    TestSAT(vars, vars * clauseFactor);
    clauseFactor = 100;
    TestSAT(vars, vars * clauseFactor);
    clauseFactor = 500;
    TestSAT(vars, vars * clauseFactor);
    clauseFactor = 1000;
    TestSAT(vars, vars * clauseFactor);
    clauseFactor = 15;
    TestSAT(vars, vars * clauseFactor);
    clauseFactor = 5;
    TestSAT(vars, vars * clauseFactor);
    clauseFactor = 10;
    TestSAT(vars, vars * clauseFactor);
    clauseFactor = 50;
    TestSAT(vars, vars * clauseFactor);
    clauseFactor = 100;
    TestSAT(vars, vars * clauseFactor);
    clauseFactor = 500;
    TestSAT(vars, vars * clauseFactor);
    clauseFactor = 1000;
    TestSAT(vars, vars * clauseFactor);
}

string GetResult(Formula formula, Assignment[]? assignments)
{
    if (assignments == null)
        return "NO";
    if (formula.IsSatisfiedBy(assignments))
        return "YES (OK)";
    else
        return "YES (NOT OK)";
}

void TestSAT(int vars, int clauses)
{
    Console.WriteLine($"Testing v={vars}, c={clauses}");
    Formula rnd = fg.RandomFormula(vars, clauses);
    Stopwatch sw = Stopwatch.StartNew();
    var a = rnd.Solve();
    sw.Stop();

    sb.AppendLine($"{vars};{clauses};Random;{GetResult(rnd, a)};{sw.ElapsedMilliseconds}");

    Formula sat = fg.SatisfiableFormula(vars, clauses);
    sw = Stopwatch.StartNew();
    a = sat.Solve();
    sw.Stop();

    sb.AppendLine($"{vars};{clauses};Satisfiable;{GetResult(sat, a)};{sw.ElapsedMilliseconds}");

    Formula unsat = fg.UnsatisfiableFormula(vars, clauses);
    sw = Stopwatch.StartNew();
    a = unsat.Solve();
    sw.Stop();

    sb.AppendLine($"{vars};{clauses};Unsatisfiable;{GetResult(unsat, a)};{sw.ElapsedMilliseconds}");
    File.AppendAllText("results.csv", sb.ToString());
    sb.Clear();
}

sb.AppendLine($"variables;clauses;type;result;timeMs");
int vars = 3;
TestSATGroup(vars);
vars = 5;
TestSATGroup(vars);
vars = 10;
TestSATGroup(vars);
vars = 15;
TestSATGroup(vars);
vars = 20;
TestSATGroup(vars);
vars = 50;
TestSATGroup(vars);
vars = 75;
TestSATGroup(vars);
vars = 100;
TestSATGroup(vars);
vars = 150;
TestSATGroup(vars);
vars = 200;
TestSATGroup(vars);

Console.WriteLine("Done.");