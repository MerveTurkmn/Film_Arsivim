using CefSharp.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Film_Arsivim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        //Data Source=LAPTOP-HLMQ9PKG;Initial Catalog=FilmArsivim;Integrated Security=True;
        SqlConnection baglanti =
            new SqlConnection(@"Data Source=LAPTOP-HLMQ9PKG;Initial Catalog=FilmArsivim;Integrated Security=True");
        void filmler()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select AD, KATEGORI,LINK   from TBLFILMLER", baglanti);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {


            filmler();
        }



        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLFILMLER (AD,KATEGORI,LINK) values (@P1,@P2,@P3) ", baglanti);
            komut.Parameters.AddWithValue("@P1", TxtFilmAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtKategori.Text);
            komut.Parameters.AddWithValue("@P3", TxtLink.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Film listenize eklenmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            filmler();

        }
        ChromiumWebBrowser chromiumWebBrowser;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {



            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            string link = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            ChromiumWebBrowser chromiumWebBrowser = new ChromiumWebBrowser(link);
            this.groupBox2.Controls.Add(chromiumWebBrowser);
            //chromiumWebBrowser.Dock = DockStyle.Fill;
            chromiumWebBrowser1.Load(link);

        }

        private void BtnHakkimizda_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu proje 28/11/2023 tarihinde kodlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnRenk_Click(object sender, EventArgs e)
        {
            //Color[] renkler = { Color.LawnGreen, Color.LightBlue, Color.Orange, Color.Pink, Color.SandyBrown, Color.Yellow, Color.Purple, Color.Violet, Color.Gray, Color.Honeydew };

            //Random rand = new Random();
            //int sayi = rand.Next(0, 10);

            //this.BackColor = renkler[sayi];
            Random rand = new Random();
            int red = rand.Next(0, 255);
            int green = rand.Next(0, 255);
            int blue = rand.Next(0, 255);

            this.BackColor = Color.FromArgb(red, green, blue);
        }

        private void BtnTamEkran_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            groupBox2.Size = new Size(1110, 1110);
            chromiumWebBrowser1.Size = new Size(this.ClientSize.Width, this.ClientSize.Height);
            groupBox2.Location = new Point(230, 60);
            groupBox3.Visible = false;
            pictureBox1.Location = new Point(1260, 5);
        }
    }
}
