using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace Proje
{
    public partial class Kayit : Form
    {
        public static SqlConnection baglantı = new SqlConnection("Data Source=.\\SQLEXPRESS; Initial Catalog = Bank; Integrated Security=TRUE");

        public Kayit()
        {
            InitializeComponent();

        }

        bool mov;
        int movX, movY;

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && e.KeyChar !=8)
            {
                e.Handled = true;
            }

        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private bool telefonNumarasiKayitliMi(string phone)
        {
            SqlCommand command = new SqlCommand("Select * from Member WHERE phone_number=@phone", baglantı);
            command.Parameters.AddWithValue("@phone", phone);
            baglantı.Open();

            SqlDataReader oku = command.ExecuteReader();

            bool result; 

            if (oku.Read())
            {
                result = true;
            } else
            {
                result = false;
            }
            baglantı.Close();
            
            return result;


        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            var entities = new BankEntities();
            

            var dialogResult = MessageBox.Show("Kaydetmek istiyor musun?", "Müşteri Kayıt İşlemi", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)

            {
                if (
                      txtName.Text == "" || txtSurname.Text == "" || txtPhoneNumber.Text == "" || txtPassword.Text == "" || txtAmount.Text == "" ||
                     txtName.Text == String.Empty || txtSurname.Text == String.Empty || txtPhoneNumber.Text == String.Empty || txtPassword.Text == String.Empty || txtAmount.Text == String.Empty
)
                {
                    

                    MessageBox.Show("Tüm alanlar doldurulmalıdır.", "Boş Alan Hatası");
                }
                else {
                    if (txtPhoneNumber.Text.Length == 11)
                    {

                        if (telefonNumarasiKayitliMi(txtPhoneNumber.Text))
                        {
                            MessageBox.Show(txtPhoneNumber.Text + " nolu telefon numarası ile daha once kayıt yapıldı.", "Uyarı");

                        }
                        else
                        {
                            entities.Member.Add(new Member
                            {
                                name = txtName.Text,
                                surname = txtSurname.Text,
                                phone_number = txtPhoneNumber.Text,
                                amount = Convert.ToDecimal(txtAmount.Text),
                                password = txtPassword.Text,
                                create_date = DateTime.Now
                            });

                            entities.SaveChanges();

                            MessageBox.Show("Kayıt İşlemi Başarılı.");
                            Giris input = new Giris();
                            input.Show();
                            this.Hide();
                        }

                        

                    }
                    else {
                        MessageBox.Show("Telefon numarasi 11 haneli olmali.", "Uyarı");
                    }
                
                }
            }
            else
            {
                MessageBox.Show("Kayıt İşlemi Başarısız.");
            }
        }
        

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {
           

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

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }

        private void cds(object sender, EventArgs e)
        {

        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Kayıt_Load(object sender, EventArgs e)
        {

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }

        private void Kayit_MouseDown(object sender, MouseEventArgs e)
        {
            mov = true;
            movX = e.X;
            movY = e.Y;
        }

        private void Kayit_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void Kayit_MouseUp(object sender, MouseEventArgs e)
        {
            mov = false;

        }
    }
}
