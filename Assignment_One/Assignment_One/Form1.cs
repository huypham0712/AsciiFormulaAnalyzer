using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AsciiFormulaAnalyzer
{
    public partial class MainForm : Form
    {
        private AsciiTree myTree;
        private string originalFormula;
        public MainForm()
        {
            InitializeComponent();
            tbInput.MaxLength = Int32.MaxValue;
            Text = "Ascii Formula Analyzer";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            myTree = new AsciiTree();
        }


        private void btnProcess_Click(object sender, EventArgs e)
        {
            //Check the formula
            string userInput = Convert.ToString(tbInput.Text);
            if (string.IsNullOrWhiteSpace(userInput))
            {
                MessageBox.Show("Please provide a valid formula or at least something!!", "Error");
                ResetForm();
                return;
            }

            if (!myTree.SetFormula(userInput))
            {
                MessageBox.Show("The formula is not in a correct format!!", "Error");
                ResetForm();
                return;
            }

            originalFormula = userInput;

            //Show all variables in the formula
            ShowVariable();

            //Draw the graphical representation of the formula using GraphViz
            DrawTree();

            //Compute the truth value
            CreateTruthTable();

            //Compute the hexadecimal value from the truth table
            FindHexValue();

            SimplifyTruthTable();
        }

        private void SimplifyTruthTable()
        {
            if (myTree.MainFormula == null)
            {
                MessageBox.Show("Could not provide a truth table for an empty formula!!", "Error");
                return;
            }
            try
            {
                //Compute truth value
                TruthTable myTruthTable = myTree.TruthTable;
                var truthTableValues = TruthTableHelper.GetPreComputeValues(myTree.Variables);
                var truthTableResult = myTruthTable.ComputeTruthTable();
                var preSimplifiedValue = TruthTableHelper.GetPreSimplifyValues(truthTableValues, truthTableResult, myTree.Variables);
                var result = myTruthTable.Simplify(preSimplifiedValue);
                var falseRows = TruthTableHelper.GetFalseRows(truthTableValues, truthTableResult, myTree.Variables);

                //Setup DataGridView
                dgvSimplifiedTruthTable.ColumnCount = myTruthTable.NumberOfColumn + 1;
                dgvSimplifiedTruthTable.RowCount = falseRows.Count + result[0].Count;
                dgvSimplifiedTruthTable.Name = "Simplified Truth Table";
                dgvSimplifiedTruthTable.RowHeadersVisible = false;
                dgvSimplifiedTruthTable.AutoSize = true;

                for (int i = 0; i < myTruthTable.NumberOfColumn; i++)
                {
                    dgvSimplifiedTruthTable.Columns[i].Name = myTruthTable.Variables[i].Proposition.ToString();
                }

                dgvSimplifiedTruthTable.Columns[dgvTruthTable.ColumnCount - 1].Name = originalFormula;
                dgvSimplifiedTruthTable.Rows.Clear();

                for (int j = 0; j < falseRows.Count; j++)
                {
                    string[] rows = new string[falseRows[j].Length + 1];
                    falseRows[j].CopyTo(rows,0);
                    rows[falseRows[j].Length] = "0";
                    dgvSimplifiedTruthTable.Rows.Add(rows);                
                }

                for (int i = 0; i < result[0].Count; i++)
                {
                    string[] rows = new string[result[0][i].Length + 1];
                    result[0][i].CopyTo(rows, 0);
                    rows[result[0][i].Length] = "1";
                    dgvSimplifiedTruthTable.Rows.Add(rows);
                }

                foreach (DataGridViewColumn column in dgvSimplifiedTruthTable.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                dgvSimplifiedTruthTable.Columns[dgvTruthTable.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                throw;
            }
        }


        #region Helper methods

        private void FindHexValue()
        {
            List<int> truthValues = myTree.TruthTable.ComputeTruthTable();
            string binaryString = "";

            for (int i = truthValues.Count - 1; i >= 0; i--)
            {
                binaryString += truthValues[i].ToString();
            }

            tbHexValue.Text = Convert.ToInt32(binaryString, 2).ToString("X");
        }

        private void CreateTruthTable()
        {
            if (myTree.MainFormula == null)
            {
                MessageBox.Show("Could not provide a truth table for an empty formula!!", "Error");
                return;
            }
            try
            {
                //Compute truth value
                TruthTable myTruthTable = myTree.TruthTable;
                List<int[]> preComputeData = TruthTableHelper.GetPreComputeValues(myTree.Variables);
                List<int> result = myTruthTable.ComputeTruthTable();

                //Setup DataGridView
                dgvTruthTable.ColumnCount = myTruthTable.NumberOfColumn + 1;
                dgvTruthTable.RowCount = myTruthTable.NumberOfRow;
                dgvTruthTable.Name = "Truth Table";
                dgvTruthTable.RowHeadersVisible = false;
                dgvTruthTable.AutoSize = true;

                for (int i = 0; i < myTruthTable.NumberOfColumn; i++)
                {
                    dgvTruthTable.Columns[i].Name = myTruthTable.Variables[i].Proposition.ToString();
                }

                dgvTruthTable.Columns[dgvTruthTable.ColumnCount - 1].Name = originalFormula;

                for (int i = 0; i < myTruthTable.Variables.Count + 1; i++)
                {
                    for (int j = 0; j < myTruthTable.NumberOfRow; j++)
                    {
                        dgvTruthTable.Rows[j].Cells[i].Value = i != myTruthTable.Variables.Count ? preComputeData[i][j] : result[j];
                    }
                }

                foreach (DataGridViewColumn column in dgvTruthTable.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                dgvTruthTable.Columns[dgvTruthTable.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                throw;
            }
        }

        private void ResetForm()
        {
            myTree = new AsciiTree();

            tbInput.Clear();
            lbVariables.Items.Clear();
            pbTreeGraph.ImageLocation = @"Resources/Images/empty.png";
        }

        private void DrawTree()
        {
            if (myTree.MainFormula == null)
            {
                MessageBox.Show("Could not provide a tree for an empty formula!!", "Error");
                return;
            }

            try
            {
                int index = 1;
                string graphVizFormat = "graph logic { \r\nnode [ fontname = \"Sans serif\" ] " + myTree.MainFormula.DrawGraph(ref index) + "\r\n}";
                File.WriteAllText(@"treeObjects.dot", graphVizFormat);

                Process dot = new Process();
                dot.StartInfo.FileName = @"dot.exe";
                dot.StartInfo.Arguments = "-Tpng -otreeObjects.png treeObjects.dot";
                dot.Start();
                dot.WaitForExit();
                pbTreeGraph.SizeMode = PictureBoxSizeMode.StretchImage;
                pbTreeGraph.ImageLocation = @"treeObjects.png";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }

        private void ShowVariable()
        {
            if (myTree.MainFormula == null)
            {
                MessageBox.Show("Please supply a formula!!", "Error");
                return;
            }

            try
            {
                lbVariables.Items.Clear();
                foreach (Variable variable in myTree.MainFormula.Variables)
                {
                    lbVariables.Items.Add(variable.ToString());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }

        #endregion
    }
}
