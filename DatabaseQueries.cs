using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class DatabaseQueries
    {
        //COMBOBOXLARIN İÇİNİ DOLDURMAK İÇİN
        //Yazarlar
        public string yazarlar = "SELECT * FROM yazarlar";
        public string yazarId = "yazar_id";
        //Yayınevleri
        public string yayinevleri = "SELECT * FROM yayinevleri";
        public string yayineviId = "yayinevi_id";
        //Kategoriler
        public string kategoriler = "SELECT * FROM kategoriler";
        public string kategoriId = "kategori_id";
        //Diller
        public string diller = "SELECT * FROM diller";
        public string dilId = "dil_id";
        //Hamur Tipleri
        public string hamurTipleri = "SELECT * FROM hamur_tipleri";
        public string hamurTipiId = "hamur_tipi_id";
        //Cilt Tipleri
        public string ciltTipleri = "SELECT * FROM cilt_tipleri";
        public string ciltTipiId = "cilt_tipi_id";
        //Çevirmenler
        public string cevirmenler = "SELECT * FROM cevirmenler";
        public string cevirmenId = "cevirmen_id";
        //Kitaplar 
        public string kitaplar = "SELECT * FROM kitaplar";
        public string kitapId = "kitap_id";
        //Uyeler
        public string uyeler = "SELECT * FROM uyeler";
        public string uyeId = "TC_no";
        //COMBOBOXLAR İÇİN OLAN KISIM BİTTİ 



       
    }
}
