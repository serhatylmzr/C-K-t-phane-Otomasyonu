using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace WindowsFormsApp1
{

    public partial class Form2 : System.Windows.Forms.Form
    {

        MySqlConnection baglanti;
        


        public Form2()
        {
            InitializeComponent();
           
        }

        DatabaseConnection veritabani = new DatabaseConnection();
        DatabaseQueries query = new DatabaseQueries();
        

        

        private void Form1_Load(object sender, EventArgs e)
        {   
           yazarlar();
           yayinevleri();
           kategoriler();
           diller();
           hamurTipleri();
           ciltTipleri();
           kitaplar();
           cevirmenler();
           uyeler();
           oduncKitaplar();
            maxIadeTarihi.Value = verilisTarihi.Value.AddDays(25);
            
        }
        public void listele(DataGridView dgv,string selectQuery)
        {
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, veritabani.baglanti);
            adapter.Fill(table);
            dgv.DataSource = table;
        }
        public void ara(DataGridView dgv, string searchQuery)
        {
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(searchQuery, veritabani.baglanti);
            adapter.Fill(table);
            dgv.DataSource = table;
        }
        
        //****************KİTAPLAR************
       
        void fillCombo(string selectQuery,string row0)
        {

            MySqlCommand command = new  MySqlCommand(selectQuery, veritabani.baglanti);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            

            if (row0 == "yazar_id")
            {
                
                cbYazarAdi.DisplayMember = "yazar_ad";
                cbYazarAdi.ValueMember = "yazar_id";
                cbYazarAdi.DataSource = dt;
                cbYazarAdi.SelectedIndex = 1;

            }
            else if (row0 == "yayinevi_id")
            {
                cbYayineviAdi.DisplayMember = "yayinevi_ad";
                cbYayineviAdi.ValueMember = "yayinevi_id";
                cbYayineviAdi.DataSource = dt;
            }
            else if (row0 == "kategori_id")
            {
                cbKategoriAdi.DisplayMember = "kategori_ad";
                cbKategoriAdi.ValueMember = "kategori_id";
                cbKategoriAdi.DataSource = dt;
            }
            else if (row0 == "dil_id")
            {
                cbDil.DisplayMember = "dil";
                cbDil.ValueMember = "dil_id";
                cbDil.DataSource = dt;
            }
            else if (row0 == "hamur_tipi_id")
            {
                cbHamurTipi.DisplayMember = "hamur_tipi";
                cbHamurTipi.ValueMember = "hamur_tipi_id";
                cbHamurTipi.DataSource = dt;
            }
            else if (row0 == "cilt_tipi_id")
            {
                cbCiltTipi.DisplayMember = "cilt_tipi";
                cbCiltTipi.ValueMember = "cilt_tipi_id";
                cbCiltTipi.DataSource = dt;

            }
            else if(row0 == "cevirmen_id")
            {
                cbCevirmenAdi.DisplayMember = "cevirmen_ad";
                cbCevirmenAdi.ValueMember = "cevirmen_id";
                cbCevirmenAdi.DataSource = dt;
            }
            else if(row0 == "kitap_id")
            {
                cbKitapAdi.DisplayMember = "kitap_ad";
                cbKitapAdi.ValueMember = "kitap_id";
                cbKitapAdi.DataSource = dt;
            }
            else if (row0 == "TC_no")
            {
                cbUyeAdi.DisplayMember = "uye_ad";
                cbUyeAdi.ValueMember = "TC_no";
                cbUyeAdi.DataSource = dt;
            }


        }
        //***********COMBOBOX MOUSE CLICKS*****************
        private void cbCevirmenAdi_MouseClick(object sender, MouseEventArgs e)
        {
            fillCombo(query.cevirmenler, query.cevirmenId);
        }

        private void cbCiltTipi_MouseClick(object sender, MouseEventArgs e)
        {
            fillCombo(query.ciltTipleri, query.ciltTipiId);
        }

        private void cbHamurTipi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbHamurTipi_MouseClick(object sender, MouseEventArgs e)
        {
            fillCombo(query.hamurTipleri, query.hamurTipiId);
        }

        private void cbDil_MouseClick(object sender, MouseEventArgs e)
        {
            fillCombo(query.diller, query.dilId);
        }

        private void cbKategoriAdi_MouseClick(object sender, MouseEventArgs e)
        {
            fillCombo(query.kategoriler, query.kategoriId);
        }

        private void cbYayineviAdi_MouseClick(object sender, MouseEventArgs e)
        {
            fillCombo(query.yayinevleri, query.yayineviId);
        }

        private void cbYazarAdi_MouseClick(object sender, MouseEventArgs e)
        {
            fillCombo(query.yazarlar, query.yazarId);
        }

        //***********************KİTAPLAR

        public void kitaplar()
        {

            string selectQuery = "SELECT kitap_id as 'Kitap No' ,kitap_ad as 'Kitap Adi',adet as 'Adedi',yazar_ad as 'Yazar Adi'," +
                "yayinevi_ad as 'Yayınevi',kategori_ad as 'Kategorisi'," +
                "baski_sayisi as 'Baski Sayisi',isbn as 'ISBN',cevirmen_ad as 'Çevirmen Adi'," +
                "dil as 'Dili',cilt_tipi as 'Cilt Tipi',hamur_tipi as 'Hamur Tipi'," +
                "sayfa_sayi as 'Sayfa Sayisi',basim_tarihi as 'Basim Tarihi'" +
                "from kitaplar t1 " +
                "inner join yazarlar t3 on t1.yazar_id = t3.yazar_id " +
                "inner join yayinevleri t4 on t1.yayinevi_id =t4.yayinevi_id " +
                "inner join kategoriler t5 on t1.kategori_id = t5.kategori_id " +
                "inner join cevirmenler t6 on t1.cevirmen_id = t6.cevirmen_id " +
                "inner join diller t7 on t1.dil_id = t7.dil_id " +
                "inner join cilt_tipleri t8 on t1.cilt_tipi_id = t8.cilt_tipi_id " +
                "inner join hamur_tipleri t9 on t1.hamur_tipi_id = t9.hamur_tipi_id ";
           listele(dGVKitaplar, selectQuery);
            
        }

        private void btKitapGuncelle_Click(object sender, EventArgs e)
        {
            if (tbKitapAdi.Text != "" && cbYazarAdi.Text != "Seçiniz" && cbYayineviAdi.Text != "Seçiniz"
               && cbKategoriAdi.Text != "Seçiniz" && cbDil.Text != "Seçiniz"
               && cbHamurTipi.Text != "Seçiniz" && cbCiltTipi.Text != "Seçiniz" && tbBaskiSayisi.Text != "")
            {

                string update = "update kitaplar set kitap_ad= '" + tbKitapAdi.Text + "',yazar_id='" + cbYazarAdi.SelectedValue + "'," +
                "yayinevi_id='" + cbYayineviAdi.SelectedValue + "'," +
                "baski_sayisi='" + tbBaskiSayisi.Text + "'," +
                "adet= '" + tbAdet.Text + "'," +
                "isbn='" + tbISBN.Text + "'," +
                "kategori_id='" + cbKategoriAdi.SelectedValue + "'," +
                "dil_id='" + cbDil.SelectedValue + "'," +
                "hamur_tipi_id='" + cbHamurTipi.SelectedValue + "'," +
                "cilt_tipi_id='" + cbCiltTipi.SelectedValue + "',cevirmen_id='" + cbCevirmenAdi.SelectedValue + "'," +
                "sayfa_sayi='" + tbSayfaSayisi.Text + "',basim_tarihi ='" + BasimTarihi.Text + "' where kitap_id =" + int.Parse(dGVKitaplar.CurrentRow.Cells[0].Value.ToString());
                MySqlCommand command = new MySqlCommand(update, veritabani.baglanti);
                executeMyQuery(update);
                kitaplar();
            }
            else
            {
                MessageBox.Show("Stün Seçmeden Güncelleme Yapmayınız");
            }

        }

        private void btKitapEkle_Click(object sender, EventArgs e)
        {
            
            if (tbKitapAdi.Text != "" && cbYazarAdi.Text != "Seçiniz" && cbYayineviAdi.Text != "Seçiniz" 
                && cbKategoriAdi.Text != "Seçiniz" && cbDil.Text != "Seçiniz" 
                && cbHamurTipi.Text != "Seçiniz" && cbCiltTipi.Text != "Seçiniz" && tbBaskiSayisi.Text != "")
            {
                string insertQuery = "INSERT INTO kitaplar(kitap_ad,yazar_id,yayinevi_id,baski_sayisi,isbn,kategori_id,cevirmen_id,adet,dil_id," +
                    "hamur_tipi_id,cilt_tipi_id,sayfa_sayi,basim_tarihi) " +
                    "VALUES('" + tbKitapAdi.Text + "','" + cbYazarAdi.SelectedValue + "'," +
                    "'" + cbYayineviAdi.SelectedValue + "'," +
                    "'" + tbBaskiSayisi.Text + "'," +
                    "'" + tbISBN.Text + "'," +
                    "'" + cbKategoriAdi.SelectedValue + "'," +
                    "'" + cbCevirmenAdi.SelectedValue + "'," +
                    "'" + tbAdet.Text + "','" + cbDil.SelectedValue + "'," +
                    "'" + cbHamurTipi.SelectedValue + "'," +
                    "'" + cbCiltTipi.SelectedValue + "'," +
                    "'" + tbSayfaSayisi.Text + "','" + BasimTarihi.Text + "')";
                executeMyQuery(insertQuery);
                kitaplar();
            } else
            {
                MessageBox.Show("Yıldızlı Alanlar Boş Geçilemez");
            }
        }

        private void btKitapSil_Click(object sender, EventArgs e)
        {
            string deleteQuery = "delete from kitaplar where kitap_id =" + int.Parse(dGVKitaplar.CurrentRow.Cells[0].Value.ToString());
            executeMyQuery(deleteQuery);
            kitaplar();
        }
        private void btKitapAra_Click(object sender, EventArgs e)
        {
            string value = "";

            if (cbKitapAra.SelectedIndex == 0)
            {
                value = "where  kitap_ad like '%" + tbKitapAra.Text + "%'";
}
            else if (cbKitapAra.SelectedIndex == 1)
            {
                value = "where yazar_ad like '%" + tbKitapAra.Text + "%'";
            }
            else if (cbKitapAra.SelectedIndex == 2)
            {
                value = "where yayinevi_ad like '%" + tbKitapAra.Text + "%'";
            }

          
            string searchQuery = "SELECT kitap_id as 'Kitap No' ,kitap_ad as 'Kitap Adı',adet as 'Adedi',yazar_ad as 'Yazar Adı'," +
                "yayinevi_ad as 'Yayınevi',kategori_ad as 'Kategorisi'," +
                "baski_sayisi as 'Baskı Sayısı',isbn as 'ISBN',cevirmen_ad as 'Çevirmen Adı'," +
                "dil as 'Dili',cilt_tipi as 'Cilt Tipi',hamur_tipi as 'Hamur Tipi'," +
                "sayfa_sayi as 'Sayfa Sayısı',basim_tarihi as 'Basım Tarihi'" +
                "from kitaplar t1 " +
                "inner join yazarlar t3 on t1.yazar_id = t3.yazar_id " +
                "inner join yayinevleri t4 on t1.yayinevi_id =t4.yayinevi_id " +
                "inner join kategoriler t5 on t1.kategori_id = t5.kategori_id " +
                "inner join cevirmenler t6 on t1.cevirmen_id = t6.cevirmen_id " +
                "inner join diller t7 on t1.dil_id = t7.dil_id " +
                "inner join cilt_tipleri t8 on t1.cilt_tipi_id = t8.cilt_tipi_id " +
                "inner join hamur_tipleri t9 on t1.hamur_tipi_id = t9.hamur_tipi_id " +
                value;

            ara(dGVKitaplar, searchQuery);
        }
        private void btPDFKitap_Click(object sender, EventArgs e)
        {
            exportPdf(dGVKitaplar, "kitaplar");
        }
        private void cbDil_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDil.Text == "Türkçe") { 
                cbCevirmenAdi.Enabled = false;
            }
            else
                cbCevirmenAdi.Enabled = true;
        }

        private void dGVKitaplar_MouseClick(object sender, MouseEventArgs e)
        {
            fillCombo(query.cevirmenler, query.cevirmenId);
            fillCombo(query.ciltTipleri, query.ciltTipiId);
            fillCombo(query.hamurTipleri, query.hamurTipiId);
            fillCombo(query.diller, query.dilId);
            fillCombo(query.kategoriler, query.kategoriId);
            fillCombo(query.yayinevleri, query.yayineviId);
            fillCombo(query.yazarlar, query.yazarId);
            tbKitapAdi.Text = dGVKitaplar.CurrentRow.Cells[1].Value.ToString();
            tbAdet.Text = dGVKitaplar.CurrentRow.Cells[2].Value.ToString();
            cbYazarAdi.Text = dGVKitaplar.CurrentRow.Cells[3].Value.ToString();
            cbYayineviAdi.Text = dGVKitaplar.CurrentRow.Cells[4].Value.ToString();
            cbKategoriAdi.Text = dGVKitaplar.CurrentRow.Cells[5].Value.ToString();
            tbBaskiSayisi.Text = dGVKitaplar.CurrentRow.Cells[6].Value.ToString();
            tbISBN.Text = dGVKitaplar.CurrentRow.Cells[7].Value.ToString();
            cbCevirmenAdi.Text = dGVKitaplar.CurrentRow.Cells[8].Value.ToString();
            cbDil.Text = dGVKitaplar.CurrentRow.Cells[9].Value.ToString();
            cbCiltTipi.Text = dGVKitaplar.CurrentRow.Cells[10].Value.ToString();
            cbHamurTipi.Text = dGVKitaplar.CurrentRow.Cells[11].Value.ToString();
            tbSayfaSayisi.Text = dGVKitaplar.CurrentRow.Cells[12].Value.ToString();
            BasimTarihi.Text = dGVKitaplar.CurrentRow.Cells[13].Value.ToString();
            


        }
        //******************BİTTİ*************
        //******YAZARLAR*******
        public void yazarlar()
        {
            string selectQuery = "SELECT yazar_id as 'Numara',yazar_ad as 'Yazar Adı' FROM yazarlar";
            listele(dGVYazarlar, selectQuery);
        }
        private void btYazarGuncelle_Click(object sender, EventArgs e)
        {
            if (tbYazarAdi.Text != "")
            {
                string update = "update yazarlar set yazar_ad= '" + tbYazarAdi.Text + "' where yazar_id =" + int.Parse(dGVYazarlar.CurrentRow.Cells[0].Value.ToString());
                MySqlCommand command = new MySqlCommand(update, veritabani.baglanti);
                executeMyQuery(update);
                yazarlar();
            } else
            {
                MessageBox.Show("Sütun Seçmeden Güncelleme Yapmayınız");
            }
            
        }

        private void btYazarEkle_Click(object sender, EventArgs e)
        {
            if (tbYazarAdi.Text != "")
            {
                string insertQuery = "INSERT INTO yazarlar(yazar_ad) VALUES('" + tbYazarAdi.Text + "')";
                executeMyQuery(insertQuery);
                yazarlar();
            } else
            {
                MessageBox.Show("Yazar Adı Boş Geçilemez");
            }
        }

        private void btYazarSil_Click(object sender, EventArgs e)
        {
            string deleteQuery = "delete from yazarlar where yazar_id =" + int.Parse(dGVYazarlar.CurrentRow.Cells[0].Value.ToString());
            executeMyQuery(deleteQuery);
            yazarlar();
        }
        
        private void tbYazarAra_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = "select yazar_id as 'Numara',yazar_ad as 'Yazar Adı' from yazarlar where  yazar_ad like '%" + tbYazarAra.Text + "%'";
            ara(dGVYazarlar, searchQuery);
        }
        private void dGVYazarlar_MouseClick(object sender, MouseEventArgs e)
        {
            tbYazarAdi.Text = dGVYazarlar.CurrentRow.Cells[1].Value.ToString();
        }
        //***********BİTTİ********

        //*********** YAYINEVLERİ *************
        public void yayinevleri()
        {
            string selectQuery = "SELECT yayinevi_id as 'Numara',yayinevi_ad as 'Yayınevi Adı' FROM yayinevleri";
            listele(dGVYayinevleri, selectQuery);
        }
        private void btYayineviGuncelle_Click(object sender, EventArgs e)
        {
            if (tbYayineviAdi.Text != "") 
            {
                string update = "update yayinevleri set yayinevi_ad= '" + tbYayineviAdi.Text + "' where yayinevi_id =" + int.Parse(dGVYayinevleri.CurrentRow.Cells[0].Value.ToString());
                MySqlCommand command = new MySqlCommand(update, veritabani.baglanti);
                executeMyQuery(update);
                yayinevleri();
            } else
            {
                MessageBox.Show("Sütun Seçmeden Güncelleme Yapmayınız");
            }
        }

        private void btYayineviEkle_Click(object sender, EventArgs e)
        {
            if (tbYayineviAdi.Text != "")
            {
                string insertQuery = "INSERT INTO yayinevleri(yayinevi_ad) VALUES('" + tbYayineviAdi.Text + "')";
                executeMyQuery(insertQuery);
                yayinevleri();
            } else
            {
                MessageBox.Show("Yayınevi Adı Boş Geçilemez");
            }
        }

        private void btYayineviSil_Click(object sender, EventArgs e)
        {
            string deleteQuery = "delete from yayinevleri where yayinevi_id =" + int.Parse(dGVYayinevleri.CurrentRow.Cells[0].Value.ToString());
            executeMyQuery(deleteQuery);
            yayinevleri();
        }
        
        private void tbYayineviAra_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = "select yayinevi_id as 'Numara',yayinevi_ad as 'Yayınevi Adı' from yayinevleri where  yayinevi_ad like '%" + tbYayineviAra.Text + "%'";
            ara(dGVYayinevleri, searchQuery);
        }

        private void dGVYayinevleri_MouseClick(object sender, MouseEventArgs e)
        {
            tbYayineviAdi.Text = dGVYayinevleri.CurrentRow.Cells[1].Value.ToString();
        }
        //***********BİTTİ********

        //**************KATEGORİLER******
        public void kategoriler()
        {
            string selectQuery = "SELECT kategori_id as 'Numara',kategori_ad as 'Kategori Adı' FROM kategoriler";
            listele(dGVKategoriler, selectQuery);
        }

        private void btKategoriGuncelle_Click(object sender, EventArgs e)
        {
            if (tbKategoriAdi.Text != "") 
            {
                
                string update = "update kategoriler set kategori_ad= '" + tbKategoriAdi.Text + "' where kategori_id =" + int.Parse(dGVKategoriler.CurrentRow.Cells[0].Value.ToString());
                MySqlCommand command = new MySqlCommand(update, veritabani.baglanti);
                executeMyQuery(update);
                kategoriler();
            }
            else
            {
                MessageBox.Show("Sütun seçmeden güncelleme yapmayınız");
            }
        }

        private void btKategoriEkle_Click(object sender, EventArgs e)
        {
            if (tbKategoriAdi.Text != "")
            {
                string insertQuery = "INSERT INTO kategoriler(kategori_ad) VALUES('" + tbKategoriAdi.Text + "')";
                executeMyQuery(insertQuery);
                kategoriler();
            } else
            {
                MessageBox.Show("Kategori Adı Boş Geçilemez");
            }
        }

        private void btKategoriSil_Click(object sender, EventArgs e)
        {
            string deleteQuery = "delete from kategoriler where kategori_id =" + int.Parse(dGVKategoriler.CurrentRow.Cells[0].Value.ToString());
            executeMyQuery(deleteQuery);
            kategoriler();
        }

       
        private void tbKategoriAra_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = "select kategori_id as 'Numara',kategori_ad as 'Kategori Adı' from kategoriler where  kategori_ad like '%" + tbKategoriAra.Text + "%'";
            ara(dGVKategoriler, searchQuery);
        }

        private void dGVKategoriler_MouseClick(object sender, MouseEventArgs e)
        {
            tbKategoriAdi.Text = dGVKategoriler.CurrentRow.Cells[1].Value.ToString();
        }

        //***********BİTTİ********


        //***********DİLLER********
        public void diller()
        {
            string selectQuery = "SELECT dil_id as 'Numara',dil as 'Dil ' FROM diller";
            listele(dGVDiller, selectQuery);
        }
        private void btDilGuncelle_Click(object sender, EventArgs e)
        {
            if (tbDil.Text != "") 
            {
                string update = "update diller set dil= '" + tbDil.Text + "' where dil_id =" + int.Parse(dGVDiller.CurrentRow.Cells[0].Value.ToString());
                MySqlCommand command = new MySqlCommand(update, veritabani.baglanti);
                executeMyQuery(update);
                diller();
            } else
                MessageBox.Show("Sütun seçmeden güncelleme yapmayınız");

        }

        private void btDilEkle_Click(object sender, EventArgs e)
        {
            if (tbDil.Text != "")
            {
                string insertQuery = "INSERT INTO diller(dil) VALUES('" + tbDil.Text + "')";
                executeMyQuery(insertQuery);
                diller();
            }
            else
                MessageBox.Show("Dil Boş Geçilemez");
        }

        private void btDilSil_Click(object sender, EventArgs e)
        {
            string deleteQuery = "delete from diller where dil_id =" + int.Parse(dGVDiller.CurrentRow.Cells[0].Value.ToString());
            executeMyQuery(deleteQuery);
            diller();
        }

       
        private void tbDilAra_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = "select dil_id as 'Numara',dil as 'Dil' from diller where  dil like '%" + tbDilAra.Text + "%'";
            ara(dGVDiller, searchQuery);
        }

        private void dGVDiller_MouseClick(object sender, MouseEventArgs e)
        {
            tbDil.Text = dGVDiller.CurrentRow.Cells[1].Value.ToString();
        }

        //***********BİTTİ********

        //***********HAMUR TİPLERİ********
        public void hamurTipleri()
        {
            string selectQuery = "SELECT hamur_tipi_id as 'Numara',hamur_tipi as 'Hamur Tipi ' FROM hamur_tipleri";
            listele(dGVHamurTipleri, selectQuery);
        }

        private void btHamurTipiGuncelle_Click(object sender, EventArgs e)
        {
            if (tbHamurTipi.Text != "") 
            {
                string update = "update hamur_tipleri set hamur_tipi= '" + tbHamurTipi.Text + "' where hamur_tipi_id =" + int.Parse(dGVHamurTipleri.CurrentRow.Cells[0].Value.ToString());
                MySqlCommand command = new MySqlCommand(update, veritabani.baglanti);
                executeMyQuery(update);
                hamurTipleri();
            } else
            {
                MessageBox.Show("Sütun seçmeden güncelleme yapmayınız");
            }
        }

        private void btHamurTipiEkle_Click(object sender, EventArgs e)
        {
            if (tbHamurTipi.Text != "")
            {
                string insertQuery = "INSERT INTO hamur_tipleri(hamur_tipi) VALUES('" + tbHamurTipi.Text + "')";
                executeMyQuery(insertQuery);
                hamurTipleri();
            }else
            {
                MessageBox.Show("Hamur Tipi Boş Geçilemez");
            }
        }

        private void btHamurTipiSil_Click(object sender, EventArgs e)
        {
            string deleteQuery = "delete from hamur_tipleri where hamur_tipi_id =" + int.Parse(dGVHamurTipleri.CurrentRow.Cells[0].Value.ToString());
            executeMyQuery(deleteQuery);
            hamurTipleri();
        }

      
        private void tbHamurTipiAra_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = "select hamur_tipi_id as 'Numara',hamur_tipi as 'Hamur Tipi' from hamur_tipleri where  hamur_tipi like '%" + tbHamurTipiAra.Text + "%'";
            ara(dGVHamurTipleri, searchQuery);
        }

        private void dGVHamurTipleri_MouseClick(object sender, MouseEventArgs e)
        {
            tbHamurTipi.Text = dGVHamurTipleri.CurrentRow.Cells[1].Value.ToString();
        }

        //***********BİTTİ********


        //***********CİLT TİPLERİ********
        public void ciltTipleri()
        {
            string selectQuery = "SELECT cilt_tipi_id as 'Numara',cilt_tipi as 'Cilt Tipi ' FROM cilt_tipleri";
            listele(dGVCiltTipleri, selectQuery);
        }
        private void btCiltTipiGuncelle_Click(object sender, EventArgs e)
        {
            if (tbCiltTipi.Text != "")
            {
                string update = "update cilt_tipleri set cilt_tipi= '" + tbCiltTipi.Text + "' where cilt_tipi_id =" + int.Parse(dGVCiltTipleri.CurrentRow.Cells[0].Value.ToString());
                MySqlCommand command = new MySqlCommand(update, veritabani.baglanti);
                executeMyQuery(update);
                ciltTipleri();
            } else
            {
                MessageBox.Show("Sütun Seçmeden GÜncelleme Yapmayınız");
            }
        }

        private void btCilTipiEkle_Click(object sender, EventArgs e)
        {
            if (tbCiltTipi.Text != "")
            {
                string insertQuery = "INSERT INTO cilt_tipleri(cilt_tipi) VALUES('" + tbCiltTipi.Text + "')";
                executeMyQuery(insertQuery);
                ciltTipleri();
            }
            else
                MessageBox.Show("Cilt Tipi Boş Geçilemez");
        }

        private void btCiltTipiSil_Click(object sender, EventArgs e)
        {
            string deleteQuery = "delete from cilt_tipleri where cilt_tipi_id =" + int.Parse(dGVCiltTipleri.CurrentRow.Cells[0].Value.ToString());
            executeMyQuery(deleteQuery);
            ciltTipleri();
        }
       
        private void tbCiltTipiAra_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = "select cilt_tipi_id as 'Numara',cilt_tipi as 'Cil Tipi' from cilt_tipleri where  cilt_tipi like '%" + tbCiltTipiAra.Text + "%'";
            ara(dGVCiltTipleri, searchQuery);
        }

        private void dGVCiltTipleri_MouseClick(object sender, MouseEventArgs e)
        {
            tbCiltTipi.Text = dGVCiltTipleri.CurrentRow.Cells[1].Value.ToString();
        }


        //***********BİTTİ********
        //***************ÇEVİRMENLER ******************

        public void cevirmenler() {
            string selectQuery = "SELECT cevirmen_id as 'Numara',cevirmen_ad as 'Çevirmen Adı ' FROM cevirmenler";
            listele(dGVCevirmenler, selectQuery);
        }
        private void btCevirmenGuncelle_Click(object sender, EventArgs e)
        {
            if (tbCevirmenAdi.Text != "")
            {
                string update = "update cevirmenler set cevirmen_ad= '" + tbCevirmenAdi.Text + "'" +
                    " where cevirmen_id =" + int.Parse(dGVCevirmenler.CurrentRow.Cells[0].Value.ToString());
                MySqlCommand command = new MySqlCommand(update, veritabani.baglanti);
                executeMyQuery(update);
                cevirmenler();
            }
            else
            {
                MessageBox.Show("Tablodan Sütun Seçmeden Güncelleme Yapmayınız");
            }
        }
        private void btCevirmenEkle_Click(object sender, EventArgs e)
        {
            if (tbCevirmenAdi.Text != "")
            {
                string insertQuery = "INSERT INTO cevirmenler(cevirmen_ad) VALUES('" + tbCevirmenAdi.Text + "')";
                executeMyQuery(insertQuery);
                cevirmenler();
            }
            else
            {
                MessageBox.Show("Çevirmen Adı Boş Geçilemez");
            }
        }
        private void btCevirmenSil_Click(object sender, EventArgs e)
        {

            string deleteQuery = "delete from cevirmenler where cevirmen_id =" + int.Parse(dGVCevirmenler.CurrentRow.Cells[0].Value.ToString());
            executeMyQuery(deleteQuery);
            cevirmenler();
        }
        private void tbCevirmenAra_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = "select cevirmen_id as 'Numara',cevirmen_ad as 'Çevirmen Adı' from cevirmenler where  cevirmen_ad like '%" + tbCevirmenAra.Text + "%'";
            ara(dGVCevirmenler, searchQuery);
        }
        private void dGVCevirmenler_MouseClick(object sender, MouseEventArgs e)
        {
            tbCevirmenAdi.Text = dGVCevirmenler.CurrentRow.Cells[1].Value.ToString();
        }
        //************BİTTİ****************************

        //*********************ÜYELER*********************
        public void uyeler()
        {
            string selectQuery = "SELECT TC_no as 'TC Kimlik No',uye_ad as 'Uye Adi '," +
                "cep_no as 'Cep Telefonu',email as 'E-posta Adresi',adres as 'Adresi',gec_getirme as 'Gec Getirme Sayisi' FROM uyeler";
            listele(dGVUyeler, selectQuery);
        }
        private void btUyeGuncelle_Click(object sender, EventArgs e)
        {
            if(tbUyeAdi.Text != "" && tbTelefonNo.Text != "" && tbAdres.Text != "") { 
            string update = "update uyeler set uye_ad= '" + tbUyeAdi.Text + "'," +
                "cep_no='" + tbTelefonNo.Text + "',email='" + tbEposta.Text + "'," +
                "adres='" + tbAdres.Text + "'  where TC_no =" + Int64.Parse(dGVUyeler.CurrentRow.Cells[0].Value.ToString());
            MySqlCommand command = new MySqlCommand(update, veritabani.baglanti);
            executeMyQuery(update);
            uyeler();
            }
            else
            {
                MessageBox.Show("Tablodan Stün Seçmeden Güncelleme Yapmayınız");
            }
        }

        private void btUyeEkle_Click(object sender, EventArgs e)
        {
            if(tbUyeAdi.Text != "" && tbTelefonNo.Text != "" && tbAdres.Text != "" && tbKimlikNo.Text != "") { 
            string insertQuery = "INSERT INTO uyeler(TC_no,uye_ad,cep_no,email,adres) VALUES('" + tbKimlikNo.Text + "'," +
                "'" + tbUyeAdi.Text + "'," +
                "'" + tbTelefonNo.Text + "'," +
                "'" + tbEposta.Text + "'," +
                "'" + tbAdres.Text + "')";
            executeMyQuery(insertQuery);
            uyeler();
            }
            else
            {
                MessageBox.Show("Yıldızlı Satırlar Boş Geçilemez");
            }
        }

        private void btUyeSil_Click(object sender, EventArgs e)
        {
            
            string deleteQuery = "delete from uyeler where TC_no =" + int.Parse(dGVUyeler.CurrentRow.Cells[0].Value.ToString());
            executeMyQuery(deleteQuery);
            uyeler();
            
           
        }

       
        private void tbUyeAra_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = "select TC_no as 'TC Kimlik No',uye_ad as 'Üye Adı'," +
               "cep_no as 'Cep Telefonu',email as 'E-posta Adresi',adres as 'Adresi'," +
               "gec_getirme as 'Geç Getirme Sayısı' from uyeler where  concat(TC_no,uye_ad,cep_no) like '%" + tbUyeAra.Text + "%'";
            ara(dGVUyeler, searchQuery);
        }
       

        private void btPDFUye_Click(object sender, EventArgs e)
        {
            exportPdf(dGVUyeler, "üyeler");
        }

        private void btKaraListele_Click(object sender, EventArgs e)
        {
            string KaraListeleQuery = "select distinct t1.TC_no as 'TC Kimlik No',uye_ad as 'Üye Adı'," +
                "cep_no as 'Cep Telefonu',email as 'E-posta Adresi',adres as 'Adresi',gec_getirme " +
                "from uyeler t1 inner join odunc_kitaplar t2 on t1.TC_no = t2.TC_no  " +
                "where gec_getirme > 2 or (DATE_ADD(max_iade_tarih, INTERVAL 7 DAY) < NOW() and t2.iade_tarih Is NULL) ";
            
            
            listele(dGVUyeler, KaraListeleQuery);
        }

        private void dGVUyeler_MouseClick(object sender, MouseEventArgs e)
        {
            tbUyeAdi.Text = dGVUyeler.CurrentRow.Cells[1].Value.ToString();
            tbTelefonNo.Text = dGVUyeler.CurrentRow.Cells[2].Value.ToString();
            tbAdres.Text = dGVUyeler.CurrentRow.Cells[4].Value.ToString();
            tbEposta.Text = dGVUyeler.CurrentRow.Cells[3].Value.ToString();
            
        }
        //*****************BİTTİ*****************************

        //*******************************ÖDÜNÇ KİTAP VER *************************
        private void cbKitapAdi_MouseClick(object sender, MouseEventArgs e)
        {
            fillCombo( query.kitaplar,query.kitapId);
        }
        private void cbUyeAdi_MouseClick(object sender, MouseEventArgs e)
        {
            fillCombo(query.uyeler, query.uyeId);
        }
        private void verilisTarihi_ValueChanged(object sender, EventArgs e)
        {
            maxIadeTarihi.Value = verilisTarihi.Value.AddDays(25);
        }

        public void oduncKitaplar()
        {

            string selectQuery = "SELECT  t1.odunc_id as 'Numara',t1.TC_no as ' Uye Kimlik No',uye_ad as 'Uye Adi',kitap_ad as 'Kitap Adi'," +
                "odunc_tarih as 'Verilis  Tarihi',max_iade_tarih as 'Maksimum Iade Tarihi'," +
                "iade_tarih as 'Iade Ettiği Tarih'" +
                "from odunc_kitaplar t1 " +
                "inner join kitaplar t2 on t1.kitap_id = t2.kitap_id  " +
                "inner join uyeler t3 on t1.TC_no = t3.TC_no";
            listele(dGVOduncKitaplar, selectQuery);
        }
        private void btOduncVer_Click(object sender, EventArgs e)
        {
            if (cbKitapAdi.Text != "Seçiniz" && cbUyeAdi.Text != "Seçiniz" )
            {
                string insertQuery = "INSERT INTO odunc_kitaplar(kitap_id,TC_no,odunc_tarih,max_iade_tarih) " +
                    "VALUES('" + cbKitapAdi.SelectedValue + "','" + cbUyeAdi.SelectedValue + "'," +
                     "'" + verilisTarihi.Text + "'," +
                     "'" + maxIadeTarihi.Text + "')";
                    executeMyQuery(insertQuery);
                    string updateAdetQuery = "update kitaplar set adet=(adet-1) where kitap_id='" + cbKitapAdi.SelectedValue + "'";
                    executeMyQuery(updateAdetQuery);
                    oduncKitaplar();
            } else
                MessageBox.Show("Yıldızlı Alanlar Boş Geçilemez");
        }

        private void btIadeAl_Click(object sender, EventArgs e)
        {
            if (cbKitapAdi.Text != "Seçiniz" && cbUyeAdi.Text != "Seçiniz")
            {
                string update = "update odunc_kitaplar set iade_tarih= '" + iadeTarihi.Text + "',durum=1" +
                           " where odunc_id =" + int.Parse(dGVOduncKitaplar.CurrentRow.Cells[0].Value.ToString());
                MySqlCommand command = new MySqlCommand(update, veritabani.baglanti);
                executeMyQuery(update);
                string updateAdetQuery = "update kitaplar set adet=(adet+1) where kitap_id='" + cbKitapAdi.SelectedValue + "'";
                executeMyQuery(updateAdetQuery);
                if (maxIadeTarihi.Value < iadeTarihi.Value)
                {
                    string updateGecGetirmeQuery = "update uyeler set gec_getirme=(gec_getirme+1) where TC_no='" + cbUyeAdi.SelectedValue + "'";
                    executeMyQuery(updateGecGetirmeQuery);
                }

                oduncKitaplar();
            } else
            {
                MessageBox.Show("Sütun Seçmeden İade Alma İşlemi Gerçekleştiremezsiniz");
            }
        }

        private void btOduncSil_Click(object sender, EventArgs e)
        {
            string deleteQuery = "delete from odunc_kitaplar where odunc_id =" + int.Parse(dGVOduncKitaplar.CurrentRow.Cells[0].Value.ToString());
            executeMyQuery(deleteQuery);
            oduncKitaplar();
        }
        private void tbOduncAra_TextChanged(object sender, EventArgs e)
        {
            
            string searchQuery = "SELECT  t1.odunc_id as 'Numara',t1.TC_no as ' Uye Kimlik No',uye_ad as 'Uye Adi',kitap_ad as 'Kitap Adi'," +
                "odunc_tarih as 'Veriliş  Tarihi',max_iade_tarih as 'Maksimum İade Tarihi'," +
                "iade_tarih as 'İade Ettiği Tarih'" +
                "from odunc_kitaplar t1 " +
                "inner join kitaplar t2 on t1.kitap_id = t2.kitap_id  " +
                "inner join uyeler t3 on t1.TC_no = t3.TC_no where  concat(t1.TC_no,uye_ad) like '%" + tbOduncAra.Text + "%'";
            ara(dGVOduncKitaplar, searchQuery);
        }
        private void btIadeEdilmeyenler_Click(object sender, EventArgs e)
        {
            string selectQuery = "SELECT  t1.odunc_id as 'Numara',t1.TC_no as ' Uye Kimlik No',uye_ad as 'Uye Adİ',kitap_ad as 'Kitap Adi'," +
                "odunc_tarih as 'VeriliS  Tarihi',max_iade_tarih as 'Maksimum Iade Tarihi'," +
                "iade_tarih as 'İade Ettigi Tarih'" +
                "from odunc_kitaplar t1 " +
                "inner join kitaplar t2 on t1.kitap_id = t2.kitap_id  " +
                "inner join uyeler t3 on t1.TC_no = t3.TC_no where iade_tarih IS null ";
            listele(dGVOduncKitaplar, selectQuery);
        }
        private void dGVOduncKitaplar_MouseClick(object sender, MouseEventArgs e)
        {
            fillCombo(query.kitaplar, query.kitapId);
            fillCombo(query.uyeler, query.uyeId);
            cbUyeAdi.Text = dGVOduncKitaplar.CurrentRow.Cells[2].Value.ToString();
            cbKitapAdi.Text = dGVOduncKitaplar.CurrentRow.Cells[3].Value.ToString();
            verilisTarihi.Text = dGVOduncKitaplar.CurrentRow.Cells[4].Value.ToString();
            maxIadeTarihi.Text = dGVOduncKitaplar.CurrentRow.Cells[5].Value.ToString();
            iadeTarihi.Text = dGVOduncKitaplar.CurrentRow.Cells[6].Value.ToString();
            

        }


        //****************BİTTİ*****************************************************

        //*************************** PDF OLUŞTURUCU ***********************************
        public void exportPdf(DataGridView dgw, string filename)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdftable = new PdfPTable(dgw.Columns.Count);
            pdftable.DefaultCell.Padding = 3;
            pdftable.WidthPercentage = 100;
            pdftable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdftable.DefaultCell.BorderWidth = 1;
            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
            //Add Header 
            foreach (DataGridViewColumn column in dgw.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdftable.AddCell(cell);

            }
            //Add datarow
            foreach (DataGridViewRow row in dgw.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    pdftable.AddCell(new Phrase(cell.Value.ToString(), text));
                }
            }
            var saveFiledialoge = new SaveFileDialog();
            saveFiledialoge.FileName = filename;
            saveFiledialoge.DefaultExt = ".pdf";
            if (saveFiledialoge.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFiledialoge.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    pdfdoc.Add(pdftable);
                    pdfdoc.Close();
                    stream.Close();
                }
            }
        }
        //****************BİTTİ*****************************************************



        //*******************VERİTABANI BAĞLANTI KONTROLÜ****************

        public void openConnection()
        {
            if(veritabani.baglanti.State == ConnectionState.Closed)
            {
                veritabani.baglanti.Open();
            }
        }
        public void closeConnection()
        {
            if(veritabani.baglanti.State == ConnectionState.Open)
            {
                veritabani.baglanti.Close();
            }
        }
        public void executeMyQuery(string query)
        {
            try
            {
                openConnection();
                MySqlCommand command = new MySqlCommand(query, veritabani.baglanti);
                if(command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("İşlem Başarılı");
                } else
                {
                    MessageBox.Show("İşlem Başarısız");
                }

            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                closeConnection();
            }
        }
        //***************************VERİTABANI KONTROLÜ BİTTİİ*****************


        private void tabPage7_Click(object sender, EventArgs e)
        {

        }
       

       

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        private void cbKitapAdi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void label19_Click(object sender, EventArgs e)
        {

        }

        

        private void dGVCevirmenler_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbCevirmenAdi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

       

        private void cbKategoriAdi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MaxIadeTarihi_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void btpdf_Click(object sender, EventArgs e)
        {
            exportPdf(dGVYazarlar, "yazarlar");
        }

        private void label56_Click(object sender, EventArgs e)
        {

        }


        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < Application.OpenForms.Count; ++i)
                if (Application.OpenForms[i] != this)
                    Application.OpenForms[i].Close();
        }

        private void tbKitapNo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
