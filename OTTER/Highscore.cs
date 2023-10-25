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
    public partial class Scoreboard : Form
    {
        public Scoreboard()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Scoreboard_Load(object sender, EventArgs e)
        {
            using (StreamReader sr = File.OpenText("HS.txt"))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] n = line.Split(' ');
                    string player = n[0];
                    int bodovi = int.Parse(n[1]);
                    if (bodovi != 0)
                    {
                        richTextBox1.AppendText(line + "\n");
                    }
                    line = sr.ReadLine();
                }
            }
        }
    }
}
