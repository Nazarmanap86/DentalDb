using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Randevu : Form
    {
        public Randevu()
        {
            InitializeComponent();
        }
        Connectionstring Mycon = new Connectionstring();
        private void fiilHasta()
        {
            SqlConnection baglanti = Mycon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select HAd from HastaTbl",baglanti);
            SqlDataReader rdr;
            rdr = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("HAd", typeof(string));
            dt.Load(rdr);
            RadCb.ValueMember = "HAd";
            RadCb.DataSource = dt;
            baglanti.Close();
        }
        Connectionstring MyCon = new Connectionstring();
        private void fiilTedavi()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select TAd from TedaviTbl", baglanti);
            SqlDataReader rdr;
            rdr = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TAd", typeof(string));
            dt.Load(rdr);
            RtedaviCb.ValueMember = "TAd";
            RtedaviCb.DisplayMember = "TAd";
            RtedaviCb.DataSource = dt;
            baglanti.Close();
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Randevu_Load(object sender, EventArgs e)
        {
            fiilHasta();
            uyeler();
            Reset();
        }
        void uyeler()
        {
            Hastalar Hs = new Hastalar();
            string guery = "select * from RandevuTbl";
            DataSet ds = Hs.ShowHasta(guery);
            RandevuDGV.DataSource = ds.Tables[0];
        }
        void filter()
        {
            Hastalar Hs = new Hastalar();
            string guery = "select * from RandevuTbl where Hasta like'%" + AraTb.Text+ "%'";
            DataSet ds = Hs.ShowHasta(guery);
            RandevuDGV.DataSource = ds.Tables[0];
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string query = "insert into RandevuTbl values ('" + RadCb.SelectedValue.ToString() + "','" + RtedaviCb.Text + "','" + Rtarih.Text.ToString()+ "','" +SaatCb.Text +"')";
            Hastalar Hs = new Hastalar();
            try
            {
                Hs.HastaEkle(query);
                MessageBox.Show("Randevu Başarıyla Eklendi");
                uyeler();
                Reset();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RtedaviCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        int key = 0;
        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Güncellenecek Randevuyu Seçiniz");
            }
            else
            {
                try
                {
                     string guery = "Update RandevuTbl set Hasta='" + RadCb.SelectedValue.ToString() + "',Tedavi='" + RtedaviCb.SelectedValue.ToString()  +"',RTarih='"+ Rtarih.Text + "',RSaat='" + SaatCb.SelectedIndex.ToString()+ "'where TId = " + key + ";";
                   
                    Hs.HastaGuncelle(guery);
                    MessageBox.Show("Randevu işlemi Başarıyla Güncellendi");
                    uyeler();
                    Reset();
                    key = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SaatCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RandevuDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RadCb.SelectedValue = RandevuDGV.SelectedRows[0].Cells[1].Value.ToString();
            RtedaviCb.SelectedValue = RandevuDGV.SelectedRows[0].Cells[2].Value.ToString();
            Rtarih.Text = RandevuDGV.SelectedRows[0].Cells[3].Value.ToString();
            SaatCb.Text = RandevuDGV.SelectedRows[0].Cells[4].Value.ToString();

            if (RadCb.SelectedIndex == -1)
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(RandevuDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        void Reset()
        {
            RadCb.SelectedIndex= -1;
            RtedaviCb.SelectedIndex= -1;
            Rtarih.Text = "";
            SaatCb.Text = "";

        }
        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Randevuyu Seçiniz");
            }
            else
            {
                try
                {
                    string guery = "Delete  from RandevuTbl where RId = " + key + "";
                    Hs.HastaSil(guery);
                    MessageBox.Show("Randevu İşlemi Başarıyla Silindi");
                    uyeler();
                    Reset();
                    key = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void RadCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            Anasayfa ana = new Anasayfa();
            ana.Show();
            this.Hide();
        }

        private void AraTb_TextChanged(object sender, EventArgs e)
        {
            filter();
        }
    }
}
