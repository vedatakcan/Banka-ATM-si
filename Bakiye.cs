using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje
{

    public partial class Bakiye : Form
    {

        public static SqlConnection baglantı = new SqlConnection("Data Source=.\\SQLEXPRESS; Initial Catalog = Bank; Integrated Security=TRUE");
        public Bakiye()
        {
            InitializeComponent();
        }
        bool mov;
        int movX, movY;
        private void Bakiye_Load(object sender, EventArgs e)
        {
            label1.Text = Giris.bakiye;
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            Tercih tercih = new Tercih();
            tercih.Show();
            this.Hide();
        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Çıkış yapmak istiyor musunuz?", "Müşteri Kayıt İşlemi", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Giris giris = new Giris();
                giris.Show();
                this.Hide();
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Bakiye_MouseDown(object sender, MouseEventArgs e)
        {
            mov = true;
            movX = e.X;
            movY = e.Y;
        }

        private void Bakiye_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void Bakiye_MouseUp(object sender, MouseEventArgs e)
        {
            mov = false;
        }
    }
}
