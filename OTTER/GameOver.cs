using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OTTER
{
    public partial class GameOver : Form
    {
        public GameOver(string message)
        {
            InitializeComponent();
            lblText.Text = message;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
        private void btnPA_Click(object sender, EventArgs e)
        {
            this.Hide();
            Application.Restart();
            this.Close();
        }
    }
}
