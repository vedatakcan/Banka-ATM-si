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
    public partial class Giris : Form
    {
         public static SqlConnection baglantı = new SqlConnection("Data Source=.\\SQLEXPRESS; Initial Catalog = Bank; Integrated Security=TRUE");

        public Giris()
        {
            InitializeComponent();
        }

        bool mov;
        int movX, movY;

        public static string girisYapanAdSoyad, bakiye, id,name, surname, phone, password;

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void Giris_MouseDown(object sender, MouseEventArgs e)
        {
            mov = true;
            movX = e.X;
            movY = e.Y;
        }

        private void Giris_MouseMove(object sender, MouseEventArgs e)
        {

            if (mov)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }

        }

        private void Giris_MouseUp(object sender, MouseEventArgs e)
        {
            mov = false;

        }

        private void linkLabelKayit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Kayit kayit = new Kayit();
            kayit.Show();
            this.Hide();
        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {

        }

        bool isThere;

        private void btnInput_Click(object sender, EventArgs e)
        {


            string phone = txtPhoneNumber.Text;
            string password = txtPassword.Text;



           
            SqlCommand command = new SqlCommand("Select * from Member WHERE phone_number=@phone AND password= @password", baglantı);
            command.Parameters.AddWithValue("@phone", phone);
            command.Parameters.AddWithValue("@password", password);
             baglantı.Open();

            SqlDataReader oku = command.ExecuteReader();

            if (phone.Length == 11)
            {

                while (oku.Read())
            {
                Giris.girisYapanAdSoyad = oku["Name"].ToString() + " " + oku["Surname"].ToString();
                Giris.bakiye = oku["Amount"].ToString();
                    Giris.id = oku["id"].ToString();
                    Giris.name = oku["name"].ToString();
                    Giris.surname = oku["surname"].ToString();
                    Giris.phone = oku["phone_number"].ToString();
                    Giris.password = oku["Password"].ToString();


                    if (phone == oku["phone_number"].ToString().TrimEnd() && password == oku["password"].ToString().TrimEnd())
                {
                    isThere = true;
                    break;
                }
                else
                {
                    isThere = false;
                }
            }
            baglantı.Close();

            if (isThere)
            {
                MessageBox.Show("Giriş İşleminiz Başarılı.", "Program", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Tercih tercih = new Tercih();
                tercih.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş Denemesi.", "Program");
            }
            }
            else
            {
                MessageBox.Show("Telefon numarasi 11 haneli olmali.", "Uyarı");
            }
        }
        

        private void Giris_Load(object sender, EventArgs e)
        {

        }
    }
}
