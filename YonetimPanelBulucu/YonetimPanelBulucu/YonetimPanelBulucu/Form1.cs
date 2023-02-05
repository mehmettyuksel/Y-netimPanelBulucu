using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;

namespace YonetimPanelBulucu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Thread işlem;
        void Tarama()
        {
            int kactane = listBox1.Items.Count;
            for (int i = 0; i < kactane; i++)
            {
                try
                {
                    HttpWebRequest istek = (HttpWebRequest)HttpWebRequest.Create(textBox1.Text + listBox1.Items[i].ToString());
                    HttpWebResponse cevap = (HttpWebResponse)istek.GetResponse();
                    string durum = cevap.StatusCode.ToString();
                    if (durum == "OK")
                    {
                        listBox2.Items.Add(listBox1.Items[i].ToString() + "BAŞARILI");
                        label2.Text = listBox1.Items[i].ToString();
                        label2.ForeColor = Color.Green;
                    }
                    else
                    {
                        listBox2.Items.Add(listBox1.Items[i].ToString() + "BAŞARISIZ");
                    }
                }
                catch 
                {
                    listBox2.Items.Add(listBox1.Items[i].ToString() + "BAŞARISIZ");
                }
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader oku = new StreamReader(Application.StartupPath + @"/Panel.txt");
            string metin = oku.ReadLine();
            while(metin!=null)
            {
                listBox1.Items.Add(metin);
                metin = oku.ReadLine();
            }
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            işlem = new Thread(new ThreadStart(Tarama));
            işlem.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            işlem.Abort();
        }
    }
}
