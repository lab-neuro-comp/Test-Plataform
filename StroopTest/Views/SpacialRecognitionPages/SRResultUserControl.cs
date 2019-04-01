﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestPlatform.Models.Tests.SpacialRecognition;
using System.Resources;
using System.Globalization;
using System.IO;
using TestPlatform.Models;

namespace TestPlatform.Views.SpacialRecognitionPages
{
    public partial class SRResultUserControl : UserControl
    {
        private string path = SpacialRecognitionProgram.GetResultsPath();
        private ResourceManager LocRM = new ResourceManager("TestPlatform.Resources.Localizations.LocalizedResources", typeof(FormMain).Assembly);
        private CultureInfo currentCulture = CultureInfo.CurrentUICulture;
        private Point mousePosition = new Point();
        public SRResultUserControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            string[] filePaths = null;

            // getting names of resulting headers and separating them
            string localizedHeaders = LocRM.GetString("spacialRecognitionTestHeader", currentCulture).ToString();
            string[] separators = { @"\t" };
            string[] headers = localizedHeaders.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView1.AutoResizeColumns();
            foreach (string columnName in headers)
            {
                dataGridView1.Columns.Add(columnName, columnName); // Add header to table
                this.dataGridView1.Columns[columnName].Frozen = false;
            }

            // filling result combobox with result in pattern participant_programname in the directory
            if (Directory.Exists(path))
            {
                filePaths = Directory.GetFiles(path, "*.txt", SearchOption.AllDirectories);
                for (int i = 0; i < filePaths.Length; i++)
                {
                    fileNameBox.Items.Add(Path.GetFileNameWithoutExtension(filePaths[i]));
                }
            }
            else
            {
                MessageBox.Show("{0}" + LocRM.GetString("invalidPath", currentCulture), path);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }

        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosition = e.Location;
            mousePosition.Y += 50;
            mousePosition.X += 10;
        }

        private void fileNameBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            string[] line;
            try
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                line = Program.readDataFile(path + "/" + fileNameBox.SelectedItem.ToString() + ".txt");
                Console.WriteLine(line[0]);
                if (line.Count() > 0)
                {
                    for (int i = 0; i < line.Count(); i++)
                    {
                        string[] cellArray = line[i].Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        if (cellArray.Length == dataGridView1.Columns.Count)
                        {
                            dataGridView1.Rows.Add(cellArray);
                            for (int j = 0; j < cellArray.Length; j++)
                            {
                                if (Validations.isHexPattern(cellArray[j]))
                                {
                                    dataGridView1.Rows[i].Cells[j].Style.ForeColor = ColorTranslator.FromHtml(cellArray[j]);
                                }
                            }
                        }
                    }
                }

                dataGridView1.AutoSize = false;
                dataGridView1.ScrollBars = ScrollBars.Both;
                dataGridView1.AutoResizeColumns();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void csvExportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string[] lines;

            saveFileDialog1.Filter = "Excel CSV (.csv)|*.csv"; // salva em .csvs
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = fileNameBox.Text;

            try
            {
                // checks if there are any results selected
                if (!(fileNameBox.SelectedIndex == -1))
                {
                    lines = MatchingProgram.readDataFile(path + "/" + fileNameBox.SelectedItem.ToString() + ".txt");
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK) // abre caixa para salvar
                    {
                        using (TextWriter tw = new StreamWriter(saveFileDialog1.FileName))
                        {
                            tw.WriteLine(LocRM.GetString("reactionTestHeader", currentCulture));
                            for (int i = 0; i < lines.Length; i++)
                            {
                                tw.WriteLine(lines[i]); // escreve linhas no novo arquivo
                            }
                            tw.Close();
                            MessageBox.Show("Arquivo exportado com sucesso!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show(LocRM.GetString("selectDataFile", currentCulture));
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            FormInstructions infoBox = new FormInstructions(LocRM.GetString("SRResultsInstructions", currentCulture));
            try { infoBox.Show(); }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
