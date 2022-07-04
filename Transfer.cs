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
    public partial class Transfer : Form
    {
        public Transfer()
        {
            InitializeComponent();
        }
        bool mov;
        int movX, movY;

        public static SqlConnection baglantı = new SqlConnection("Data Source=.\\SQLEXPRESS; Initial Catalog = Bank; Integrated Security=TRUE");
     

        private void btnGeri_Click(object sender, EventArgs e)
        {
            Tercih tercih = new Tercih();
            tercih.Show();
            this.Hide();
        }

        private void Transfer_Load(object sender, EventArgs e)
        {

            label1.Text = Giris.bakiye;
        }
        private bool kayitliMi(string phone)
        {
            SqlCommand command = new SqlCommand("Select * from Member WHERE  phone_number=@phone", baglantı);

            command.Parameters.AddWithValue("@phone", phone);
            baglantı.Open();

            SqlDataReader oku = command.ExecuteReader();

            bool result;

            if (oku.Read())
            {
                result = true;
            }
            else
            {
                result = false;
            }
            baglantı.Close();

            return result;


        }
      
        private void btnGonder_Click(object sender, EventArgs e)
        {
            string phone = txtTelefonNoGir.Text;
            string amount = txtTutarGir.Text;

            if (phone == "" || amount == "" || phone == String.Empty || amount == String.Empty)
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz");
            }
            else {
                if (phone.Length == 11)
                {
                    var dialog = MessageBox.Show("Transferi onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        if (kayitliMi(phone))
                        {

                            if (Convert.ToInt32(Giris.bakiye) >= Convert.ToInt32(amount))
                            {
                                paraGonder(phone, amount);
                                txtTutarGir.Text = "";

                            }
                            else
                            {
                                MessageBox.Show("Bakiyeniz yetersiz", "Uyarı");
                            }

                      
                    }
                    else
                    {
                        MessageBox.Show(phone + " nolu telefon numaralı kişi kayıtlarda yok", "Uyarı");
                    }

                    } 
                }
                else
                {
                    MessageBox.Show("Telefon numarasi 11 haneli olmali.", "Uyarı");
                }

            }
        }

        private int gonderenKisininEskiBakiyesi()
        {
            // Gonderenin ilk bakiyesini veritabsanindan çek
            SqlCommand gonderenIlkBakiyesi = new SqlCommand("Select * from Member WHERE id=@id", baglantı);
            gonderenIlkBakiyesi.Parameters.AddWithValue("@id", Giris.id);
            baglantı.Open();

            SqlDataReader command = gonderenIlkBakiyesi.ExecuteReader();

            int gondereninBakiyesi = 0;

            while (command.Read())
            {
                gondereninBakiyesi = Convert.ToInt32(command["amount"]);
            }

            baglantı.Close();

            return gondereninBakiyesi;
        }

        private void gondereninBakiyesiniGuncelle(int gondereninKalanBakiyesi)
        {
            // Gönderen kişinin bakiyesini düşür
            SqlCommand command = new SqlCommand("Update Member SET amount=@amount WHERE id=@id", baglantı);
            command.Parameters.AddWithValue("@amount", gondereninKalanBakiyesi);

            command.Parameters.AddWithValue("@id", Giris.id);
            baglantı.Open();

            command.ExecuteReader();
            baglantı.Close();
        }
        private int alicininEskiBakiyesiniVeriTabanındanCek(string phone)
        {

            // Alicini ilk bakiyesini veritabsanindan çek
            SqlCommand command = new SqlCommand("Select * from Member WHERE phone_number=@phone", baglantı);
            command.Parameters.AddWithValue("@phone", phone);
            baglantı.Open();

            SqlDataReader oku = command.ExecuteReader();

            int aliciBakiyesi = 0;

            while (oku.Read())
            {
                aliciBakiyesi = Convert.ToInt32(oku["amount"]);
            }
            baglantı.Close();
            return aliciBakiyesi;
        }
        private void aliciYeniBakiyeGuncelle(string phone, int gonderilenMiktar, int alicininGuncelBakiyesi)
        {
            // Alicini yeni bakiyesini güncelle
            SqlCommand aliciBakiyeGuncelle = new SqlCommand("Update Member SET amount=@amount WHERE phone_number=@phone", baglantı);
            aliciBakiyeGuncelle.Parameters.AddWithValue("@amount", alicininGuncelBakiyesi);
            aliciBakiyeGuncelle.Parameters.AddWithValue("@phone", phone);
            baglantı.Open();

            aliciBakiyeGuncelle.ExecuteReader();
            baglantı.Close();
            MessageBox.Show(gonderilenMiktar + " Tl gönderme işleminiz başarılı");
        }

        private void paraGonder(string phone, string amount)
        {
            // Gönderenin göndereceği miktar
            int gonderilenMiktar = Convert.ToInt32(amount);

            // Gönderenin ilk bakiyesini veri tabanından çek
            int gondereninBakiyesi = gonderenKisininEskiBakiyesi();


            int gondereninKalanBakiyesi = gondereninBakiyesi - gonderilenMiktar;
            Giris.bakiye = Convert.ToString(gondereninKalanBakiyesi);

            // Gönderenin, gönderdikten sonraki kalan bakiyesini güncelle
            gondereninBakiyesiniGuncelle(gondereninKalanBakiyesi);


            label1.Text = Convert.ToString(gondereninKalanBakiyesi);

            // Alıcının ilk bakiyesini veritabanindan çek
           int aliciBakiyesi= alicininEskiBakiyesiniVeriTabanındanCek(phone);

           int alicininGuncelBakiyesi = aliciBakiyesi + gonderilenMiktar;

            // Alicini yeni bakiyesini güncelle
           aliciYeniBakiyeGuncelle(phone, gonderilenMiktar, alicininGuncelBakiyesi);

        }

        private void txtAdGir_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (sender as TextBox);
            txt.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtSoyadGir_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (sender as TextBox);
            txt.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtTelefonNoGir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txtAdGir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSoyadGir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnKapa_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Giris giris = new Giris();
            giris.Show();
            this.Hide();
        }

        private void Transfer_MouseDown(object sender, MouseEventArgs e)
        {
            mov = true;
            movX = e.X;
            movY = e.Y;
        }

        private void Transfer_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void Transfer_MouseUp(object sender, MouseEventArgs e)
        {
            mov = false;
        }
    }
}
