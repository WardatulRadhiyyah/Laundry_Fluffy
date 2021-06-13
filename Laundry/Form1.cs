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

namespace Laundry
{
    public partial class Form1 : Form
    {
        Pesanan pelanggan;
        public enum Mode { Insert, Edit }
        Mode mode;
        public Form1()
        {
            InitializeComponent();
            mode = Mode.Insert;
            btnsimpan.Text = "Simpan";
        }
        private void SimpanData()
        {
            if (tbnama.Text != "" && tbnohp.Text != "" && tbberat.Text != "" && tbjenispaket.Text != "" && tbharga.Text != "" && tbhargatotal.Text != "" && dateTimePicker2.Text != ""
                && dateTimePicker1.Text != "")
            {
                using (var db = new PesananModel())
                {
                    pelanggan = new Pesanan
                    {
                        Nama = tbnama.Text,
                        No_Hp = tbnohp.Text,
                        Berat_Cucian = Convert.ToInt32(tbberat.Text),
                        Jenis_Paket = tbjenispaket.Text,
                        Harga_Paket = Convert.ToDouble(tbharga.Text),
                        Harga_Total = Convert.ToDouble(tbhargatotal.Text),
                        Tanggal_Pemsanan = Convert.ToDateTime(dateTimePicker2.Text),
                        Tanggal_Pengambilan = Convert.ToDateTime(dateTimePicker1.Text)
                    };
                    db.Pesanans.Add(pelanggan);
                    db.SaveChanges();
                    MessageBox.Show("Pesanan Disimpan");
                    tbnama.Clear();
                    tbnohp.Clear();
                    tbberat.Clear();
                    tbjenispaket.Text = "";
                    tbharga.Clear();
                    tbhargatotal.Clear();
                }
            }
            else
            {
                MessageBox.Show("Nama, Nomor Hp, Berat Cucian, Jenis Paket, Tanggal Pemesanan harus diisi");
            }
        }

        public void hitungTotal()
        {
            Data_pemesanan data = new Data_pemesanan(tbnama.Text, tbnohp.Text, Convert.ToInt32(tbberat.Text), Convert.ToDateTime(dateTimePicker2.Text), tbjenispaket.Text);
            switch (tbjenispaket.Text)
            {
                case "Paket A":

                    Paket_A paketA = new Paket_A();
                    data.Totalbiaya = data.Berat * paketA.Hargapaket;
                    tbharga.Text = Convert.ToString(paketA.Hargapaket);
                    break;


                case "Paket B":
                    Paket_B paketB = new Paket_B();
                    data.Totalbiaya = data.Berat * paketB.Hargapaket;
                    tbharga.Text = Convert.ToString(paketB.Hargapaket);
                    break;

                case "Paket C":
                    Paket_C paketC = new Paket_C();
                    data.Totalbiaya = data.Berat * paketC.Hargapaket;
                    tbharga.Text = Convert.ToString(paketC.Hargapaket);
                    break;
            }
            tbhargatotal.Text = Convert.ToString(data.Totalbiaya);
        }
        public void hitungTanggal()
        {
            Data_pemesanan data = new Data_pemesanan(tbnama.Text, tbnohp.Text, Convert.ToInt32(tbberat.Text), Convert.ToDateTime(dateTimePicker2.Text), tbjenispaket.Text);
            switch (tbjenispaket.Text)
            {
                case "Paket A":
                    Paket_A paketA = new Paket_A();
                    data.Tanggalpengambilan = data.Tanggalpemesanan.AddDays(paketA.Durasi);
                    break;

                case "Paket B":
                    Paket_B paketB = new Paket_B();
                    data.Tanggalpengambilan = data.Tanggalpemesanan.AddDays(paketB.Durasi);
                    break;

                case "Paket C":
                    Paket_C paketC = new Paket_C();
                    data.Tanggalpengambilan = data.Tanggalpemesanan.AddDays(paketC.Durasi);
                    break;
            }
            dateTimePicker1.Text = Convert.ToString(data.Tanggalpengambilan);
        }

        

        private void button6_Click_1(object sender, EventArgs e)
        {
            lbpesanan.Items.Clear();
            groupBox1.Visible = false;
            groupBox2.Visible = true;

            using (var db = new PesananModel())
            {
                var query = from pelanggan in db.Pesanans
                            where pelanggan.Nama != ""
                            select pelanggan;
                foreach (var item in query)
                {
                    lbpesanan.Items.Add(item.Nama + "\t" + item.Jenis_Paket + "\t" + item.Berat_Cucian + "\t" + item.Tanggal_Pemsanan + "\t" + item.Tanggal_Pengambilan + "\t" + item.Harga_Total);

                }
            }
        }

        private void tbjenispaket_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Data_pemesanan data = new Data_pemesanan();
            hitungTotal();
            hitungTanggal();
        }

        private void Input_Click(object sender, EventArgs e)
        {
            tbnama.Text = "";
            tbnohp.Text = "";
            tbjenispaket.Text = "";
            tbhargatotal.Text = "";
            tbharga.Text = "";
            tbberat.Text = "";
            dateTimePicker1.Text = "";
            dateTimePicker2.Text = "";


            groupBox1.Visible = true;
            groupBox2.Visible = false;
        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            if (mode == Mode.Insert)
            {
                SimpanData();
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login masuk = new Login();
            masuk.Show();
            this.Close();
        }
    }
}



