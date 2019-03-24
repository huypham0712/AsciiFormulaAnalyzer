using System.Collections.Generic;
using System.Linq;

namespace AsciiFormulaAnalyzer
{
    public class AsciiTree
    {
        private readonly char[] _toBeIgnoredChars = { ' ', ',', '(', ')' };
        public List<Variable> Variables { get; set; }
        public AsciiFormula MainFormula { get; private set; }
        public TruthTable TruthTable { get; set; }

        private readonly Stack<AsciiFormula> _stack;
        private static AsciiTree _instance = null;

        private AsciiTree()
        {
            Variables = new List<Variable>();
            _stack = new Stack<AsciiFormula>();
            MainFormula = null;
        }

        public static AsciiTree GetAsciiTree()
        {
            if (_instance == null)
            {
                return new AsciiTree();
            }

            return _instance;
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

        public AsciiFormula FindNandifiedForm()
        {
            AsciiFormula result = MainFormula.Nandify();
            return result;
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
                    return $"~({variable.Proposition.ToString()})";

                default:
                    return string.Empty;
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

        private AsciiFormula AnalyzeFormula(string mainFormula)
        {
            AsciiFormula singleFormula;
            AsciiFormula leftFormula;
            AsciiFormula rightFormula;
            Variables = new List<Variable>();

            for (int i = mainFormula.Length - 1; i >= 0; i--)
            {
                char toBeChecked = mainFormula[i];
                if (!_toBeIgnoredChars.Contains(toBeChecked))
                {
                    switch (toBeChecked)
                    {
                        case '=':
                            leftFormula = _stack.Pop();
                            rightFormula = _stack.Pop();
                            _stack.Push(new BiImplication(leftFormula, rightFormula));
                            break;

                        case '&':
                            leftFormula = _stack.Pop();
                            rightFormula = _stack.Pop();
                            _stack.Push(new Conjunction(leftFormula, rightFormula));
                            break;

                        case '|':
                            leftFormula = _stack.Pop();
                            rightFormula = _stack.Pop();
                            _stack.Push(new Disjunction(leftFormula, rightFormula));
                            break;

                        case '~':
                            singleFormula = _stack.Pop();
                            _stack.Push(new Negation(singleFormula));
                            break;

                        case '>':
                            leftFormula = _stack.Pop();
                            rightFormula = _stack.Pop();
                            _stack.Push(new Implication(leftFormula, rightFormula));
                            break;

                        case char proposition when char.IsUpper(proposition):
                            Variable upperChar = CheckExistVariable(proposition);

                            if (upperChar == null)
                            {
                                upperChar = new Variable(proposition);
                                Variables.Add(upperChar);
                            }

                            _stack.Push(upperChar);
                            break;
                        case char proposition when char.IsLower(proposition):
                            Variable lowerChar = CheckExistVariable(proposition);

                            if (lowerChar == null)
                            {
                                lowerChar = new Variable(proposition);
                                Variables.Add(lowerChar);
                            }

                            _stack.Push(lowerChar);
                            break;

                        case '%':
                            leftFormula = _stack.Pop();
                            rightFormula = _stack.Pop();
                            _stack.Push(new Nand(leftFormula, rightFormula));
                            break;

                        default:
                            return null;
                    }
                }
            }

            if (_stack.Count > 0)
            {
                AsciiFormula resultFormula = _stack.Pop();
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
