using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OTTER
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnQ_Click(object sender, EventArgs e)
        {
            hs.Dispose();
            cc.Dispose();
            Application.Exit();
        }

      
        Scoreboard hs =new Scoreboard();
        CharacterCreation cc = new CharacterCreation();

        private void btnHS_Click(object sender, EventArgs e)
        {
            hs.ShowDialog();

        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Collect 9 treasure and defeat the boss to win. \nUse arrow keys(or WASD) to move and Space(or Mouse Click) to shoot. ");
        }

        private void btnNG_Click(object sender, EventArgs e)
        {
            cc.ShowDialog();
            
        }
    }
}
