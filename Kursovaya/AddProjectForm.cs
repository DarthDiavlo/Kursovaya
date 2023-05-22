using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovaya
{
    public partial class AddProjectForm : Form
    {
        Panel hat;
        TextBox name;
        string login;
        public AddProjectForm(string login)
        {
            InitializeComponent();
            this.login = login;
        }

        private void AddProjectForm_Load(object sender, EventArgs e)
        {
            hat = new Panel();
            hat.Dock = DockStyle.Top;
            hat.Size = new Size(410, 50);
            hat.BackColor = Color.Blue;
            Controls.Add(hat);

            //
            Label hatter = new Label();
            hatter.Text = "Добавление проекта";
            hatter.Dock = DockStyle.Top;
            hatter.Size = new Size(410, 50);
            hatter.TextAlign = ContentAlignment.TopCenter;
            hatter.Font = new Font("Times New Roman", 28);
            hat.Controls.Add(hatter);

            //name
            name = new TextBox();
            name.Location = new Point(ClientSize.Width / 4, ClientSize.Height / 2);
            name.Size = new Size(200, 40);
            name.Text = "Ведите имя проекта";
            name.Click += Name_Click;
            Controls.Add(name);



            //but out
            Button ButOut = new Button();
            ButOut.Text = "Назад";
            ButOut.Location = new Point(20, 190);
            ButOut.Size = new Size(120, 30);
            Controls.Add(ButOut);
            ButOut.MouseClick += ButOut_MouseClick;

            //but add
            Button ButAdd = new Button();
            ButAdd.Text = "Add";
            ButAdd.Location = new Point(220, 190);
            ButAdd.Size = new Size(120, 30);
            ButAdd.MouseClick += ButAdd_MouseClick;
            Controls.Add(ButAdd);
        }
        private void Name_Click(object sender, EventArgs e)
        {
            name.Clear();
        }
        private void ButOut_MouseClick(object sender, MouseEventArgs e)
        {
            MenuForm menu = new MenuForm(login);
            menu.Show();
            this.Close();
        }
        private void ButAdd_MouseClick(object sender, MouseEventArgs e)
        {
            System.IO.File.Create($@"..\..\users\{login}\project\#{name.Text}.txt").Close();
            MenuForm menu = new MenuForm(login);
            menu.Show();
            this.Close();
        }
    }
}
