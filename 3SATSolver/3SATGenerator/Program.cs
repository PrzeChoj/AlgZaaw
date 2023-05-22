using _3SATSolverLib;

int choose = 0;
while (choose < '1' || choose > '3')
{
    Console.WriteLine("Rodzaj testu:");
    Console.WriteLine("\t1. losowy");
    Console.WriteLine("\t2. spelnialny");
    choose = Console.Read();
}

Console.ReadLine();
int variables = 0;
while (variables <= 0)
{
    Console.Write("Podaj liczbe zmiennych: ");
    int.TryParse(Console.ReadLine(), out variables);
}

int clauses = 0;
while (clauses <= 0)
{
    Console.Write("Podaj liczbe klauzuli: ");
    int.TryParse(Console.ReadLine(), out clauses);
}

FormulaGenerator fg = new FormulaGenerator();
Formula formula;
switch (choose)
{
    case '1':
        formula = fg.RandomFormula(variables, clauses);
        break;
    case '2':
        formula = fg.SatisfiableFormula(variables, clauses);
        break;
    default:
        throw new InvalidDataException();
}

Console.Write("Wygenerowano formule. Podaj nazwe pliku do zapisu: ");
string fileName = Console.ReadLine();
File.WriteAllText(fileName, formula.ToString());