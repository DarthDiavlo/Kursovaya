using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;


namespace Kursovaya
{
    public partial class MenuForm : Form
    {
        public string Login;
        Panel menu;
        Panel desk;
        public string NameProject;
        public MenuForm(string login)
        {
            InitializeComponent();
            Login = login;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            //menu
            menu = new Panel();
            menu.Dock = DockStyle.Left;
            menu.Size = new Size(300, 574);
            menu.BackColor = Color.Blue;
            menu.AutoScroll = true;
            menu.MouseMove += panel_MouseMove;
            menu.MouseDown += panel_MouseDown;
            Controls.Add(menu);

            //desk
            desk = new Panel();
            desk.Dock = DockStyle.Right;
            desk.Size = new Size(ClientSize.Width - menu.Width, ClientSize.Height);
            desk.AutoScroll = false;
            desk.MouseMove += panel_MouseMove;
            desk.MouseDown += panel_MouseDown;
            Controls.Add(desk);

            TabControl tb= new TabControl();
            tb.Dock = DockStyle.Right;
            tb.Size = new Size(10,10);
            tb.MouseMove += panel_MouseMove;
            tb.MouseDown += panel_MouseDown;
            desk.Controls.Add(tb);

            TabPage tabPage1 = new TabPage();
            tabPage1.Text = "tabPage1";
            tabPage1.Size = new System.Drawing.Size(256, 214);
            tabPage1.TabIndex = 0;

            tb.Controls.Add(tabPage1);

            //login
            PictureBox pictureBox = new PictureBox();
            pictureBox.Location = new Point(20, 20);
            pictureBox.Size = new Size(50, 50);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.ImageLocation = @"..\..\images\user.png";
            menu.Controls.Add(pictureBox);

            //ник нейм пользователя
            Label Login = new Label();
            Login.Location = new Point(80, 30);
            Login.Size = new Size(150, 30);
            Login.Text = this.Login;
            Login.Font = new Font("Times New Roman", 16);
            Login.ForeColor = Color.White;
            menu.Controls.Add(Login);

            //добавить проект
            Button add = new Button();
            add.Dock = DockStyle.Bottom;
            add.Size = new Size(150, 50);
            add.BackColor = Color.White;
            add.Text = "Добавить проект";
            add.TabIndex = 1;
            menu.Controls.Add(add);
            add.MouseClick += add_MouseClick;

            // мои задачи
            Button task = new Button();
            task.Location = new Point(20, 100);
            task.Size = new Size(260, 30);
            task.Text = "Мои задачи";
            task.BackColor = Color.White;
            task.MouseClick += MyTask_MouseClick;
            menu.Controls.Add(task);

            // отчёты
            Button reports = new Button();
            reports.Location = new Point(20, 140);
            reports.Size = new Size(260, 30);
            reports.Text = "отчёты";
            reports.BackColor = Color.White;
            menu.Controls.Add(reports);

            // команда
            Button team = new Button();
            team.Location = new Point(20, 180);
            team.Size = new Size(260, 30);
            team.Text = "команда";
            team.BackColor = Color.White;
            team.MouseClick += TeamAdd_MouseClick;
            menu.Controls.Add(team);

            addButton(this.Login);
        }
        private void MyTask_MouseClick(object sender, MouseEventArgs e)
        {
            desk.Controls.Clear();
            MasTasks tasks= new MasTasks();
            foreach (string file in Directory.EnumerateFiles($"../../users/{Login}/project", "*", SearchOption.AllDirectories))
            {
                foreach(Tasks t in Desserialized(file, 0).ListTasks)
                {
                    if (t.worker=="Выбор сотрудника")
                    {
                        tasks.ListTasks.Add(t);
                    }
                }
            }
            AddRadioButton(tasks);
        }
        private void TeamAdd_MouseClick(object sender, MouseEventArgs e)
        {
            desk.Controls.Clear();

            Button addTask = new Button();
            addTask.Location = new Point(400, 500);
            addTask.Size = new Size(200, 30);
            addTask.Text = "Добавить сотрудника";
            addTask.BackColor = Color.White;
            addTask.MouseClick += addWorker_MouseClick;
            desk.Controls.Add(addTask);


            StreamReader sr = new StreamReader($"../../users/{Login}/worker.txt");
            string line;
            string[] masname;
            int y = 20;
            while ((line=sr.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line)) { continue; }
                masname = line.Split('*');
                Label name= new Label();
                name.Location = new Point(50, y);
                name.Size = new Size(800, 20);
                if (y < 500)
                {
                    y += 25;
                }
                else
                {
                    y = 515;
                }
                name.Text = masname[0];
                name.Font = new Font("Times New Roman", 14);
                name.MouseClick += DeleteWorker_MouseClick;
                desk.Controls.Add(name);
            }
            sr.Close();
        }
        private void DeleteWorker_MouseClick(object sender, MouseEventArgs e)
        {
            Label buf=(Label)sender;
            DelWorkerForm form = new DelWorkerForm(buf.Text, Login);
            form.Show();
        }
        private void addWorker_MouseClick(object sender, MouseEventArgs e)
        {
            AddWorkerForm AddWorker= new AddWorkerForm(Login);
            AddWorker.Show();
        }

        private void addButton(string Login)
        {
            int y=220;
            foreach (string file in Directory.EnumerateFiles($"../../users/{Login}/project", "*", SearchOption.AllDirectories))
            {
                string name = file.Split('#')[1];
                Button test = new Button();
                
                test.Location = new Point(20, y);
                if (y < 500)
                {
                    y += 40;
                }
                else
                {
                    y = 515;
                }
                test.Size = new Size(260, 30);
                test.Text = $"{name.Substring(0, name.Length - 4)}";
                test.BackColor = Color.White;
                test.MouseClick += project_MouseClick;
                menu.Controls.Add(test);
            }
        }
        private void add_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
            AddProjectForm AddProject = new AddProjectForm(Login);
            AddProject.Show();
        }

        private void project_MouseClick(object sender, MouseEventArgs e)
        {
            desk.Controls.Clear();
            Button Buf = (Button)sender;
            NameProject = Buf.Text;

            Button addTask = new Button();
            addTask.Location = new Point(400, 500);
            addTask.Size = new Size(200, 30);
            addTask.Text = "Добавить задачу";
            addTask.BackColor = Color.White;
            addTask.MouseClick += addTask_MouseClick;
            desk.Controls.Add(addTask);

            Button saveTask = new Button();
            saveTask.Location = new Point(50, 500);
            saveTask.Size = new Size(200, 30);
            saveTask.Text = "Удалить проект";
            saveTask.BackColor = Color.White;
            saveTask.MouseClick += DelProject_MouseClick;
            desk.Controls.Add(saveTask);


            AddRadioButton(Desserialized(Buf.Text));
        }
        private void AddRadioButton(MasTasks Listtast)
        {
            int y = 20;
            foreach(Tasks t in Listtast.ListTasks)
            {
                RadioButton rb= new RadioButton();
                rb.Location = new Point(50,y);
                rb.Size = new Size(800,40);
                if (y < 500)
                {
                    y += 30;
                }
                else
                {
                    y = 515;
                }                
                rb.Text= t.Writer();
                switch (t.level)
                {
                    case 1:
                    rb.ForeColor = Color.Green;break;
                    case 2:
                    rb.ForeColor = Color.FromArgb(176, 171, 16);break;
                    case 3:
                    rb.ForeColor = Color.Red; break;
                    default:break;
                }
                rb.Font= new Font("Times New Roman", 14);
                rb.MouseClick += rb_MouseClick;
                desk.Controls.Add(rb);
            }
        }

        private void rb_MouseClick(object sender, MouseEventArgs e)
        {
            RadioButton Buf = (RadioButton)sender;
            MasTasks ListTask;
            ListTask = Desserialized(NameProject);
            string[] masstr = { "; " };
            string[] mas = Buf.Text.Split(masstr, StringSplitOptions.RemoveEmptyEntries);
            Tasks buffer= new Tasks();
            foreach (Tasks t in ListTask.ListTasks)
            {               
                if (String.Equals(t.Name, mas[0]) && String.Equals(t.worker, mas[1]) && String.Equals(t.date.ToString("dd/MM/yyyy"), mas[2]))
                {                    
                    buffer= t;                   
                    Buf.Dispose();
                }
            }
            ListTask.ListTasks.Remove(buffer);
            Serealize(ListTask);
        }
        private void DelProject_MouseClick(object sender, MouseEventArgs e)
        {            
            if (MessageBox.Show(
                "Вы точно хотите удалить проект",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                File.Delete($@"..\..\users\{Login}\project\#{NameProject}.txt");
                MenuForm menu=new MenuForm(Login);
                this.Close();
                menu.Show();
            }                        
        }
        private void Serealize(MasTasks Listtas)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(MasTasks));
            File.Delete($@"..\..\users\{Login}\project\#{NameProject}.txt");
            using (FileStream fs = new FileStream($@"..\..\users\{Login}\project\#{NameProject}.txt", FileMode.OpenOrCreate,FileAccess.Write))
            {
                formatter.Serialize(fs, Listtas);
                MessageBox.Show("ok");
            }                     
        }
        private MasTasks Desserialized(string NameProject)
        {
            MasTasks Listtast=new MasTasks();
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
        private MasTasks Desserialized(string path,int i)
        {
            MasTasks Listtast = new MasTasks();
            XmlSerializer formatter = new XmlSerializer(typeof(MasTasks));
            using (FileStream fs = new FileStream($@"{path}", FileMode.Open))
            {
                if (new FileInfo($@"{path}").Length != 0)
                {
                    Listtast = (MasTasks)formatter.Deserialize(fs);
                }
            }
            return Listtast;
        }
        private void addTask_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
            AddTaskForm AddTask = new AddTaskForm(NameProject,this.Login);
            AddTask.Show();
        }

        Point lastPoint;
        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

    }
}
