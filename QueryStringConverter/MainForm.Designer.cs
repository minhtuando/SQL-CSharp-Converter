namespace QueryStringConverter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.txtSQLQuery = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCCode = new System.Windows.Forms.RichTextBox();
            this.btnSQLToC = new System.Windows.Forms.Button();
            this.btnCToSQL = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.radioFramework = new System.Windows.Forms.RadioButton();
            this.radioFramework2 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtSQLQuery
            // 
            this.txtSQLQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSQLQuery.Location = new System.Drawing.Point(12, 50);
            this.txtSQLQuery.Name = "txtSQLQuery";
            this.txtSQLQuery.Size = new System.Drawing.Size(703, 460);
            this.txtSQLQuery.TabIndex = 0;
            this.txtSQLQuery.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "SQL Query";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(814, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "C# Code";
            // 
            // txtCCode
            // 
            this.txtCCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCCode.Location = new System.Drawing.Point(817, 51);
            this.txtCCode.Name = "txtCCode";
            this.txtCCode.Size = new System.Drawing.Size(703, 460);
            this.txtCCode.TabIndex = 3;
            this.txtCCode.Text = "";
            // 
            // btnSQLToC
            // 
            this.btnSQLToC.Location = new System.Drawing.Point(721, 194);
            this.btnSQLToC.Name = "btnSQLToC";
            this.btnSQLToC.Size = new System.Drawing.Size(90, 64);
            this.btnSQLToC.TabIndex = 4;
            this.btnSQLToC.Text = "»";
            this.btnSQLToC.UseVisualStyleBackColor = true;
            this.btnSQLToC.Click += new System.EventHandler(this.btnSQLToC_Click);
            // 
            // btnCToSQL
            // 
            this.btnCToSQL.Location = new System.Drawing.Point(721, 275);
            this.btnCToSQL.Name = "btnCToSQL";
            this.btnCToSQL.Size = new System.Drawing.Size(90, 64);
            this.btnCToSQL.TabIndex = 5;
            this.btnCToSQL.TabStop = false;
            this.btnCToSQL.Text = "«";
            this.btnCToSQL.UseVisualStyleBackColor = true;
            this.btnCToSQL.Click += new System.EventHandler(this.btnCToSQL_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(718, 72);
            this.label3.MaximumSize = new System.Drawing.Size(100, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 119);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tip: Code after converting will be automatically copied to Clipboard";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radioFramework
            // 
            this.radioFramework.AutoSize = true;
            this.radioFramework.Checked = true;
            this.radioFramework.Location = new System.Drawing.Point(898, 13);
            this.radioFramework.Name = "radioFramework";
            this.radioFramework.Size = new System.Drawing.Size(137, 21);
            this.radioFramework.TabIndex = 7;
            this.radioFramework.TabStop = true;
            this.radioFramework.Text = "Entity Framework";
            this.radioFramework.UseVisualStyleBackColor = true;
            // 
            // radioFramework2
            // 
            this.radioFramework2.AutoSize = true;
            this.radioFramework2.Location = new System.Drawing.Point(1053, 13);
            this.radioFramework2.Name = "radioFramework2";
            this.radioFramework2.Size = new System.Drawing.Size(76, 21);
            this.radioFramework2.TabIndex = 8;
            this.radioFramework2.Text = "Dapper";
            this.radioFramework2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(679, 523);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "From Minh Tuan Do with ♥";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1532, 549);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.radioFramework2);
            this.Controls.Add(this.radioFramework);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCToSQL);
            this.Controls.Add(this.btnSQLToC);
            this.Controls.Add(this.txtCCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSQLQuery);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Query String Converter 4.0";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtSQLQuery;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtCCode;
        private System.Windows.Forms.Button btnSQLToC;
        private System.Windows.Forms.Button btnCToSQL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioFramework;
        private System.Windows.Forms.RadioButton radioFramework2;
        private System.Windows.Forms.Label label4;
    }
}

