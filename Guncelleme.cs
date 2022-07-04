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
    public partial class Guncelleme : Form
    {
        public Guncelleme()
        {
            InitializeComponent();
        }

        bool mov;
        int movX, movY;

        private void Guncelleme_Load(object sender, EventArgs e)
        {
            txtName.Text = Giris.name;
            txtSurname.Text = Giris.surname;
            txtPhoneNumber.Text = Giris.phone;
            txtPassword.Text = Giris.password;
        }
        public static SqlConnection baglantı = new SqlConnection("Data Source=.\\SQLEXPRESS; Initial Catalog = Bank; Integrated Security=TRUE");
        private void btnGeri_Click(object sender, EventArgs e)
        {
            Tercih tercih = new Tercih();
            tercih.Show();
            this.Hide();
        }
        

        

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string surname = txtSurname.Text;
            string phone = txtPhoneNumber.Text;
            string password = txtPassword.Text;

            
            baglantı.Open();
            SqlCommand command = new SqlCommand("update member set name=@name, surname=@surname, phone_number=@phone, password=@password WHERE id=@id", baglantı);
            command.Parameters.AddWithValue("@id", Giris.id);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@phone", phone);
            command.Parameters.AddWithValue("@password", password);

            command.ExecuteNonQuery();
            baglantı.Close();
            Giris.name = name;
            Giris.surname = surname;
            Giris.phone = phone;
            Giris.password = password;
            Giris.girisYapanAdSoyad = name + " " + surname;

            MessageBox.Show("Güncelleme işlemi başarı ile gerçekleştirildi");

             
            
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (sender as TextBox);
            txt.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtSurname_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (sender as TextBox);
            txt.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Giris giris = new Giris();
            giris.Show();
            this.Hide();
        }

        private void Guncelleme_MouseDown(object sender, MouseEventArgs e)
        {
            mov = true;
            movX = e.X;
            movY = e.Y;
        }

        private void Guncelleme_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }

        }

        private void Guncelleme_MouseUp(object sender, MouseEventArgs e)
        {
            mov = false;
        }
    }
}
