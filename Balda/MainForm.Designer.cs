namespace Balda
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
            this.txtBxFoundWords = new System.Windows.Forms.TextBox();
            this.txtBxLett1 = new System.Windows.Forms.TextBox();
            this.txtBxLett5 = new System.Windows.Forms.TextBox();
            this.txtBxLett4 = new System.Windows.Forms.TextBox();
            this.txtBxLett3 = new System.Windows.Forms.TextBox();
            this.txtBxLett2 = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.nmrcStep = new System.Windows.Forms.NumericUpDown();
            this.lblStartWord = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nmrcStep)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBxFoundWords
            // 
            this.txtBxFoundWords.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtBxFoundWords.Location = new System.Drawing.Point(221, 12);
            this.txtBxFoundWords.Multiline = true;
            this.txtBxFoundWords.Name = "txtBxFoundWords";
            this.txtBxFoundWords.ReadOnly = true;
            this.txtBxFoundWords.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBxFoundWords.Size = new System.Drawing.Size(217, 313);
            this.txtBxFoundWords.TabIndex = 0;
            // 
            // txtBxLett1
            // 
            this.txtBxLett1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtBxLett1.Location = new System.Drawing.Point(15, 119);
            this.txtBxLett1.Name = "txtBxLett1";
            this.txtBxLett1.Size = new System.Drawing.Size(33, 29);
            this.txtBxLett1.TabIndex = 1;
            this.txtBxLett1.TextChanged += this.txtBx_TextChanged;
            // 
            // txtBxLett5
            // 
            this.txtBxLett5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtBxLett5.Location = new System.Drawing.Point(171, 119);
            this.txtBxLett5.Name = "txtBxLett5";
            this.txtBxLett5.Size = new System.Drawing.Size(33, 29);
            this.txtBxLett5.TabIndex = 5;
            this.txtBxLett5.TextChanged += this.txtBx_TextChanged;
            // 
            // txtBxLett4
            // 
            this.txtBxLett4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtBxLett4.Location = new System.Drawing.Point(132, 119);
            this.txtBxLett4.Name = "txtBxLett4";
            this.txtBxLett4.Size = new System.Drawing.Size(33, 29);
            this.txtBxLett4.TabIndex = 4;
            this.txtBxLett4.TextChanged += this.txtBx_TextChanged;
            // 
            // txtBxLett3
            // 
            this.txtBxLett3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtBxLett3.Location = new System.Drawing.Point(93, 119);
            this.txtBxLett3.Name = "txtBxLett3";
            this.txtBxLett3.Size = new System.Drawing.Size(33, 29);
            this.txtBxLett3.TabIndex = 3;
            this.txtBxLett3.TextChanged += this.txtBx_TextChanged;
            // 
            // txtBxLett2
            // 
            this.txtBxLett2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtBxLett2.Location = new System.Drawing.Point(54, 119);
            this.txtBxLett2.Name = "txtBxLett2";
            this.txtBxLett2.Size = new System.Drawing.Size(33, 29);
            this.txtBxLett2.TabIndex = 2;
            this.txtBxLett2.TextChanged += this.txtBx_TextChanged;
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(129, 171);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 6;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // nmrcStep
            // 
            this.nmrcStep.Location = new System.Drawing.Point(47, 174);
            this.nmrcStep.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nmrcStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrcStep.Name = "nmrcStep";
            this.nmrcStep.Size = new System.Drawing.Size(61, 20);
            this.nmrcStep.TabIndex = 7;
            this.nmrcStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblStartWord
            // 
            this.lblStartWord.AutoSize = true;
            this.lblStartWord.Location = new System.Drawing.Point(12, 103);
            this.lblStartWord.Name = "lblStartWord";
            this.lblStartWord.Size = new System.Drawing.Size(101, 13);
            this.lblStartWord.TabIndex = 8;
            this.lblStartWord.Text = "Начальное слово: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Ход:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 337);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblStartWord);
            this.Controls.Add(this.nmrcStep);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.txtBxLett2);
            this.Controls.Add(this.txtBxLett3);
            this.Controls.Add(this.txtBxLett4);
            this.Controls.Add(this.txtBxLett5);
            this.Controls.Add(this.txtBxLett1);
            this.Controls.Add(this.txtBxFoundWords);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.Text = "Balda";
            ((System.ComponentModel.ISupportInitialize)(this.nmrcStep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBxFoundWords;
        private System.Windows.Forms.TextBox txtBxLett1;
        private System.Windows.Forms.TextBox txtBxLett5;
        private System.Windows.Forms.TextBox txtBxLett4;
        private System.Windows.Forms.TextBox txtBxLett3;
        private System.Windows.Forms.TextBox txtBxLett2;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.NumericUpDown nmrcStep;
        private System.Windows.Forms.Label lblStartWord;
        private System.Windows.Forms.Label label1;
    }
}

