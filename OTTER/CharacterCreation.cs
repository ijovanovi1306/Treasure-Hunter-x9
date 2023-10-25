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
    public partial class CharacterCreation : Form
    {
        public CharacterCreation()
        {
            InitializeComponent();
        }

        public string type = "caster";
        public string player = "Player";
        public string dat = "HS.txt";

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                MessageBox.Show("Please enter name for your character.");
                return;
            }
            else if (!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked)
            {
                MessageBox.Show("Please select your character.");
                return;
            }
            else
            {
                player = textBox1.Text;
                this.Hide();
                BGL bgl = new BGL(type,player);
                BGL.allSprites.Clear();
                bgl.ShowDialog();
                bgl.Dispose();
                this.Close();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.type = "caster";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.type = "archer";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.type = "assassin";
                
        }

        private void btnGB_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
