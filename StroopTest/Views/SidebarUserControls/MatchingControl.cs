﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestPlatform.Views.ExperimentPages;
using System.Resources;
using System.Globalization;
using TestPlatform.Views.MainForms;
using TestPlatform.Views.MatchingPages;
using TestPlatform.Models;

namespace TestPlatform.Views.SidebarUserControls
{
    public partial class MatchingControl : UserControl
    {
        private ResourceManager LocRM = new ResourceManager("TestPlatform.Resources.Localizations.LocalizedResources", typeof(FormMain).Assembly);
        private CultureInfo currentCulture = CultureInfo.CurrentUICulture;

        public MatchingControl()
        {
            InitializeComponent();
        }


        private bool checkSave()
        {

            bool result = false;
            if (FileManipulation.GlobalFormMain._contentPanel.Controls[0] is FormMatchConfig)
            {
                DialogResult dialogResult = MessageBox.Show(LocRM.GetString("savePending", currentCulture), LocRM.GetString("savePendingTitle", currentCulture), MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    FormMatchConfig programToSave = (FormMatchConfig)(FileManipulation.GlobalFormMain._contentPanel.Controls[0]);
                    result = programToSave.save();
                }
                else
                {
                    FileManipulation.GlobalFormMain._contentPanel.Controls.Clear();
                    return true;
                }
            }
            if (result == false)
            {
                FileManipulation.GlobalFormMain._contentPanel.Controls.Clear();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void newMatchButton_Click(object sender, EventArgs e)
        {
            bool screenTranslationAllowed = true;
            try
            {
                if (FileManipulation.GlobalFormMain._contentPanel.Controls.Count > 0)
                {
                    screenTranslationAllowed = checkSave();
                }
                if (screenTranslationAllowed)
                {
                    if (newMatchButton.Checked)
                    {
                        FormMatchConfig newExperiment = new FormMatchConfig("false");
                        FileManipulation.GlobalFormMain._contentPanel.Controls.Add(newExperiment);
                        newMatchButton.Checked = false;
                    }
                    else
                    {
                        /*do nothing*/
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void editMatchButton_Click(object sender, EventArgs e)
        {
            bool screenTranslationAllowed = true;
            if (editMatchButton.Checked)
            {
                if (FileManipulation.GlobalFormMain._contentPanel.Controls.Count > 0)
                {
                    screenTranslationAllowed = checkSave();
                }
                if (screenTranslationAllowed)
                {
                    FormDefine defineProgram;
                    DialogResult result;
                    string editProgramName = "error";

                    try
                    {
                        defineProgram = new FormDefine(LocRM.GetString("editProgram", currentCulture), MatchingProgram.GetProgramsPath(), "prg", "program", false, false);
                        result = defineProgram.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            editProgramName = defineProgram.ReturnValue;
                            FormMatchConfig configureProgram = new FormMatchConfig(editProgramName);
                            configureProgram.PrgName = editProgramName;
                            FileManipulation.GlobalFormMain._contentPanel.Controls.Add(configureProgram);
                            editMatchButton.Checked = false;
                        }
                        else
                        {
                            /*do nothing, user cancelled selection of program*/
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
            else
            {
                /*do nothing*/
            }
        }

        private void deleteMatchButton_Click(object sender, EventArgs e)
        {
            {
                bool screenTranslationAllowed = true;

                if (FileManipulation.GlobalFormMain._contentPanel.Controls.Count > 0)
                {
                    screenTranslationAllowed = checkSave();
                }
                if (screenTranslationAllowed)
                {
                    try
                    {
                        if (deleteMatchButton.Checked)
                        {
                            FileManagment deleteProgram = new FileManagment(MatchingProgram.GetProgramsPath(), FileManipulation._matchingTestFilesBackupPath, 'd', LocRM.GetString("matchingTest", currentCulture));
                            FileManipulation.GlobalFormMain._contentPanel.Controls.Add(deleteProgram);
                            deleteMatchButton.Checked = false;
                        }
                        else
                        {
                            /*do nothing */
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }

        private void recoverMatchButton_Click(object sender, EventArgs e)
        {
            bool screenTranslationAllowed = true;

            if (FileManipulation.GlobalFormMain._contentPanel.Controls.Count > 0)
            {
                screenTranslationAllowed = checkSave();
            }
            if (screenTranslationAllowed)
            {
                try
                {
                    if (recoverMatchButton.Checked)
                    {
                        FileManagment recoverProgram = new FileManagment(FileManipulation._matchingTestFilesBackupPath, MatchingProgram.GetProgramsPath(), 'r', LocRM.GetString("matchingTest", currentCulture));
                        FileManipulation.GlobalFormMain._contentPanel.Controls.Add(recoverProgram);
                        recoverMatchButton.Checked = false;
                    }
                    else
                    {
                        /*do nothing */
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void editMatchButton_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
