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
    public partial class ParaYatirCek : Form
   
    {
        public static SqlConnection baglantı = new SqlConnection("Data Source=.\\SQLEXPRESS; Initial Catalog = Bank; Integrated Security=TRUE");
        public ParaYatirCek()
        {
            InitializeComponent();
        }

        bool mov;
        int movX, movY;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ParaYatirCek_Load(object sender, EventArgs e)
        {

            label1.Text = Giris.bakiye;

        }

        private void btnCek_Click(object sender, EventArgs e)
        {
            if (txtTutarGir.Text == "" || txtTutarGir.Text == String.Empty)
            {
                MessageBox.Show("Lütfen bir tutar giriniz");
            }
            else
            {
                int cekilecekTutar = Convert.ToInt32(txtTutarGir.Text);

                if (cekilecekTutar < 0)
                {
                    MessageBox.Show("Lütfen pozitif bir tutar giriniz");
                }

                var dialog = MessageBox.Show("Çekme işlemini onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    if (txtTutarGir.Text == "" || txtTutarGir.Text == String.Empty)
                    {
                        MessageBox.Show("Lütfen bir tutar giriniz");
                    }
                    if (cekilecekTutar % 10 == 0)
                    {
                        if (Convert.ToInt32(Giris.bakiye) >= cekilecekTutar)
                        {

                            string amount = Convert.ToString((Convert.ToInt32(Giris.bakiye) - cekilecekTutar));


                            SqlCommand command = new SqlCommand("Update Member SET amount=@amount WHERE id=@id", baglantı);
                            command.Parameters.AddWithValue("@amount", amount);
                            command.Parameters.AddWithValue("@id", Giris.id);
                            baglantı.Open();

                            SqlDataReader oku = command.ExecuteReader();
                            baglantı.Close();

                            label1.Text = Convert.ToString((Convert.ToInt32(Giris.bakiye) - cekilecekTutar));
                            Giris.bakiye = amount;
                            txtTutarGir.Text = "";


                            MessageBox.Show("Çekme işlemi gerçekleştirildi");
                        }
                        else
                        {
                            MessageBox.Show("Bakiyeniz yetersiz");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen 10'un katları bir tutar çekiniz");
                    }

                }
                else
                {
                    MessageBox.Show("Çekme işlemi yapılamadı");
                }
            }
    
        }

        private void btnYatir_Click(object sender, EventArgs e)
        {
            if (txtTutarGir.Text == "" || txtTutarGir.Text == String.Empty)
            {
                MessageBox.Show("Lütfen bir tutar giriniz");
            }
            else
            {
                int yatirilacakTutar = Convert.ToInt32(txtTutarGir.Text);

                if (yatirilacakTutar < 0)
                {
                    MessageBox.Show("Lütfen pozitif bir tutar giriniz");
                }
                var dialog = MessageBox.Show("Yatırma işlemini onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    if (yatirilacakTutar % 10 == 0)
                    {
                        string amount = Convert.ToString((yatirilacakTutar + Convert.ToInt32(Giris.bakiye)));


                        SqlCommand command = new SqlCommand("Update Member SET amount=@amount WHERE id=@id", baglantı);
                        command.Parameters.AddWithValue("@amount", amount);
                        command.Parameters.AddWithValue("@id", Giris.id);
                        baglantı.Open();

                        SqlDataReader oku = command.ExecuteReader();
                        baglantı.Close();

                        label1.Text = Convert.ToString((yatirilacakTutar + Convert.ToInt32(Giris.bakiye)));
                        Giris.bakiye = amount;

                        txtTutarGir.Text = "";
                        MessageBox.Show("Yatırma işleminiz gerçekleştirildi");
                    }
                    else
                    {
                        MessageBox.Show("Lütfen 10'un katları bir tutar giriniz");
                    }
                }
                else
                {
                    MessageBox.Show("Yatırma işlemi yapılamadı");
                }
            }
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Tercih tercih = new Tercih();
            tercih.Show();
            this.Hide();
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

        private void btn10_Click(object sender, EventArgs e)
        {
            txtTutarGir.Text = Convert.ToString(10);
        }
        private void btn20_Click(object sender, EventArgs e)
        {
            txtTutarGir.Text = Convert.ToString(20);
        }
        private void btn50_Click(object sender, EventArgs e)
        {
            txtTutarGir.Text = Convert.ToString(50);
        }

        private void btn100_Click(object sender, EventArgs e)
        {
            txtTutarGir.Text = Convert.ToString(100);
        }

        private void button200_Click(object sender, EventArgs e)
        {
            txtTutarGir.Text = Convert.ToString(200);
        }

        private void btn500_Click(object sender, EventArgs e)
        {
            txtTutarGir.Text = Convert.ToString(500);
        }

        private void txtTutarGir_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ParaYatirCek_MouseDown(object sender, MouseEventArgs e)
        {
            mov = true;
            movX = e.X;
            movY = e.Y;
        }

        private void ParaYatirCek_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void ParaYatirCek_MouseUp(object sender, MouseEventArgs e)
        {
            mov = false;

        }
    }
}
