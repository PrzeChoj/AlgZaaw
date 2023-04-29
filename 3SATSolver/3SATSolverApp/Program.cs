﻿using _3SATSolverLib;

Formula formula;
if (args.Length > 0)
    formula = FormulaReader.ReadFromFile(args[0]);
else
    formula = FormulaReader.ReadFromConsole();

formula.Solve();

SolutionWriter.WriteToConsole(formula);
