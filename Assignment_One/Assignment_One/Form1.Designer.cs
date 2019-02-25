namespace AsciiFormulaAnalyzer
{
    partial class MainForm
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
            this.tbInput = new System.Windows.Forms.TextBox();
            this.lbInput = new System.Windows.Forms.Label();
            this.btnProcess = new System.Windows.Forms.Button();
            this.pbTreeGraph = new System.Windows.Forms.PictureBox();
            this.lbVariables = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvTruthTable = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.tbHexValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbTreeGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTruthTable)).BeginInit();
            this.SuspendLayout();
            // 
            // tbInput
            // 
            this.tbInput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbInput.Location = new System.Drawing.Point(12, 33);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(208, 29);
            this.tbInput.TabIndex = 0;
            // 
            // lbInput
            // 
            this.lbInput.AutoSize = true;
            this.lbInput.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInput.Location = new System.Drawing.Point(12, 9);
            this.lbInput.Name = "lbInput";
            this.lbInput.Size = new System.Drawing.Size(143, 21);
            this.lbInput.TabIndex = 1;
            this.lbInput.Text = "The input formula:";
            // 
            // btnProcess
            // 
            this.btnProcess.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcess.Location = new System.Drawing.Point(226, 33);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(79, 29);
            this.btnProcess.TabIndex = 2;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // pbTreeGraph
            // 
            this.pbTreeGraph.Location = new System.Drawing.Point(349, 12);
            this.pbTreeGraph.Name = "pbTreeGraph";
            this.pbTreeGraph.Size = new System.Drawing.Size(439, 426);
            this.pbTreeGraph.TabIndex = 3;
            this.pbTreeGraph.TabStop = false;
            // 
            // lbVariables
            // 
            this.lbVariables.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVariables.FormattingEnabled = true;
            this.lbVariables.ItemHeight = 21;
            this.lbVariables.Location = new System.Drawing.Point(12, 89);
            this.lbVariables.Name = "lbVariables";
            this.lbVariables.Size = new System.Drawing.Size(89, 88);
            this.lbVariables.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "Variables:";
            // 
            // dgvTruthTable
            // 
            this.dgvTruthTable.AllowUserToAddRows = false;
            this.dgvTruthTable.AllowUserToDeleteRows = false;
            this.dgvTruthTable.AllowUserToResizeColumns = false;
            this.dgvTruthTable.AllowUserToResizeRows = false;
            this.dgvTruthTable.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvTruthTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvTruthTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvTruthTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTruthTable.Location = new System.Drawing.Point(12, 207);
            this.dgvTruthTable.MultiSelect = false;
            this.dgvTruthTable.Name = "dgvTruthTable";
            this.dgvTruthTable.ReadOnly = true;
            this.dgvTruthTable.RowHeadersVisible = false;
            this.dgvTruthTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvTruthTable.Size = new System.Drawing.Size(293, 158);
            this.dgvTruthTable.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 21);
            this.label2.TabIndex = 7;
            this.label2.Text = "Truth Table";
            // 
            // tbHexValue
            // 
            this.tbHexValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHexValue.ForeColor = System.Drawing.Color.SteelBlue;
            this.tbHexValue.Location = new System.Drawing.Point(256, 62);
            this.tbHexValue.Name = "tbHexValue";
            this.tbHexValue.ReadOnly = true;
            this.tbHexValue.Size = new System.Drawing.Size(48, 29);
            this.tbHexValue.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(103, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 21);
            this.label3.TabIndex = 9;
            this.label3.Text = "Hexadecimal value";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbHexValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvTruthTable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbVariables);
            this.Controls.Add(this.pbTreeGraph);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.lbInput);
            this.Controls.Add(this.tbInput);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbTreeGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTruthTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Label lbInput;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.PictureBox pbTreeGraph;
        private System.Windows.Forms.ListBox lbVariables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvTruthTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbHexValue;
        private System.Windows.Forms.Label label3;
    }
}

