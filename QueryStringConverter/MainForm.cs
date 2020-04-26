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
using System.Xml;

namespace QueryStringConverter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            txtCCode.EnableContextMenu();
            txtMappingModel.EnableContextMenu();
            txtSQLQuery.EnableContextMenu();
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

                var pattern = @"@[a-zA-Z0-9]+";
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
                    sb.Append("var result = db.Query<>(query, new { " + queryAndParam + " }).ToList();");
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

                    sb.AppendFormat("var result = db.Database.SqlQuery<>({0}).ToList();", queryAndParam);
                }

                sb.AppendLine();

                txtCCode.Text = sb.ToString();
                //Copy vào clipboard
                Clipboard.SetText(sb.ToString());

                //Phân tích mapping models
                var selectClause = txtSQLQuery.Text;

                selectClause = selectClause.Replace(" from ", " FROM ")
                                            .Replace("[", "")
                                            .Replace("]", "")
                                            .Replace("\nfrom ", "\nFROM ")
                                            .Replace("\n from ", "\nFROM ")
                                            .Replace("\n FROM ", "\nFROM ");

                if (selectClause.Contains("\nFROM "))
                {
                    if (CountStringOccurrences(selectClause, "\nFROM ") == 1)
                    {
                        selectClause = selectClause.Substring(0, selectClause.IndexOf("\nFROM "));
                    }
                    else
                    {
                        selectClause = selectClause.Substring(0, selectClause.LastIndexOf("\nFROM "));
                    }
                }

                var fields = selectClause.Split(',');

                sb.Clear();

                foreach (var col in fields)
                {
                    string fieldName = col;

                    string type = "int";


                    if (fieldName.ToLower().Contains("."))
                    {
                        fieldName = fieldName.Substring(fieldName.IndexOf(".") + 1);
                    }

                    if (fieldName.ToLower().Contains(" as "))
                    {
                        string[] aliasMapKeys = { " as ", " As ", " aS ", " AS " };

                        foreach (var key in aliasMapKeys)
                        {
                            if (fieldName.Contains(key))
                            {
                                fieldName = fieldName.Substring(fieldName.IndexOf(key) + 4);
                            }
                        }
                    }

                    fieldName = fieldName.Replace("select", "");
                    fieldName = fieldName.Replace("SELECT", "");
                    fieldName = fieldName.Trim();

                    if (fieldName.Contains(" "))
                    {
                        fieldName = fieldName.Substring(0, fieldName.IndexOf(" "));
                    }

                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        type = GetTypeOfField(fieldName);

                        sb.AppendLine($"public { type } { fieldName } {{ get; set; }}");
                    }
                }

                txtMappingModel.Text = sb.ToString();
                //End phân tích

            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void ShowErrorMessage(string msg = "")
        {
            MessageBox.Show("There was an error with the program. " + msg, "Opps! The program crashed!");
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

        private string GetTypeOfField(string fieldName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("ConfigDataType.xml");

            var nodes = doc.DocumentElement.SelectNodes("/config/lowerContaints/mapData");

            for (int i = 0; i < nodes.Count; i++)
            {
                if (string.IsNullOrEmpty(nodes[i].InnerText.Trim()))
                {
                    continue;
                }
                string[] mappingKeys = nodes[i].InnerText.Split(',');

                foreach (var key in mappingKeys)
                {
                    if (fieldName.ToLower().Contains(key.Trim()))
                    {
                        return nodes[i].Attributes["type"]?.InnerText;
                    }
                }
            }

            nodes = doc.DocumentElement.SelectNodes("/config/startsWith/mapData");

            for (int i = 0; i < nodes.Count; i++)
            {
                string[] mappingKeys = nodes[i].InnerText.Split(',');

                foreach (var key in mappingKeys)
                {
                    if (fieldName.StartsWith(key))
                    {
                        return nodes[i].Attributes["type"]?.InnerText;
                    }
                }
            }

            nodes = doc.DocumentElement.SelectNodes("/config/endsWith/mapData");

            for (int i = 0; i < nodes.Count; i++)
            {
                string[] mappingKeys = nodes[i].InnerText.Split(',');

                foreach (var key in mappingKeys)
                {
                    if (fieldName.EndsWith(key))
                    {
                        return nodes[i].Attributes["type"]?.InnerText;
                    }
                }
            }

            nodes = doc.DocumentElement.SelectNodes("/config/equals/mapData");

            for (int i = 0; i < nodes.Count; i++)
            {
                string[] mappingKeys = nodes[i].InnerText.Split(',');

                foreach (var key in mappingKeys)
                {
                    if (fieldName.Equals(key))
                    {
                        return nodes[i].Attributes["type"]?.InnerText;
                    }
                }
            }

            var license = doc.DocumentElement.SelectSingleNode("/config/license");

            if (license.Attributes["author"].Value.ToString().Trim() != "Minh Tuan Do")
            {
                return "Can not run due to license violation";
            }

            //default:
            return "int";
        }

        private int CountStringOccurrences(string text, string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }

        private void btnCoppyMapping_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txtMappingModel.Text);
            }
            catch { }
        }
    }
}
