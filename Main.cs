using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        bool mov;
        int movX, movY;


        private void button1_Click(object sender, EventArgs e)
        {
            Kayit member = new Kayit();
            member.Show();
            this.Hide();
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

       

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Giris giris = new Giris();
            giris.Show();
            this.Hide();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            mov = true;
            movX = e.X;
            movY = e.Y;
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov)
            {
                this.SetDesktopLocation(MousePosition.X-movX, MousePosition.Y-movY);
            }
        }

        private void Main_MouseUp(object sender, MouseEventArgs e)
        {
            mov = false;
        }
    }
}
