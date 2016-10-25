﻿/*
 * Copyright (c) 2016 All Rights Reserved
 * Hugo Honda
 */

using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace StroopTest
{
    public partial class FormLstConfig : Form
    {
        private string hexPattern = "^#(([0-9a-fA-F]{2}){3}|([0-9a-fA-F]){3})$";
        private string path;
        private string appendType = "";

        public FormLstConfig(string dataFolderPath, string lstName)
        {
            InitializeComponent();
            path = dataFolderPath + "/lst/";
            
            wordsListLabel.Text = "_words.lst";
            colorsListLabel.Text = "_color.lst";

            checkWords.Checked = true;
            checkColors.Checked = true;

            if (lstName.ToLower() != "false")
            {
                editList(lstName);
            }
        }
        
        private void listNameBox_TextChanged(object sender, EventArgs e)
        {
            wordsListLabel.Text = listNameTextBox.Text + "_words.lst";
            colorsListLabel.Text = listNameTextBox.Text + "_color.lst";
        }

        private void editList(string fileName)
        {
            StroopProgram program = new StroopProgram();
            string wordsFilePath = "", colorsFilePath = "";
            string[] list = null, wordsArray = null, colorsArray = null;

            try
            {
                //MessageBox.Show(fileName);
                /*
                var typeOfList = fileName.Substring(fileName.Length - 6, fileName.Length);
                switch (typeOfList.ToLower())
                {
                    case "_words":
                        fileName = fileName.Remove(fileName.Length - 6);
                        break;
                    case "_color":
                        fileName = fileName.Remove(fileName.Length - 6);
                        break;
                    default:
                        break;
                }
                */
                fileName = fileName.Remove(fileName.Length - 6);

                wordsFilePath = path + fileName + "_words.lst";
                colorsFilePath = path + fileName + "_colors.lst";

                listNameTextBox.Text = fileName;

                checkWords.Checked = false;
                checkColors.Checked = false;

                if (File.Exists(wordsFilePath))
                {
                    wordsArray = StroopProgram.readListFile(wordsFilePath);
                    checkWords.Checked = true;
                    foreach (string item in wordsArray) { wordsColoredList.Items.Add(item); }
                }
                if (File.Exists(colorsFilePath))
                {
                    colorsArray = StroopProgram.readListFile(colorsFilePath);
                    checkColors.Checked = true;
                    for (int i = 0; i < colorsArray.Length; i++)
                    {
                        if (Regex.IsMatch(colorsArray[i], hexPattern))
                        {
                            hexColorsList.Items.Add(colorsArray[i]);
                            hexColorsList.Items[i].ForeColor = ColorTranslator.FromHtml(colorsArray[i]);
                            if (File.Exists(wordsFilePath)) { wordsColoredList.Items[i].ForeColor = ColorTranslator.FromHtml(colorsArray[i]); }
                        }
                    }
                }

                /*
                wordsListLabel.Text = testFileName(lst) + "_Words";
                colorsListLabel.Text = testFileName(lst) + "_Color";
                listNameTextBox.Text = testFileName(lst);
                
                wordsArray = program.readListFile(path + appendType + ".lst");
                list = program.readListFile(path + appendType + ".lst");

                if (editLstName != "error")
                {
                    list = program.readListFile(path + editLstName + ".lst");
                    if (Regex.IsMatch(list[0], hexPattern))
                    {
                        checkColors.Checked = true;
                        checkWords.Checked = false;
                        for (int i = 0; i < list.Length; i++)
                        {
                            hexColorsList.Items.Add(list[i]);
                            hexColorsList.Items[i].ForeColor = ColorTranslator.FromHtml(list[i]);
                        }
                    }
                    else
                    {
                        checkColors.Checked = false;
                        checkWords.Checked = true;
                        for (int i = 0; i < list.Length; i++)
                        {
                            wordsColoredList.Items.Add(list[i]);
                        }
                    }

                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }
        
        private void wordsCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (checkWords.Checked)
            {
                wordTextBox.Enabled = true;
                wordsColoredList.Enabled = true;
                wordsListLabel.Enabled = true;
                wordsColoredList.Items.Clear();
            }
            else
            {
                if (!hexColorTextBox.Enabled)
                {
                    hexColorTextBox.Enabled = true;
                    hexColorsList.Enabled = true;
                    checkColors.Checked = true;
                    colorsListLabel.Enabled = true;
                }
                wordTextBox.Enabled = false;
                wordTextBox.Text = "";
                wordsColoredList.Enabled = false;
                wordsListLabel.Enabled = false;
                wordsColoredList.Items.Clear();
            }
        }
        
        private void colorsCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (checkColors.Checked)
            {
                hexColorTextBox.Enabled = true;
                hexColorTextBox.Text = "#000000";
                hexColorsList.Enabled = true;
                wordsColoredList.Items.Clear();
            }
            else
            {
                if (!wordTextBox.Enabled)
                {
                    wordTextBox.Enabled = true;
                    wordsColoredList.Enabled = true;
                    checkWords.Checked = true;
                }
                hexColorTextBox.Enabled = false;
                hexColorsList.Enabled = false;
                hexColorsList.Items.Clear();
            }
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            string colorCode = pickColor();
            if (colorCode != null)
            {
                hexColorTextBox.ForeColor = ColorTranslator.FromHtml(colorCode);
                hexColorTextBox.Text = colorCode;
            }
        }

        private string pickColor()
        {
            ColorDialog MyDialog = new ColorDialog();
            Color colorPicked = new Color();
            MyDialog.CustomColors = new int[] {
                                        ColorTranslator.ToOle(ColorTranslator.FromHtml("#F8E000")),
                                        ColorTranslator.ToOle(ColorTranslator.FromHtml("#007BB7")),
                                        ColorTranslator.ToOle(ColorTranslator.FromHtml("#7EC845")),
                                        ColorTranslator.ToOle(ColorTranslator.FromHtml("#D01C1F"))
                                      };
            colorPicked = this.BackColor;
            string hexColor = null;
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                colorPicked = MyDialog.Color;
                hexColor = "#" + colorPicked.R.ToString("X2") + colorPicked.G.ToString("X2") + colorPicked.B.ToString("X2");
            }
            return hexColor;
        }

        private void colorTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var box = (TextBox)sender;
                if (box.Text.Length <= 1)
                {
                    box.Text = "#";
                    box.SelectionStart = 1;
                }

                if (box.Text.Length == 7 && !Regex.IsMatch(box.Text, hexPattern))
                {
                    throw new Exception("O código de cor deve estar em formato hexadecimal.\nEx: #000000");
                }


                if (Regex.IsMatch(hexColorTextBox.Text, hexPattern) && hexColorTextBox.TextLength == 7)
                {
                    hexColorTextBox.ForeColor = ColorTranslator.FromHtml(hexColorTextBox.Text);
                }
                else
                {
                    hexColorTextBox.ForeColor = Color.Black;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(Regex.IsMatch(hexColorTextBox.Text, hexPattern) && hexColorTextBox.TextLength == 7)
            {
                if(checkWords.Checked && checkColors.Checked && !String.IsNullOrEmpty(wordTextBox.Text) && !String.IsNullOrEmpty(hexColorTextBox.Text))
                {
                    if(wordsColoredList.Items.Count != hexColorsList.Items.Count || hexColorsList.Items.Count != wordsColoredList.Items.Count)
                    {
                        wordsColoredList.Items.Clear();
                        hexColorsList.Items.Clear();
                    }
                    wordsColoredList.Items.Add(wordTextBox.Text);
                    hexColorsList.Items.Add(hexColorTextBox.Text);
                }
                if(checkWords.Checked && !checkColors.Checked && !String.IsNullOrEmpty(wordTextBox.Text))
                {
                    wordsColoredList.Items.Add(wordTextBox.Text);
                }
                if (checkColors.Checked && !checkWords.Checked && !String.IsNullOrEmpty(hexColorTextBox.Text))
                {
                    hexColorsList.Items.Add(hexColorTextBox.Text);
                }
                foreach (ListViewItem lvw1 in hexColorsList.Items)
                {
                    lvw1.ForeColor = ColorTranslator.FromHtml(lvw1.Text);
                }
                for (int i = 0; i < wordsColoredList.Items.Count; i++)
                {
                    if(i < hexColorsList.Items.Count)
                    {
                        wordsColoredList.Items[i].ForeColor = ColorTranslator.FromHtml(hexColorsList.Items[i].Text);
                    }
                }
            }
            else
            { 
                MessageBox.Show("A cor deve estar no formato hexadecimal padrão;\nExemplo: #000000");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(checkWords.Checked && checkColors.Checked)
            {
                if (hexColorsList.Items.Count > 0 && wordsColoredList.Items.Count > 0)
                {
                    if (hexColorsList.SelectedItems.Count == 0)
                    {
                        hexColorsList.Items.RemoveAt(hexColorsList.Items.Count - 1);
                        wordsColoredList.Items.RemoveAt(wordsColoredList.Items.Count - 1);
                    }
                    else
                    {
                        for (int i = 0; i < hexColorsList.Items.Count; i++)
                        {
                            if (hexColorsList.Items[i].Selected || wordsColoredList.Items[i].Selected)
                            {
                                hexColorsList.Items[i].Remove();
                                wordsColoredList.Items[i].Remove();
                                i--;
                            }
                        }
                    }
                }
            }
            if (checkWords.Checked && !checkColors.Checked)
            {
                if (wordsColoredList.Items.Count > 0)
                {
                    if (wordsColoredList.SelectedItems.Count == 0)
                        wordsColoredList.Items.RemoveAt(wordsColoredList.Items.Count - 1);
                    else
                        foreach (ListViewItem eachItem in wordsColoredList.SelectedItems)
                            wordsColoredList.Items.Remove(eachItem);
                }
            }

            if (checkColors.Checked && !checkWords.Checked)
            {
                if (hexColorsList.Items.Count > 0)
                {
                    if (hexColorsList.SelectedItems.Count == 0)
                        hexColorsList.Items.RemoveAt(hexColorsList.Items.Count - 1);
                    else
                        foreach (ListViewItem eachItem in hexColorsList.SelectedItems)
                            hexColorsList.Items.Remove(eachItem);
                }
            }
        }

        private void hexColorsList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

            if (!Regex.IsMatch(hexColorTextBox.Text, hexPattern) || !(hexColorTextBox.TextLength == 7))
            {

            }
        }
        
        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(listNameTextBox.Text))
                {
                    throw new Exception("Nome do(s) arquivo(s) deve ser preenchido");
                }

                if (/*saveColorsList.ShowDialog() == DialogResult.OK && */checkColors.Enabled) // lê instrução se houver
                {
                    if (hexColorsList.Items.Count > 0 && (MessageBox.Show("Deseja salvar o arquivo " + listNameTextBox.Text + "_Color.lst?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.OK))
                    {
                        if (File.Exists(path + listNameTextBox.Text + "_Color.lst"))
                        {
                            DialogResult dialogResult = MessageBox.Show("Uma lista com este nome já existe.\nDeseja sobrescrevê-la?", "", MessageBoxButtons.OKCancel);
                            if (dialogResult == DialogResult.Cancel)
                            {
                                throw new Exception("A lista não será salva!");
                            }
                        }

                        StreamWriter writer1 = new StreamWriter(path + listNameTextBox.Text + "_Color.lst" /*saveColorsList.OpenFile()*/);

                        for (int i = 0; i < hexColorsList.Items.Count; i++)
                        {
                            writer1.Write(hexColorsList.Items[i].Text + "\t");
                        }

                        writer1.Close();
                        MessageBox.Show("A lista " + listNameTextBox.Text + " foi salva com sucesso");
                    }
                    else
                    {
                        throw new Exception("A lista de cores não foi salva!");
                    }

                }

                if (/*saveWordsList.ShowDialog() == DialogResult.OK && */ checkWords.Enabled) // lê instrução se houver
                {
                    if (wordsColoredList.Items.Count > 0 && (MessageBox.Show("Deseja salvar o arquivo " + listNameTextBox.Text + "_words.lst?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.OK))
                    {
                        if (File.Exists(path + listNameTextBox.Text + "_words.lst"))
                        {
                            DialogResult dialogResult = MessageBox.Show("Uma lista com este nome já existe.\nDeseja sobrescrevê-la?", "", MessageBoxButtons.OKCancel);
                            if (dialogResult == DialogResult.Cancel)
                            {
                                throw new Exception("A lista não será salva!");
                            }
                        }

                        StreamWriter writer2 = new StreamWriter(path + listNameTextBox.Text + "_words.lst" /*saveWordsList.OpenFile()*/);

                        for (int i = 0; i < wordsColoredList.Items.Count; i++)
                        {
                            writer2.Write(wordsColoredList.Items[i].Text + "\t");
                        }

                        //writer2.Dispose();
                        writer2.Close();
                        MessageBox.Show("A lista " + listNameTextBox.Text + "_words.lst foi salva com sucesso");
                    }
                    else
                    {
                        throw new Exception("A lista de palavras não foi salva!");
                    }
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cancelButton_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
    
}
