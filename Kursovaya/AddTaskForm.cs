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
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Kursovaya
{
    public partial class AddTaskForm : Form
    {
        Panel head;
        TextBox NameTask;
        TextBox Description;
        ComboBox Worker;
        DateTimePicker date;
        string Login;
        string NameProject;
        int level=0;
        public AddTaskForm(string nameProject, string login)
        {
            InitializeComponent();
            NameProject = nameProject;
            Login = login;
        }

        private void AddTaskForm_Load(object sender, EventArgs e)
        {
            //head
            head= new Panel();
            head.Dock= DockStyle.Top;
            head.Size = new Size(1, 50);
            head.BackColor= Color.Black;
            Controls.Add(head);

            //label
            Label hatter = new Label();
            hatter.Text = "Добавление задачи";
            hatter.Dock = DockStyle.Bottom;
            hatter.Location = new Point(1, 10);
            hatter.Size = new Size(410, 50);
            hatter.TextAlign = ContentAlignment.TopCenter;
            hatter.Font = new Font("Times New Roman", 28);
            hatter.ForeColor = Color.White;
            head.Controls.Add(hatter);

            //name
            NameTask = new TextBox();
            NameTask.Text = "Введите название";
            NameTask.Location= new Point(50, 60);
            NameTask.Size = new Size(250,30);
            NameTask.MouseClick += Name_MouseClick;
            Controls.Add(NameTask);

            // календарь
            date= new DateTimePicker();
            date.Location = new Point(50,120);
            date.Size = new Size(250, 30);
            Controls.Add(date);

            //Сотрудник
            Worker = new ComboBox();
            Worker.Text = "Выбор сотрудника";
            Worker.Location = new Point(50, 90);
            Worker.Size = new Size(250, 30);
            WorkerAdd(Worker);
            Controls.Add(Worker);

            //сложность
            RadioButton Easy=new RadioButton();
            Easy.Location = new Point(100, 150);
            Easy.Size = new Size(200, 30);
            Easy.Text = "Легкая";
            Easy.Font = new Font("Times New Roman", 14);
            Easy.MouseClick += Easy_CheckedChanged;
            Controls.Add(Easy);

            RadioButton Medium = new RadioButton();
            Medium.Location = new Point(100, 180);
            Medium.Size = new Size(200, 30);
            Medium.Text = "Средняя";
            Medium.Font = new Font("Times New Roman", 14);
            Medium.MouseClick += Medium_CheckedChanged;
            Controls.Add(Medium);

            RadioButton Hard = new RadioButton();
            Hard.Location = new Point(100, 210);
            Hard.Size = new Size(200, 30);
            Hard.Text = "Сложная";
            Hard.Font = new Font("Times New Roman", 14);
            Hard.MouseClick += Hard_CheckedChanged;
            Controls.Add(Hard);

            //описание
            Description= new TextBox();
            Description.Text = "Описание";
            Description.Multiline= true;
            Description.Location = new Point(50,240);
            Description.Size= new Size(250, 120);
            Description.MouseClick += Description_MouseClick;
            Controls.Add(Description);

            //кнопка назад
            Button ButOut = new Button();
            ButOut.Text = "Назад";
            ButOut.Location = new Point(40, ClientSize.Height - 60);
            ButOut.Size = new Size(100,30);
            Controls.Add(ButOut);
            ButOut.MouseClick += ButOut_MouseClick;

            //кнопка добавить
            Button ButAdd = new Button();
            ButAdd.Text = "Добавить";
            ButAdd.Location = new Point(200, ClientSize.Height - 60);
            ButAdd.Size = new Size(100, 30);
            Controls.Add(ButAdd);
            ButAdd.MouseClick += ButAdd_MouseClick;
        }
        private void WorkerAdd(ComboBox Worker)
        {
            StreamReader sr = new StreamReader($@"..\..\users\{Login}\worker.txt");
            string line;
            string[] mas;
            while ((line = sr.ReadLine()) != null)
            {
                mas = line.Split('*');
                for(int i = 1; i < mas.Length; i++)
                {
                    if (mas[i] == NameProject)
                    {
                        Worker.Items.Add(mas[0]);
                    }
                }                
            }
            sr.Close();
        }
        private void Easy_CheckedChanged(object sender, MouseEventArgs e)
        {
            this.level = 1;
        }
        private void Medium_CheckedChanged(object sender, MouseEventArgs e)
        {
            this.level = 2;
        }
        private void Hard_CheckedChanged(object sender, MouseEventArgs e)
        {
            this.level = 3;
        }
        private void ButOut_MouseClick(object sender, MouseEventArgs e)
        {
            MenuForm menu = new MenuForm(Login);
            menu.Show();
            this.Close();
        }
        private void Name_MouseClick(object sender, MouseEventArgs e)
        {
            NameTask.Clear();
        }
        private void Description_MouseClick(object sender, MouseEventArgs e)
        {
            Description.Clear();
        }
        private void ButAdd_MouseClick(object sender, MouseEventArgs e)
        {
            Tasks Newtask = new Tasks(NameTask.Text,Worker.Text,level,Description.Text,date.Value);
            MasTasks Listtas = new MasTasks();
            Listtas= Desserialized(Listtas);
            Listtas.ListTasks.Add(Newtask);
            Serealize(Listtas);
            MenuForm menu = new MenuForm(Login);
            menu.Show();
            this.Close();
        }
        private void Serealize(MasTasks Listtas)
        {
            MasTasks newList = Listtas;
            XmlSerializer formatter = new XmlSerializer(typeof(MasTasks));            
            using (FileStream fs = new FileStream($@"..\..\users\{Login}\project\#{NameProject}.txt",FileMode.Open))
            {
                formatter.Serialize(fs, newList);
            }
        }
        private MasTasks Desserialized(MasTasks Listtast)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(MasTasks));
            using (FileStream fs = new FileStream($@"..\..\users\{Login}\project\#{NameProject}.txt", FileMode.Open))
            {
                if (new FileInfo($@"..\..\users\{Login}\project\#{NameProject}.txt").Length != 0)
                {
                    Listtast = (MasTasks)formatter.Deserialize(fs);
                }
            }
            return Listtast;
        }
    }               
}
