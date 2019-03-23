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

            //Simplify the truth table
            SimplifyTruthTable();

            //Show the disjunctive normal form
            ShowDisjunctiveNormalForm();

            //Show the disjunctive from the simplified truth table
            ShowDisjunctiveSimplifiedForm();
        }

        private void ShowDisjunctiveSimplifiedForm()
        {
            string result = myTree.FindDisjunctiveSimplifiedForm();
            tbDisjunctiveSimplified.Text = result;
        }


        #region Helper methods

        private void ShowDisjunctiveNormalForm()
        {
            //perform the finding disjunctive normal form here
            string disjunctiveNormalForm = myTree.FindDisjunctiveNormalForm();
            tbDisjunctive.Text = disjunctiveNormalForm;
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
                var result = myTruthTable.ComputeSimplifiedTruthTable();
                var falseRows = TruthTableHelper.GetFalseRows(truthTableValues, truthTableResult, myTree.Variables);

                //Setup DataGridView             
                dgvSimplifiedTruthTable.Name = "Simplified Truth Table";
                dgvSimplifiedTruthTable.RowHeadersVisible = false;
                dgvSimplifiedTruthTable.AutoSize = true;
                

                if (result != null)
                {
                    dgvSimplifiedTruthTable.ColumnCount = myTruthTable.NumberOfColumn + 1;
                    dgvSimplifiedTruthTable.RowCount = falseRows.Count + GetNrOfSimplifiedRows(result);

                    for (int i = 0; i < myTruthTable.NumberOfColumn; i++)
                    {
                        dgvSimplifiedTruthTable.Columns[i].Name = myTruthTable.Variables[i].Proposition.ToString();
                    }

                    dgvSimplifiedTruthTable.Columns[dgvSimplifiedTruthTable.ColumnCount - 1].Name = originalFormula;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                throw;
            }
        }

        private int GetNrOfSimplifiedRows(List<List<string[]>> simplifiedData)
        {
            int result = 0;
            foreach (var listOfData in simplifiedData)
            {
                result += listOfData.Count;
            }

            return result;
        }

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
            tbVariables.Clear();
            tbDisjunctive.Clear();
            tbDisjunctiveSimplified.Clear();
            tbHexValue.Clear();
            dgvTruthTable.Rows.Clear();
            dgvTruthTable.Columns.Clear();
            dgvSimplifiedTruthTable.Rows.Clear();
            dgvSimplifiedTruthTable.Columns.Clear();
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
                tbVariables.Clear();
                string variablesList = "";
                for (int i = 0; i < myTree.Variables.Count; i++)
                {
                    variablesList += myTree.Variables[i].Proposition.ToString();
                    if (i != myTree.Variables.Count - 1) variablesList += ", ";
                }

                tbVariables.Text = variablesList;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }

        #endregion

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                btnProcess.PerformClick();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }
    }
}
