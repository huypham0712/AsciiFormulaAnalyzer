using System;
using System.Collections.Generic;
using System.Linq;
using AsciiFormulaAnalyzer.Notations;

namespace AsciiFormulaAnalyzer
{
    public class AsciiTree
    {
        private readonly char[] toBeIgnoredChars = {' ', ',', '(', ')'};
        public List<Variable> Variables { get; set; }
        public AsciiFormula MainFormula { get; private set; }
        public TruthTable TruthTable { get; set; }
        private Stack<AsciiFormula> stack;

        public AsciiTree()
        {
            Variables = new List<Variable>();
            stack = new Stack<AsciiFormula>();
            MainFormula = null;
        }

        public bool SetFormula(string mainFormula)
        {
            if (FormulaValidationHelper.CheckValidFormula(mainFormula))
            {
                MainFormula = AnalyzeFormula(mainFormula);
                TruthTable = new TruthTable(MainFormula, MainFormula.Variables);
                return true;
            }

            return false;
        }

        public List<int> ComputeTruthData()
        {
            return TruthTable.ComputeTruthTable();
        }

        private AsciiFormula AnalyzeFormula(string mainFormula)
        {
            AsciiFormula singleFormula;
            AsciiFormula leftFormula;
            AsciiFormula rightFormula;
            Variables = new List<Variable>();

            for (int i = mainFormula.Length - 1; i >= 0; i--)
            {
                char toBeChecked = mainFormula[i];
                if (!toBeIgnoredChars.Contains(toBeChecked))
                {
                    switch (toBeChecked)
                    {
                        case '=':
                            leftFormula = stack.Pop();
                            rightFormula = stack.Pop();
                            stack.Push(new BiImplication(leftFormula, rightFormula));
                            break;
                        case '&':
                            leftFormula = stack.Pop();
                            rightFormula = stack.Pop();
                            stack.Push(new Conjunction(leftFormula, rightFormula));
                            break;
                        case '|':
                            leftFormula = stack.Pop();
                            rightFormula = stack.Pop();
                            stack.Push(new Disjunction(leftFormula, rightFormula));
                            break;
                        case '~':
                            singleFormula = stack.Pop();
                            stack.Push(new Negation(singleFormula));
                            break;
                        case '>':
                            leftFormula = stack.Pop();
                            rightFormula = stack.Pop();
                            stack.Push(new Implication(leftFormula, rightFormula));
                            break;
                        case char proposition when Char.IsUpper(proposition):
                            Variable upperChar = CheckExistVariable(proposition);
                            if (upperChar == null)
                            {
                                upperChar = new Variable(proposition);
                                Variables.Add(upperChar);
                            }

                            stack.Push(upperChar);
                            break;
                        case char proposition when Char.IsLower(proposition):
                            Variable lowerChar = CheckExistVariable(proposition);
                            if (lowerChar == null)
                            {
                                lowerChar = new Variable(proposition);
                                Variables.Add(lowerChar);
                            }

                            stack.Push(lowerChar);
                            break;
                        default:
                            return null;
                    }
                }
            }

            if (stack.Count > 0)
            {
                AsciiFormula resultFormula = stack.Pop();
                resultFormula.Variables = Variables;
                resultFormula.Variables.Sort();
                return resultFormula;
            }

            return null;
        }

        private Variable CheckExistVariable(char proposition)
        {
            foreach (var variable in Variables)
            {
                if (variable.Proposition.Equals(proposition))
                {
                    return variable;
                }
            }

            return null;
        }
    }
}
