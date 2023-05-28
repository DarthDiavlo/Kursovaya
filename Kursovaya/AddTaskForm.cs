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
        protected TextBox NameTask;
        protected TextBox Description;
        protected ComboBox Worker;
        protected DateTimePicker date;
        protected DateTimePicker dateSt;
        protected RadioButton Easy;
        protected RadioButton Medium;
        protected RadioButton Hard;
        protected Button ButAdd;
        Button ButOut;
        protected string Login;
        protected string NameProject;
        protected int level=0;
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
            head.BackColor= Color.FromArgb(40, 122, 113); 
            Controls.Add(head);

            //label
            Label hatter = new Label();
            hatter.Text = "Добавление задачи";
            hatter.Dock = DockStyle.Bottom;
            hatter.Location = new Point(1, 10);
            hatter.Size = new Size(410, 50);
            hatter.TextAlign = ContentAlignment.TopCenter;
            hatter.Font = new Font("Srbija Sans", 24);
            hatter.ForeColor = Color.White;
            head.Controls.Add(hatter);

            //name
            NameTask = new TextBox();
            NameTask.Text = "Введите название";
            NameTask.Location= new Point(50, 60);
            NameTask.Size = new Size(250,30);
            NameTask.MouseClick += Name_MouseClick;
            Controls.Add(NameTask);

            Label dat1= new Label();
            dat1.Text = "Введите дату начала";
            dat1.Location= new Point(50, 115);
            dat1.Size = new Size(250,20);
            dat1.Font = new Font("Srbija Sans", 10);
            Controls.Add(dat1);

            Label dat2 = new Label();
            dat2.Text = "Введите конечную дату";
            dat2.Location = new Point(50, 170);
            dat2.Size = new Size(250, 20);
            dat2.Font = new Font("Srbija Sans", 10);
            Controls.Add(dat2);

            // календарь
            dateSt = new DateTimePicker();
            dateSt.Location = new Point(50,140);
            dateSt.Size = new Size(250, 30);
            Controls.Add(dateSt);

            date = new DateTimePicker();
            date.Location = new Point(50, 195);
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
            Easy=new RadioButton();
            Easy.Location = new Point(100, 225);
            Easy.Size = new Size(200, 30);
            Easy.Text = "Легкая";
            Easy.Font = new Font("Srbija Sans", 15);
            Easy.MouseClick += Easy_CheckedChanged;
            Controls.Add(Easy);

            Medium = new RadioButton();
            Medium.Location = new Point(100, 260);
            Medium.Size = new Size(200, 30);
            Medium.Text = "Средняя";
            Medium.Font = new Font("Srbija Sans", 15);
            Medium.MouseClick += Medium_CheckedChanged;
            Controls.Add(Medium);

            Hard = new RadioButton();
            Hard.Location = new Point(100, 295);
            Hard.Size = new Size(200, 30);
            Hard.Text = "Сложная";
            Hard.Font = new Font("Srbija Sans", 16);
            Hard.MouseClick += Hard_CheckedChanged;
            Controls.Add(Hard);

            //описание
            Description= new TextBox();
            Description.Text = "Описание";
            Description.Multiline= true;
            Description.Location = new Point(50,330);
            Description.Size= new Size(250, 120);
            Controls.Add(Description);

            //кнопка назад
            ButOut = new Button();
            ButOut.Text = "Назад";
            ButOut.Location = new Point(40, ClientSize.Height - 60);
            ButOut.Size = new Size(100,30);
            ButOut.Font = new Font("Srbija Sans", 12);
            Controls.Add(ButOut);
            ButOut.MouseClick += ButOut_MouseClick;

            //кнопка добавить
            ButAdd = new Button();
            ButAdd.Text = "Добавить";
            ButAdd.Location = new Point(200, ClientSize.Height - 60);
            ButAdd.Size = new Size(100, 30);
            ButAdd.Font = new Font("Srbija Sans", 12);
            Controls.Add(ButAdd);
            ButAdd.MouseClick += ButAdd_MouseClick;
        }
        private void WorkerAdd(ComboBox Worker)
        {
            StreamReader sr = new StreamReader($@"users\{Login}\worker.txt");
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
        protected virtual void ButOut_MouseClick(object sender, MouseEventArgs e)
        {
            MenuForm menu = new MenuForm(Login);
            menu.Show();
            this.Close();
        }
        private void Name_MouseClick(object sender, MouseEventArgs e)
        {
            NameTask.Clear();
        }

        private void ButAdd_MouseClick(object sender, MouseEventArgs e)
        {
            if (date.Value >= dateSt.Value)
            {
                Tasks Newtask = new Tasks(NameTask.Text,Worker.Text,level,Description.Text,date.Value,dateSt.Value);
                MasTasks Listtas = new MasTasks();
                Listtas= Desserialized(Listtas);
                Listtas.ListTasks.Add(Newtask);
                Serealize(Listtas);
                MessageBox.Show("Задача добавлена");
            }
            else
            {
                MessageBox.Show("Начальная дата не может быть позже конечной",
                "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void Serealize(MasTasks Listtas)
        {
            MasTasks newList = Listtas;
            XmlSerializer formatter = new XmlSerializer(typeof(MasTasks));            
            using (FileStream fs = new FileStream($@"users\{Login}\project\#{NameProject}.txt",FileMode.Open))
            {
                formatter.Serialize(fs, newList);
            }
        }
        private MasTasks Desserialized(MasTasks Listtast)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(MasTasks));
            using (FileStream fs = new FileStream($@"users\{Login}\project\#{NameProject}.txt", FileMode.Open))
            {
                if (new FileInfo($@"users\{Login}\project\#{NameProject}.txt").Length != 0)
                {
                    Listtast = (MasTasks)formatter.Deserialize(fs);
                }
            }
            return Listtast;
        }
    }               
}
