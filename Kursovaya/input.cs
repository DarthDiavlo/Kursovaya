using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovaya
{
    public partial class input : Form
    {       
        public input()
        {
            InitializeComponent();
            check2.Hide();
            StartPosition= FormStartPosition.CenterScreen;
        }       
        private void textBox2_Click(object sender, EventArgs e)
        {
            Password.Clear();
            Password.UseSystemPasswordChar = true;
        }
        private void textBox1_Click(object sender, EventArgs e)
        {
            Login.Clear();
        }

        private void buttonClose_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }
        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            registrationForm registrationForm = new registrationForm();
            registrationForm.Show();
        }

        string check(string login,string passwond)
        {
            string filepath = @"users.txt";
            StreamReader sr = new StreamReader(filepath);
            string line;
            string[] mas = new string[2];
            while ((line = sr.ReadLine()) != null)
            {
                mas = line.Split(',');
                if (mas[0] == login)
                {
                    sr.Close();
                    return mas[1];
                }
            }
            sr.Close();
            return "0";
        }
        private  async void button_input_MouseClick(object sender, MouseEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Text;
            string pas = check(login, password);
            if (String.Equals(password,pas))
            {
                this.Hide();
                MenuForm Menu = new MenuForm(login);
                Menu.Show();
            }
            else
            {
                check2.Show();
                await Task.Delay(1000);
                check2.Hide();
            }
        }
        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left+= e.X-lastPoint.X;
                this.Top+= e.Y-lastPoint.Y;
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

    }
}
