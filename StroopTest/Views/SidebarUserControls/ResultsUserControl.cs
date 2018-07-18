﻿using System;
using System.Windows.Forms;
using TestPlatform.Models;
using TestPlatform.Views.ReactionPages;
using TestPlatform.Views.ExperimentPages;
using TestPlatform.Views.MatchingPages;

namespace TestPlatform.Views.SidebarUserControls
{
    public partial class ResultsUserControl : DefaultUserControl
    {

        public ResultsUserControl()
        {
            this.Dock = DockStyle.Fill;
            InitializeComponent();
        }

        private void StroopButton_Click(object sender, EventArgs e)
        {
            if(FileManipulation.GlobalFormMain._contentPanel.Controls.Count > 0) //if another result tab is open then close it
            {
                FileManipulation.GlobalFormMain._contentPanel.Controls.Clear(); 
            }
            FormShowData showData;
            try
            {
                showData = new FormShowData();
                FileManipulation.GlobalFormMain._contentPanel.Controls.Add(showData);
                StroopButton.Checked = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void reactionButton_Click(object sender, EventArgs e)
        {
            if (FileManipulation.GlobalFormMain._contentPanel.Controls.Count > 0) //if another result tab is open then close it
            {
                FileManipulation.GlobalFormMain._contentPanel.Controls.Clear();
            }
            ReactionResultUserControl showData;
            try
            {
                showData = new ReactionResultUserControl();
                FileManipulation.GlobalFormMain._contentPanel.Controls.Add(showData);
                reactionButton.Checked = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void experimentButton_Click(object sender, EventArgs e)
        {
            if (FileManipulation.GlobalFormMain._contentPanel.Controls.Count > 0) //if another result tab is open then close it
            {
                FileManipulation.GlobalFormMain._contentPanel.Controls.Clear();
            }
            try
            {
                ExperimentResultUserControl showData = new ExperimentResultUserControl();
                FileManipulation.GlobalFormMain._contentPanel.Controls.Add(showData);
                experimentButton.Checked = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void matchingButton_Click(object sender, EventArgs e)
        {
            if (FileManipulation.GlobalFormMain._contentPanel.Controls.Count > 0) //if another result tab is open then close it
            {
                FileManipulation.GlobalFormMain._contentPanel.Controls.Clear();
            }
            try
            {
                MatchingResultUserControl showData = new MatchingResultUserControl();
                FileManipulation.GlobalFormMain._contentPanel.Controls.Add(showData);
                matchingButton.Checked = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
