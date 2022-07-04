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
    
    public partial class Tercih : Form

    {
        public static SqlConnection baglantı = new SqlConnection("Data Source=.\\SQLEXPRESS; Initial Catalog = Bank; Integrated Security=TRUE");
    
        public Tercih()
        {
            InitializeComponent();
        }
        bool mov;
        int movX, movY;

        private void button2_Click(object sender, EventArgs e)
        {
            ParaYatirCek tutar = new ParaYatirCek();
            tutar.Show();
            this.Hide();
        }

          

        private void button7_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Çıkmak istediğinizden emin misiniz?", "Çıkış", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                MessageBox.Show("Çıkış Yapıldı.");
                Giris giris = new Giris();
                giris.Show();
                this.Hide();
            }
           
        }

        private void btnFaturaOdeme_Click(object sender, EventArgs e)
        {
            Fatura fatura = new Fatura();
            fatura.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Tercih_Load(object sender, EventArgs e)
        {
            label1.Text = Giris.girisYapanAdSoyad;

        }

        private void btnAyarlar_Click(object sender, EventArgs e)
        {
            Guncelleme guncelleme = new Guncelleme();
            guncelleme.Show();
            this.Hide();
          
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBakiye_Click(object sender, EventArgs e)

        {

            Bakiye bakiye = new Bakiye();
            bakiye.Show();
            this.Hide();

        }

        private void btnBilgi_Click(object sender, EventArgs e)
        {
            Bilgi bilgi = new Bilgi();
            bilgi.Show();
            this.Hide();

        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            Transfer transfer = new Transfer();
            transfer.Show();
            this.Hide();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Tercih_MouseDown(object sender, MouseEventArgs e)
        {
            mov = true;
            movX = e.X;
            movY = e.Y;
        }

        private void Tercih_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void Tercih_MouseUp(object sender, MouseEventArgs e)
        {
            mov = false;
        }
    }
}
