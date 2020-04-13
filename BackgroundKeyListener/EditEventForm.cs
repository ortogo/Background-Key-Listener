using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgroundKeyListener
{
    public partial class EditEventForm : Form
    {

        public string Shortcut = string.Empty;

        public EditEventForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Shortcut = tbKey.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void EditEventForm_Shown(object sender, EventArgs e)
        {
            tbKey.Text = Shortcut;
        }
    }
}
