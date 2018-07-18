﻿
using System;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;
using TestPlatform.Views.MainForms;
using TestPlatform.Models;

namespace TestPlatform.Views.SidebarUserControls
{
    public partial class ReactionControl : DefaultUserControl
    {
        private ResourceManager LocRM = new ResourceManager("TestPlatform.Resources.Localizations.LocalizedResources", typeof(FormMain).Assembly);
        private CultureInfo currentCulture = CultureInfo.CurrentUICulture;

        public ReactionControl()
        {
            this.Dock = DockStyle.Fill;
            InitializeComponent();
        }


        private bool checkSave()
        {
            bool result = false;
            if (FileManipulation.GlobalFormMain._contentPanel.Controls[0] is FormTRConfig)
            {
                DialogResult dialogResult = MessageBox.Show(LocRM.GetString("savePending", currentCulture), LocRM.GetString("savePendingTitle", currentCulture), MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    FormTRConfig programToSave = (FormTRConfig)(FileManipulation.GlobalFormMain._contentPanel.Controls[0]);
                    result = programToSave.save();
                }
                else
                {
                    FileManipulation.GlobalFormMain._contentPanel.Controls.Clear();
                    return true;
                }
            }
            if(result == false)
            { 
                FileManipulation.GlobalFormMain._contentPanel.Controls.Clear();
                return true;
            }
            else
            {
                return false;
            }
        }


        private void newReactButton_Click(object sender, EventArgs e)
        {
            bool screenTranslationAllowed = true;
            try
            {
                if (newReactButton.Checked)
                {
                    if (FileManipulation.GlobalFormMain._contentPanel.Controls.Count > 0)
                    {
                        screenTranslationAllowed = checkSave();
                    }
                    if (screenTranslationAllowed)
                    {
                        FormTRConfig configureProgram = new FormTRConfig("false");
                        FileManipulation.GlobalFormMain._contentPanel.Controls.Add(configureProgram);
                        newReactButton.Checked = false;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void editReactButton_Click(object sender, EventArgs e)
        {
            bool screenTranslationAllowed = true;
            if (editReactButton.Checked)
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
                        defineProgram = new FormDefine(LocRM.GetString("editProgram", currentCulture), ReactionProgram.GetProgramsPath(), "prg", "program", false, false);
                        result = defineProgram.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            editProgramName = defineProgram.ReturnValue;
                            FormTRConfig configureProgram = new FormTRConfig(editProgramName);
                            configureProgram.PrgName = editProgramName;
                            FileManipulation.GlobalFormMain._contentPanel.Controls.Add(configureProgram);
                            editReactButton.Checked = false;
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

        private void deleteReactButton_Click(object sender, EventArgs e)
        {
            bool screenTranslationAllowed = true;
            try
            {
                if (deleteReactButton.Checked)
                {
                    if (FileManipulation.GlobalFormMain._contentPanel.Controls.Count > 0)
                    {
                        screenTranslationAllowed = checkSave();
                    }
                    if (screenTranslationAllowed)
                    {
                        FileManagment deleteProgram = new FileManagment(ReactionProgram.GetProgramsPath(), FileManipulation.ReactionTestFilesBackupPath, 'd', LocRM.GetString("reactionTest", currentCulture));
                        FileManipulation.GlobalFormMain._contentPanel.Controls.Add(deleteProgram);
                        deleteReactButton.Checked = false; 
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void recoverReactButton_Click(object sender, EventArgs e)
        {
            bool screenTranslationAllowed = true;
            try
            {
                if (recoverReactButton.Checked)
                {
                    if (FileManipulation.GlobalFormMain._contentPanel.Controls.Count > 0)
                    {
                        screenTranslationAllowed = checkSave();
                    }
                    if (screenTranslationAllowed)
                    {
                        FileManagment recoverProgram = new FileManagment(FileManipulation.ReactionTestFilesBackupPath, ReactionProgram.GetProgramsPath(), 'r', LocRM.GetString("reactionTest", currentCulture));
                        FileManipulation.GlobalFormMain._contentPanel.Controls.Add(recoverProgram);
                        recoverReactButton.Checked = false; 
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
