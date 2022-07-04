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
    public partial class Fatura : Form
    {
        public static SqlConnection baglantı = new SqlConnection("Data Source=.\\SQLEXPRESS; Initial Catalog = Bank; Integrated Security=TRUE");
        public Fatura()
        {
            InitializeComponent();
        }

        bool mov;
        int movX, movY;

        private void Fatura_Load(object sender, EventArgs e)
        {

            label1.Text = Giris.bakiye;
        }

        private void btnOdemeYap_Click(object sender, EventArgs e)
        {
            if (txtTutarGir.Text == "" || txtTutarGir.Text == String.Empty)
            {
                MessageBox.Show("Lütfen bir tutar giriniz");
            }
            else
            {
                int odenecekTutar = Convert.ToInt32(txtTutarGir.Text);

                if (odenecekTutar < 0)
                {
                    MessageBox.Show("Lütfen pozitif bir tutar giriniz");
                }

                var dialog = MessageBox.Show("Ödemeyi onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    if (Convert.ToInt32(Giris.bakiye) >= odenecekTutar)
                    {

                        string amount = Convert.ToString((Convert.ToInt32(Giris.bakiye) - odenecekTutar));


                        SqlCommand command = new SqlCommand("Update Member SET amount=@amount WHERE id=@id", baglantı);
                        command.Parameters.AddWithValue("@amount", amount);
                        command.Parameters.AddWithValue("@id", Giris.id);
                        baglantı.Open();

                        SqlDataReader oku = command.ExecuteReader();
                        baglantı.Close();

                        label1.Text = Convert.ToString((Convert.ToInt32(Giris.bakiye) - odenecekTutar));
                        Giris.bakiye = amount;
                        txtTutarGir.Text = "";
                        MessageBox.Show("Ödeme işlemi gerçekleştirildi");
                    }
                    else
                    {
                        MessageBox.Show("Bakiyeniz yetersiz");
                    }

                }
                else
                {
                    MessageBox.Show("Ödeme yapılamadı");
                }
            }
            

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

        private void btnGeri_Click(object sender, EventArgs e)
        {
            Tercih tercih = new Tercih();
            tercih.Show();
            this.Close();

        }

        private void txtTutarGir_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTutarGir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Fatura_MouseDown(object sender, MouseEventArgs e)
        {
            mov = true;
            movX = e.X;
            movY = e.Y;
        }

        private void Fatura_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }

        }

        private void Fatura_MouseUp(object sender, MouseEventArgs e)
        {
            mov = false;
        }
    }
}
