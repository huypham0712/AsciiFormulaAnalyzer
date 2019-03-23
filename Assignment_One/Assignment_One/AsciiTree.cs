using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using AsciiFormulaAnalyzer.Notations;

namespace AsciiFormulaAnalyzer
{
    public class AsciiTree
    {
        private readonly char[] toBeIgnoredChars = { ' ', ',', '(', ')' };
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

        public string FindDisjunctiveNormalForm()
        {
            var tableValues = TruthTableHelper.GetPreComputeValues(Variables);
            var truthResult = ComputeTruthData();
            string result = "";

            List<string> outerPair = new List<string>();
            for (int i = 0; i < TruthTable.NumberOfRow; i++)
            {
                string outerPairTmp = "";
                if (truthResult[i] != 0)
                {
                    string rowData = "";
                    List<string> innerPair = new List<string>();
                    for (int j = 0; j < TruthTable.NumberOfColumn; j++)
                    {
                        string variable = GetDisjunctiveFormat(tableValues[j][i], Variables[j]);
                        string tmp = "";
                        if (innerPair.Count < 2)
                        {
                            innerPair.Add(variable);
                        }

                        if (innerPair.Count == 2)
                        { 
                            tmp = $"&({innerPair[0]},{innerPair[1]})";
                            innerPair.Clear();
                            innerPair.Add(tmp);
                        }

                        if (innerPair.Count == 1 && j == TruthTable.NumberOfColumn - 1)
                        {
                            tmp = $"{innerPair[0]}";
                            innerPair.Clear();
                        }

                        rowData = tmp;
                    }

                    if (outerPair.Count < 2) outerPair.Add(rowData);

                    if (outerPair.Count == 2)
                    {
                        outerPairTmp = $"|({outerPair[0]},{outerPair[1]})";
                        outerPair.Clear();
                        outerPair.Add(outerPairTmp);
                    }

                    if (outerPair.Count == 1 && i == TruthTable.NumberOfRow - 1)
                    {
                        outerPairTmp = $"{outerPair[0]}";
                        outerPair.Clear();
                    }

                    rowData = outerPairTmp;

                    if (i != TruthTable.NumberOfRow - 1)
                    {
                        result += rowData;
                    }
                    else
                    {
                        result = rowData;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Return the value in Logic notation format of the variable from the truth table
        /// </summary>
        /// <param name="truthValue"> The truth value which can be either 1 or 0</param>
        /// <param name="variable"> The variable coordinates with the truth value</param>
        /// <returns> The value in Logic notation format</returns>
        private string GetDisjunctiveFormat(int truthValue, Variable variable)
        {
            switch (truthValue)
            {
                case 1:
                    return variable.Proposition.ToString();

                case 0:
                    //return "\u00ac" + variable.Proposition.ToString();
                    return $"~({variable.Proposition.ToString()})";

                default:
                    return String.Empty;
            }
        }

        /// <summary>
        /// Return the value in Logic notation format of the variable from the simplified truth table.
        /// </summary>
        /// <param name="simplifiedValue"> The simplified value which can be either 1, 0 or *</param>
        /// <param name="variable"> The variable coordinates with the simplified value</param>
        /// <returns> The value in Logic notation format</returns>
        private string GetDisjunctiveFormat(string simplifiedValue, Variable variable)
        {
            if (int.TryParse(simplifiedValue, out var value))
            {
                return GetDisjunctiveFormat(value, variable);
            }

            string simplifiedDisjunctiveFormat =
                $"|({variable.Proposition.ToString()}, ~({variable.Proposition.ToString()}))";

            return simplifiedDisjunctiveFormat;
        }

        public string FindDisjunctiveSimplifiedForm()
        {
            var simplifiedRows = TruthTable.ComputeSimplifiedTruthTable();

            if (simplifiedRows == null)
            {
                return FindDisjunctiveNormalForm();
            }

            List<string[]> data = new List<string[]>();

            foreach (List<string[]> simplifiedRow in simplifiedRows)
            {
                data.AddRange(simplifiedRow);
            }

            string result = "";
            List<string> outerPair = new List<string>();
            for (int i = 0; i < data.Count; i++)
            {
                string outerPairTmp = "";
                string rowData = "";
                List<string> innerPair = new List<string>();
                for (int j = 0; j < data[i].Length; j++)
                {
                    string variable = GetDisjunctiveFormat(data[i][j], Variables[j]);
                    string tmp = "";
                    if (innerPair.Count < 2)
                    {
                        innerPair.Add(variable);
                    }
                    
                    if (innerPair.Count == 2)
                    {
                        tmp = $"&({innerPair[0]},{innerPair[1]})";
                        innerPair.Clear();
                        innerPair.Add(tmp);
                    }
                    
                    if (innerPair.Count == 1 && j == TruthTable.NumberOfColumn - 1)
                    {
                        tmp = $"{innerPair[0]}";
                        innerPair.Clear();
                    }
                    
                    rowData = tmp;
                }

                if (outerPair.Count < 2) outerPair.Add(rowData);
                
                if (outerPair.Count == 2)
                {
                    outerPairTmp = $"|({outerPair[0]},{outerPair[1]})";
                    outerPair.Clear();
                    outerPair.Add(outerPairTmp);
                }
                
                if (outerPair.Count == 1 && i == data.Count - 1)
                {
                    outerPairTmp = $"{outerPair[0]}";
                    outerPair.Clear();
                }
                
                rowData = outerPairTmp;
                
                if (i != TruthTable.NumberOfRow - 1)
                {
                    result = rowData;
                }
            }

            return result;
        }
    }
}
