using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        MySqlConnection baglanti;
        int i;
        public Form1()
        {
            InitializeComponent();
        }
        DatabaseConnection veritabani = new DatabaseConnection();
        Form2 fm = new Form2();

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbGirisYap_Click(object sender, EventArgs e)
        {
            i = 0;
            veritabani.baglanti.Open();
            MySqlCommand cmd = veritabani.baglanti.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from kullanicilar " +
                "where kullanici_adi ='" + tbKullaniciAdi.Text + "' " +
                "and sifre='" + tbSifre.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());

            if (i == 0)
            {
                hata.Visible = true;
            }
            else
            {
                this.Hide();
                
                fm.Show();
                
            }
            veritabani.baglanti.Close();
        }

        private void lbKapat_Click(object sender, EventArgs e)
        {
            this.Close();
            fm.Close();
            
        }
    }
}
