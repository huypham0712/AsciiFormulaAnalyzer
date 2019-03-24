using System;
using System.Windows.Forms;

namespace AsciiFormulaAnalyzer
{
    public partial class SimplifiedTruthTableForm : Form
    {
        private readonly string _originalFormula;
        private readonly AsciiTree _myTree;
        public SimplifiedTruthTableForm(string originalFormula, AsciiTree asciiTree)
        {
            InitializeComponent();
            _originalFormula = originalFormula;
            _myTree = asciiTree;
            ShowSimplifyTruthTable();
        }

        private void ShowSimplifyTruthTable()
        {
            try
            {             
                //Compute truth value
                TruthTable myTruthTable = _myTree.TruthTable;
                var truthTableValues = TruthTableHelper.GetPreComputeValues(_myTree.Variables);
                var truthTableResult = myTruthTable.ComputeTruthTable();
                var result = myTruthTable.ComputeSimplifiedTruthTable();
                var falseRows = TruthTableHelper.GetFalseRows(truthTableValues, truthTableResult, _myTree.Variables);

                //Setup DataGridView             
                dgvSimplifiedTruthTable.Name = "Simplified Truth Table";
                dgvSimplifiedTruthTable.RowHeadersVisible = false;
                dgvSimplifiedTruthTable.AutoSize = true;
                

                if (result != null)
                {
                    dgvSimplifiedTruthTable.ColumnCount = myTruthTable.NumberOfColumn + 1;
                    dgvSimplifiedTruthTable.RowCount = falseRows.Count + TruthTableHelper.GetNrOfSimplifiedRows(result);

                    for (int i = 0; i < myTruthTable.NumberOfColumn; i++)
                    {
                        dgvSimplifiedTruthTable.Columns[i].Name = myTruthTable.Variables[i].Proposition.ToString();
                    }

                    dgvSimplifiedTruthTable.Columns[dgvSimplifiedTruthTable.ColumnCount - 1].Name = _originalFormula;
                    dgvSimplifiedTruthTable.Rows.Clear();

                 
                    for (int j = 0; j < falseRows.Count; j++)
                    {
                        string[] rows = new string[falseRows[j].Length + 1];
                        falseRows[j].CopyTo(rows, 0);
                        rows[falseRows[j].Length] = "0";
                        dgvSimplifiedTruthTable.Rows.Add(rows);
                    }

                    foreach (var listOfSimplifiedRows in result)
                    {
                        for (int i = 0; i < listOfSimplifiedRows.Count; i++)
                        {
                            string[] rows = new string[listOfSimplifiedRows[i].Length + 1];
                            listOfSimplifiedRows[i].CopyTo(rows, 0);
                            rows[listOfSimplifiedRows[i].Length] = "1";
                            dgvSimplifiedTruthTable.Rows.Add(rows);
                        }
                    }
                    
                }
                else
                {
                    dgvSimplifiedTruthTable.ColumnCount = 1;
                    dgvSimplifiedTruthTable.RowCount = 1;
                    dgvSimplifiedTruthTable.Rows.Clear();
                    string[] errorMessage = { "Could not simplify the truth table of this formula!!" };
                    dgvSimplifiedTruthTable.Rows.Add(errorMessage);
                }
             
                foreach (DataGridViewColumn column in dgvSimplifiedTruthTable.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                dgvSimplifiedTruthTable.Columns[dgvSimplifiedTruthTable.ColumnCount - 1].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill; 

                this.Width = dgvSimplifiedTruthTable.Width + 40;
                this.Height = dgvSimplifiedTruthTable.Height + 60;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                throw;
            }
        }
    }
}
