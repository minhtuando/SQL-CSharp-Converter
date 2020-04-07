using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QueryStringConverter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();            
        }       

        private void btnSQLToC_Click(object sender, EventArgs e)
        {
            try
            {                
                string sqlQuery = txtSQLQuery.Text;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("StringBuilder sb = new StringBuilder();");

                for (int i = 0; i < txtSQLQuery.Lines.Length; i++)
                {
                    if (!string.IsNullOrEmpty(txtSQLQuery.Lines[i]) && !string.IsNullOrWhiteSpace(txtSQLQuery.Lines[i]))
                    {
                        sb.AppendFormat("sb.Append(\"{0} \");", txtSQLQuery.Lines[i]);
                        sb.AppendLine();
                    }
                }
                sb.AppendLine();
                sb.Append("string query = sb.ToString();");
                sb.AppendLine();
                sb.AppendLine();

                var pattern = @"@[a-zA-Z]+";
                Match match = Regex.Match(sqlQuery, pattern, RegexOptions.IgnoreCase);

                List<string> parameters = new List<string>();

                if (match.Success)
                {
                    while (match.Success)
                    {
                        var paramName = match.Groups[0].ToString().Substring(1);
                        if (!parameters.Contains(paramName))
                        {
                            if (GetSQLFrameworkChecked().Equals("Entity Framework"))
                            {
                                sb.AppendFormat("var {0}Param = new SqlParameter(\"@{0}\", {0});", paramName);
                                sb.AppendLine();
                            }

                            parameters.Add(paramName);
                        }
                        match = match.NextMatch();
                    }
                }

                sb.AppendLine();

                if (GetSQLFrameworkChecked().Equals("Dapper"))
                {
                    string queryAndParam = "";

                    if (parameters.Any())
                    {
                        queryAndParam = string.Join(",", parameters);
                    }
                    sb.Append("var result = context.Query<>(query, new { " + queryAndParam + " }).ToList();");
                }
                else
                {
                    string queryAndParam = "query";

                    if (parameters.Any())
                    {
                        parameters.ForEach(x =>
                        {
                            queryAndParam += ", " + x + "Param";
                        });
                    }

                    sb.AppendFormat("var result = context.Database.SqlQuery<>({0}).ToList();", queryAndParam);
                }

                sb.AppendLine();

                txtCCode.Text = sb.ToString();
                //Copy vào clipboard
                Clipboard.SetText(sb.ToString());
            }
            catch
            {
                ShowErrorMessage();
            }
        }

        private void ShowErrorMessage()
        {
            MessageBox.Show("There was an error with the program. Please check your input and make sure it is in a correct format!", "Opps! The program crashed!");
        }

        private string GetSQLFrameworkChecked()
        {
            //Xác định framework đang dùng
            string framework = null;
            foreach (Control control in this.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton radio = control as RadioButton;
                    if (radio.Checked)
                    {
                        framework = radio.Text;
                    }
                }
            }
            return framework;
        }

        private void btnCToSQL_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < txtCCode.Lines.Length; i++)
                {
                    if (Regex.Match(txtCCode.Lines[i].Trim(), @"[a-zA-Z0-9]+.Append").Success)
                    {
                        var processedCode = txtCCode.Lines[i]
                            .Trim()
                            .Replace("sb.Append(\"", "")
                            .Replace("sb.AppendFormat(\"", "")
                            .Replace("sb.Append($\"", "")
                            .Replace("sb.AppendFormat($\"", "")
                            .Replace("\");", "");

                        processedCode = Regex.Replace(processedCode, @"[a-zA-Z0-9]+.Append\(\""", "");
                        processedCode = Regex.Replace(processedCode, @"[a-zA-Z0-9]+.AppendFormat\(\""", "");
                        processedCode = Regex.Replace(processedCode, @"[a-zA-Z0-9]+.Append\(\$\""", "");
                        processedCode = Regex.Replace(processedCode, @"[a-zA-Z0-9]+.AppendFormat\(\$\""", "");

                        sb.AppendLine(processedCode);
                    }
                }

                string result = Regex.Replace(sb.ToString(), "\",.+", "");

                txtSQLQuery.Text = result;
                //Copy vào clipboard
                Clipboard.SetText(result);
            }
            catch
            {
                ShowErrorMessage();
            }
        }
    }
}
