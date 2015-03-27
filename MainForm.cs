using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RemoteDesktopShutdown
{
    internal partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Font = SystemInformation.MenuFont;
        }

        public void AddActions(IEnumerable<ShutdownAction> actions)
        {
            actionComboBox.BeginUpdate();

            foreach (ShutdownAction action in actions)
            {
                actionComboBox.Items.Add(action);
            }

            actionComboBox.EndUpdate();
        }

        public ShutdownAction SelectedAction
        {
            get
            {
                return (ShutdownAction)actionComboBox.SelectedItem;
            }
            set
            {
                actionComboBox.SelectedItem = value;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (actionComboBox.SelectedItem != null)
                DialogResult = DialogResult.OK;
        }
    }
}