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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvTruthTable = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.tbHexValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvSimplifiedTruthTable = new System.Windows.Forms.DataGridView();
            this.tbDisjunctive = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbDisjunctiveSimplified = new System.Windows.Forms.TextBox();
            this.tbVariables = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbTreeGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTruthTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSimplifiedTruthTable)).BeginInit();
            this.SuspendLayout();
            // 
            // tbInput
            // 
            this.tbInput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbInput.Location = new System.Drawing.Point(20, 34);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(791, 29);
            this.tbInput.TabIndex = 0;
            this.tbInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
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
            this.btnProcess.Location = new System.Drawing.Point(817, 34);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(117, 30);
            this.btnProcess.TabIndex = 2;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // pbTreeGraph
            // 
            this.pbTreeGraph.Location = new System.Drawing.Point(618, 319);
            this.pbTreeGraph.Name = "pbTreeGraph";
            this.pbTreeGraph.Size = new System.Drawing.Size(439, 402);
            this.pbTreeGraph.TabIndex = 3;
            this.pbTreeGraph.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 234);
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
            this.dgvTruthTable.Location = new System.Drawing.Point(20, 319);
            this.dgvTruthTable.MultiSelect = false;
            this.dgvTruthTable.Name = "dgvTruthTable";
            this.dgvTruthTable.ReadOnly = true;
            this.dgvTruthTable.RowHeadersVisible = false;
            this.dgvTruthTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvTruthTable.Size = new System.Drawing.Size(293, 176);
            this.dgvTruthTable.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 295);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 21);
            this.label2.TabIndex = 7;
            this.label2.Text = "Truth Table";
            // 
            // tbHexValue
            // 
            this.tbHexValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHexValue.ForeColor = System.Drawing.Color.SteelBlue;
            this.tbHexValue.Location = new System.Drawing.Point(20, 202);
            this.tbHexValue.Multiline = true;
            this.tbHexValue.Name = "tbHexValue";
            this.tbHexValue.ReadOnly = true;
            this.tbHexValue.Size = new System.Drawing.Size(1037, 29);
            this.tbHexValue.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 21);
            this.label3.TabIndex = 9;
            this.label3.Text = "Hexadecimal value";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(319, 295);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(170, 21);
            this.label4.TabIndex = 11;
            this.label4.Text = "Simplified Truth Table";
            // 
            // dgvSimplifiedTruthTable
            // 
            this.dgvSimplifiedTruthTable.AllowUserToAddRows = false;
            this.dgvSimplifiedTruthTable.AllowUserToDeleteRows = false;
            this.dgvSimplifiedTruthTable.AllowUserToResizeColumns = false;
            this.dgvSimplifiedTruthTable.AllowUserToResizeRows = false;
            this.dgvSimplifiedTruthTable.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvSimplifiedTruthTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSimplifiedTruthTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvSimplifiedTruthTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSimplifiedTruthTable.Location = new System.Drawing.Point(319, 319);
            this.dgvSimplifiedTruthTable.MultiSelect = false;
            this.dgvSimplifiedTruthTable.Name = "dgvSimplifiedTruthTable";
            this.dgvSimplifiedTruthTable.ReadOnly = true;
            this.dgvSimplifiedTruthTable.RowHeadersVisible = false;
            this.dgvSimplifiedTruthTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvSimplifiedTruthTable.Size = new System.Drawing.Size(293, 158);
            this.dgvSimplifiedTruthTable.TabIndex = 10;
            // 
            // tbDisjunctive
            // 
            this.tbDisjunctive.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDisjunctive.Location = new System.Drawing.Point(20, 90);
            this.tbDisjunctive.Multiline = true;
            this.tbDisjunctive.Name = "tbDisjunctive";
            this.tbDisjunctive.ReadOnly = true;
            this.tbDisjunctive.Size = new System.Drawing.Size(1037, 29);
            this.tbDisjunctive.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 21);
            this.label5.TabIndex = 13;
            this.label5.Text = "Disjunctive normal form:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(208, 21);
            this.label6.TabIndex = 15;
            this.label6.Text = "Disjunctive simplified form:";
            // 
            // tbDisjunctiveSimplified
            // 
            this.tbDisjunctiveSimplified.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDisjunctiveSimplified.Location = new System.Drawing.Point(20, 146);
            this.tbDisjunctiveSimplified.Multiline = true;
            this.tbDisjunctiveSimplified.Name = "tbDisjunctiveSimplified";
            this.tbDisjunctiveSimplified.ReadOnly = true;
            this.tbDisjunctiveSimplified.Size = new System.Drawing.Size(1037, 29);
            this.tbDisjunctiveSimplified.TabIndex = 14;
            // 
            // tbVariables
            // 
            this.tbVariables.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVariables.ForeColor = System.Drawing.Color.SteelBlue;
            this.tbVariables.Location = new System.Drawing.Point(20, 258);
            this.tbVariables.Multiline = true;
            this.tbVariables.Name = "tbVariables";
            this.tbVariables.ReadOnly = true;
            this.tbVariables.Size = new System.Drawing.Size(1037, 31);
            this.tbVariables.TabIndex = 16;
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(940, 34);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(117, 30);
            this.btnReset.TabIndex = 17;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(614, 295);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 21);
            this.label7.TabIndex = 18;
            this.label7.Text = "Visualization";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 725);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.tbVariables);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbDisjunctiveSimplified);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbDisjunctive);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvSimplifiedTruthTable);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbHexValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvTruthTable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbTreeGraph);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.lbInput);
            this.Controls.Add(this.tbInput);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbTreeGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTruthTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSimplifiedTruthTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Label lbInput;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.PictureBox pbTreeGraph;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvTruthTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbHexValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvSimplifiedTruthTable;
        private System.Windows.Forms.TextBox tbDisjunctive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbDisjunctiveSimplified;
        private System.Windows.Forms.TextBox tbVariables;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label7;
    }
}

