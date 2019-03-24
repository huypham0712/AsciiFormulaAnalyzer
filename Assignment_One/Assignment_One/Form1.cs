using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AsciiFormulaAnalyzer
{
    public partial class MainForm : Form
    {
        private AsciiTree _myTree;
        private string _originalFormula;
        private SimplifiedTruthTableForm _simplifiedTruthTableForm;
        public MainForm()
        {
            InitializeComponent();

            tbInput.MaxLength = int.MaxValue;
            tbInput.ScrollBars = ScrollBars.Horizontal;
            tbInput.WordWrap = false;
            tbInput.Multiline = true;

            tbDisjunctive.WordWrap = false;
            tbDisjunctive.Multiline = true;
            tbDisjunctive.ScrollBars = ScrollBars.Horizontal;

            tbDisjunctiveSimplified.WordWrap = false;
            tbDisjunctiveSimplified.Multiline = true;
            tbDisjunctiveSimplified.ScrollBars = ScrollBars.Horizontal;

            ttCheckBoxSimplifiedTruthTable.SetToolTip(cbSimplifiedTruthTable, "Check this to see the simplified truth table!\nUncheck to skip computing the simplified truth table!");

            cbSimplifiedTruthTable.Checked = true;

            btnViewTreeGraph.Enabled = false;

            this.Text = "Ascii Formula Analyzer";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            _myTree = AsciiTree.GetAsciiTree();
        }


        private void btnProcess_Click(object sender, EventArgs e)
        {
            // Check the formula
            string userInput = Convert.ToString(tbInput.Text);
            if (string.IsNullOrWhiteSpace(userInput))
            {
                MessageBox.Show("Please provide a valid formula or at least something!!", "Error");
                ResetForm();
                return;
            }

            if (!_myTree.SetFormula(userInput))
            {
                MessageBox.Show("The formula is not in a correct format!!", "Error");
                ResetForm();
                return;
            }

            btnViewTreeGraph.Enabled = true;

            _originalFormula = userInput;

            // Show all variables in the formula
            ShowVariable();

            // Compute the truth value
            CreateTruthTable();

            // Compute the hexadecimal value from the truth table
            FindHexValue();

            if (cbSimplifiedTruthTable.Checked)
            {
                //Simplify the truth table
                ShowSimplifyTruthTableForm();
            }

            // Show the disjunctive normal form
            ShowDisjunctiveNormalForm();

            // Show the disjunctive from the simplified truth table
            ShowDisjunctiveSimplifiedForm();

            // Show the nandified form of the original formula
            ShowNandifiedForm();
        }

        private void ShowNandifiedForm()
        {
            var nandifiedForm = _myTree.FindNandifiedForm();
            string nandifiedString = nandifiedForm.ToString();
            tbNandifiedForm.Text = nandifiedString;
        }

        private void ShowSimplifyTruthTableForm()
        {
            if (_simplifiedTruthTableForm != null)
            {
                _simplifiedTruthTableForm.Close();
            }
            _simplifiedTruthTableForm = new SimplifiedTruthTableForm(_originalFormula, _myTree);
            _simplifiedTruthTableForm.SetDesktopLocation(this.Location.X + this.Height, this.Location.Y + this.Width);
            _simplifiedTruthTableForm.Show();
        }

        #region Helper methods

        private void ShowDisjunctiveSimplifiedForm()
        {
            string result = _myTree.FindDisjunctiveSimplifiedForm();
            tbDisjunctiveSimplified.Text = result;
        }

        private void ShowDisjunctiveNormalForm()
        {
            //perform the finding disjunctive normal form here
            string disjunctiveNormalForm = _myTree.FindDisjunctiveNormalForm();
            tbDisjunctive.Text = disjunctiveNormalForm;
        }

        private void FindHexValue()
        {
            List<int> truthValues = _myTree.TruthTable.ComputeTruthTable();
            string binaryString = "";

            for (int i = truthValues.Count - 1; i >= 0; i--)
            {
                binaryString += truthValues[i].ToString();
            }

            tbHexValue.Text = Convert.ToInt32(binaryString, 2).ToString("X");
        }

        private void CreateTruthTable()
        {
            if (_myTree.MainFormula == null)
            {
                MessageBox.Show("Could not provide a truth table for an empty formula!!", "Error");
                return;
            }
            try
            {
                //Compute truth value
                TruthTable myTruthTable = _myTree.TruthTable;
                List<int[]> preComputeData = TruthTableHelper.GetPreComputeValues(_myTree.Variables);
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

                dgvTruthTable.Columns[dgvTruthTable.ColumnCount - 1].Name = _originalFormula;

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
            _myTree = AsciiTree.GetAsciiTree();
            tbInput.Clear();
            tbVariables.Clear();
            tbDisjunctive.Clear();
            tbDisjunctiveSimplified.Clear();
            tbNandifiedForm.Clear();
            tbHexValue.Clear();
            dgvTruthTable.Rows.Clear();
            dgvTruthTable.Columns.Clear();
        }

        private void DrawTree()
        {
            if (_myTree.MainFormula == null)
            {
                MessageBox.Show("Could not provide a tree for an empty formula!!", "Error");
                return;
            }

            try
            {
                int index = 1;
                string graphVizFormat = "graph logic { \r\nnode [ fontname = \"Sans serif\" ] " + _myTree.MainFormula.DrawGraph(ref index) + "\r\n}";
                File.WriteAllText(@"treeObjects.dot", graphVizFormat);

                Process dot = new Process();
                dot.StartInfo.FileName = @"dot.exe";
                dot.StartInfo.Arguments = "-Tpng -otreeObjects.png treeObjects.dot";
                dot.Start();
                dot.WaitForExit();

                // Open the image by the default application
                Process.Start(@"treeObjects.png");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }

        private void ShowVariable()
        {
            if (_myTree.MainFormula == null)
            {
                MessageBox.Show("Please supply a formula!!", "Error");
                return;
            }

            try
            {
                tbVariables.Clear();
                string variablesList = "";
                for (int i = 0; i < _myTree.Variables.Count; i++)
                {
                    variablesList += _myTree.Variables[i].Proposition.ToString();
                    if (i != _myTree.Variables.Count - 1) variablesList += ", ";
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

        private void btnViewTreeGraph_Click(object sender, EventArgs e)
        {
            DrawTree();
        }
    }
}
