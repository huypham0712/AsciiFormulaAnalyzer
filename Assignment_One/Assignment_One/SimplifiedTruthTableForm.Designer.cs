namespace AsciiFormulaAnalyzer
{
    partial class SimplifiedTruthTableForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvSimplifiedTruthTable = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSimplifiedTruthTable)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSimplifiedTruthTable
            // 
            this.dgvSimplifiedTruthTable.AllowUserToAddRows = false;
            this.dgvSimplifiedTruthTable.AllowUserToDeleteRows = false;
            this.dgvSimplifiedTruthTable.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvSimplifiedTruthTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSimplifiedTruthTable.Location = new System.Drawing.Point(12, 12);
            this.dgvSimplifiedTruthTable.Name = "dgvSimplifiedTruthTable";
            this.dgvSimplifiedTruthTable.ReadOnly = true;
            this.dgvSimplifiedTruthTable.Size = new System.Drawing.Size(360, 203);
            this.dgvSimplifiedTruthTable.TabIndex = 0;
            // 
            // SimplifiedTruthTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 227);
            this.Controls.Add(this.dgvSimplifiedTruthTable);
            this.Name = "SimplifiedTruthTableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simplified Truth Table";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSimplifiedTruthTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSimplifiedTruthTable;
    }
}