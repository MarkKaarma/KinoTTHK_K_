﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KinoTTHK_K
{
    public partial class StartK : Form
    {
        public int i=0, j=0;
        int[] rida_list;
        int[] koht_list;
        ListBox listbox1;
        ComboBox listCinema;


        SqlConnection connect = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =|DataDirectory|KinoDB.mdf; Integrated Security = True");

        SqlCommand cmd;
        SqlDataAdapter adapter;

        public StartK()
        {
            connect.Open();
            adapter = new SqlDataAdapter("SELECT * FROM Seat", connect);
            DataTable seat_tabel = new DataTable();
            this.Text = "Appolo"; // Измененение название приложения *только космитическое
            adapter.Fill(seat_tabel);
            listbox1 = new ListBox();
            listbox1.Font = new Font("Calibri", 14);
            listbox1.Location = new Point(59, 150);

            foreach (DataRow row in seat_tabel.Rows)
            {
                listbox1.Items.Add(row["SeatNimetus"]);
            }
            rida_list = new int[seat_tabel.Rows.Count];
            koht_list = new int[seat_tabel.Rows.Count];
            int a = 0;
            foreach (DataRow row in seat_tabel.Rows)
            {
                rida_list[a] = (int)row["Rida"];
                koht_list[a] = (int)row["Koht"];
                a = a + 1;
            }

            connect.Close();

            listbox1.MouseClick += listBox1_SelectedIndexChanged;
            this.Controls.Add(listbox1);

            listCinema = new ComboBox();
            listCinema.Font = new Font("Calibri", 14);
            listCinema.Location = new Point(59, 10);
            listCinema.Items.Add("Film1");
            listCinema.Items.Add("Film2");
            listCinema.Items.Add("Film3");
            listCinema.Items.Add("Film4");
            listCinema.Text = "Vali filmi";
            listCinema.MouseClick += listCinema_SelectedIndexChanged;
            this.Controls.Add(listCinema);
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            i = rida_list[listbox1.SelectedIndex];
            j = koht_list[listbox1.SelectedIndex];

            Roomid roomid = new Roomid(i, j);
            roomid.Show();
        }

        private void listCinema_SelectedIndexChanged(object sender, EventArgs e) 
        {
            
        }

    }
}
