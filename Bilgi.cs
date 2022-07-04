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
    public partial class Bilgi : Form
    {
        public Bilgi()
        {
            InitializeComponent();
        }

        bool mov;
        int movX, movY;

        private void Bilgi_Load(object sender, EventArgs e)
        {

        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Çıkış yapmak istiyor musunuz?", "Müşteri Kayıt İşlemi", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Giris giris = new Giris();
                giris.Show();
                this.Hide();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tercih tercih = new Tercih();
            tercih.Show();
            this.Hide();
        }

        private void Bilgi_MouseDown(object sender, MouseEventArgs e)
        {
            mov = true;
            movX = e.X;
            movY = e.Y;
        }

        private void Bilgi_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void Bilgi_MouseUp(object sender, MouseEventArgs e)
        {
            mov = false;
        }
    }
}
