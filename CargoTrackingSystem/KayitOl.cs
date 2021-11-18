﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;

namespace CargoTrackingSystem
{
    public partial class KayitOl : Form
    {
        IFirebaseConfig ifare = new FirebaseConfig()
        {

            AuthSecret = "nyzY105BbyD0jrYGnjElZLT21YN0HndLcaxWToVk",
            BasePath = "https://cargotrackingsystem-b678d-default-rtdb.firebaseio.com/"

        };
        IFirebaseClient client;
        public KayitOl()
        {
            InitializeComponent();
        }

        private void KayitOl_Load(object sender, EventArgs e)
        {

            try
            {
                client = new FireSharp.FirebaseClient(ifare);
            }

            catch
            {
                MessageBox.Show("İnternet yok");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            #region Condition
            if (string.IsNullOrWhiteSpace(textBox1.Text) &&
               string.IsNullOrWhiteSpace(textBox2.Text) &&


               string.IsNullOrWhiteSpace(textBox1.Text) &&
               string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Gerekli yerleri doldurun");
                return;
            }
            #endregion

            Kullanicilar kullanici = new Kullanicilar()
            {
                KullaniciAdi = textBox1.Text,
                Sifre = textBox2.Text,
            };

            SetResponse set = client.Set("Kullanıcılar/" + textBox1.Text, kullanici);


            MessageBox.Show("Ok");
        }
    }
}