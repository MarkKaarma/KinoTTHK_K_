using System;
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
    public partial class Roomid : Form
    {
        int i, j;
        Label[,] _arr;
        Button buy;
        //Timer tm;
        Label label1;


        SqlCommand cmd;
        SqlDataAdapter adapter;
        SqlConnection connect = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =|DataDirectory|KinoDB.mdf; Integrated Security = True");

        public Roomid(int i_, int j_)
        {
            InitializeComponent();
            _arr = new Label[i_, j_];
            this.AutoSize = true;
            this.Text = "Ap_polo_kino"; // Измененение название приложения *только космитическое
            for(i = 0; i < i_; i++) // Нужно для того, чтобы создасть места(кресла) для сеанса. 
            { 
                for (j=0; j < j_; j++)
                {
                    _arr[i, j] = new Label();
                    _arr[i, j].BackColor = Color.Green;
                    _arr[i, j].Text = " Koht" + (j + 1);
                    _arr[i, j].Size = new Size(50, 50);
                    _arr[i, j].BorderStyle = BorderStyle.Fixed3D;
                    _arr[i, j].Location = new Point(j * 50 + 50, i * 50);
                    this.Controls.Add(_arr[i, j]);
                    _arr[i, j].Tag = new int[] { i, j };
                    _arr[i, j].Click += new System.EventHandler(Form1_Click);

                    

                }
            }
            // Пишем сюда чтобы позже вызывать к примеру Label, Button и т.д.

            buy = new Button(); // Добавление кнопки. Дает возможность "купить" место и резервирует место
            buy.Text = "Osta";
            buy.Font = new Font("Calibri", 12);
            buy.AutoSize = true;
            buy.Location = new Point(30, i_ * 50 + 20);
            buy.Click += new System.EventHandler(Buy_Click);
            this.Controls.Add(buy);
        // Создается таймер
            tm = new Timer();
            tm.Tick += new EventHandler(tm_tick);
            tm.Interval = 1;
            tm.Start();

            label1 = new Label();
            this.Controls.Add(label1);
            label1.Text = "00:00";
            label1.AutoSize = true;
            label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            label1.Font = new Font("Calibri", 24);
            label1.BackColor = Color.SlateGray;
            label1.ForeColor = Color.Orange;
            label1.Location = new Point(25, i_ * 50 + 85);
            
        }



        private void Form1_Click(object sender, EventArgs e)
        {
            var label = (Label)sender; // Чтобы при выборе запоминал какую надпись выбрали
            var tag = (int[])label.Tag; // Определение координат надписей 
            if (_arr[tag[0], tag[1]].Text != "Kinni") // Если будет свободно, то сможет отобразить желтый цвет на месте
            {
                _arr[tag[0], tag[1]].Text = "Kinni"; 
                _arr[tag[0], tag[1]].BackColor = Color.Yellow;
            }
            else // Если будет занято, то выдаст MessageBox мол "Это место уже занято!" и надо выбрать свободную.
            {
                MessageBox.Show("Pilet rida:" + (tag[0] + 1) + " Koht:" + (tag[1] + 1) + " juba ostetud!");
            }
        }
        


        Timer tm = null;
        int startValue =  60 * 60 * 1; // Тут меняем цирфу и тогда таймер поменяется. К примеру: 1 - одна минута и т.д.

        private string Int2StringTime(int time)
        {
            int hours = (time - (time % (60 * 60))) / (60 * 60);
            int minutes = (time - time % 60) / 60 - hours * 60;
            return String.Format("{0:00}:{1:00}", hours, minutes);
        }
        private void tm_tick(object sender, EventArgs e)
        {
            if (startValue != 0)
            {
                label1.Text = Int2StringTime(startValue);
                startValue--;
            }
            else
            {
                (sender as Timer).Stop();
                (sender as Timer).Dispose();
                MessageBox.Show("Вы не успели купить билет. Попробуйте ещё раз");
                this.Close();
            }
        }

        

        private void Buy_Click(object sender, EventArgs e) // Ожидается в том, что при покупки места будет закрашено красным и невозможно другому купить такое же место
        {
            Email email = new Email();
            email.Show(this);
        }


    }
}

