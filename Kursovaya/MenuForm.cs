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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;


namespace Kursovaya
{
    public partial class MenuForm : Form
    {
        public string Login;
        Panel menu;
        Panel desk;
        Panel hat;
        DataGridView ganta;
        public string NameProject;
        public MenuForm(string login)
        {
            InitializeComponent();
            Login = login;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            //menu
            menu = new Panel();
            menu.Dock = DockStyle.Left;
            menu.Size = new Size(300, 574);
            menu.BackColor = Color.FromArgb(40, 122, 113);
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
            
            PictureBox close=new PictureBox();
            close.Location=new Point(menu.Width-25, 7);
            close.Size = new Size(20,20);
            close.ImageLocation = @"images\close.png";
            close.SizeMode = PictureBoxSizeMode.StretchImage;
            close.MouseClick += Close_MouseClick;
            close.Cursor= Cursors.Hand;
            menu.Controls.Add(close);

            //login
            PictureBox pictureBox = new PictureBox();
            pictureBox.Location = new Point(20, 20);
            pictureBox.Size = new Size(50, 50);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.ImageLocation = @"images\user.png";
            menu.Controls.Add(pictureBox);

            //ник нейм пользователя
            Label Login = new Label();
            Login.Location = new Point(80, 30);
            Login.Size = new Size(150, 30);
            Login.Text = this.Login;
            Login.Font = new Font("Srbija Sans", 16);
            Login.ForeColor = Color.White;
            menu.Controls.Add(Login);

            //добавить проект
            Button add = new Button();
            add.Dock = DockStyle.Bottom;
            add.Size = new Size(150, 50);
            add.BackColor = Color.White;
            add.Text = "Добавить проект";
            add.Font = new Font("Srbija Sans", 16);
            add.TabIndex = 1;
            add.Cursor = Cursors.Hand;
            menu.Controls.Add(add);
            add.MouseClick += add_MouseClick;

            // мои задачи
            Button task = new Button();
            task.Location = new Point(20, 100);
            task.Size = new Size(260, 30);
            task.Text = "Мои задачи";
            task.BackColor = Color.White;
            task.Font = new Font("Srbija Sans", 10);
            task.Cursor = Cursors.Hand;
            task.MouseClick += MyTask_MouseClick;
            menu.Controls.Add(task);

            // отчёты
            /*Button reports = new Button();
            reports.Location = new Point(20, 140);
            reports.Size = new Size(260, 30);
            reports.Text = "отчёты";
            reports.BackColor = Color.White;
            menu.Controls.Add(reports);*/

            // команда
            Button team = new Button();
            team.Location = new Point(20, 140);
            team.Size = new Size(260, 30);
            team.Text = "команда";
            team.Font = new Font("Srbija Sans", 10);
            team.BackColor = Color.White;
            team.Cursor = Cursors.Hand;
            team.MouseClick += TeamAdd_MouseClick; 
            menu.Controls.Add(team);
            
            addButton(this.Login);
        }
        private void Close_MouseClick(object sender, MouseEventArgs e)
        {
           System.Windows.Forms.Application.Exit();
        }
        private void MyTask_MouseClick(object sender, MouseEventArgs e)
        {
            desk.Controls.Clear();
            MasTasks tasks= new MasTasks();
            foreach (string file in Directory.EnumerateFiles($"users/{Login}/project", "*", SearchOption.AllDirectories))
            {
                foreach(Tasks t in Desserialized(file, 0).ListTasks)
                {
                    if (t.worker=="Выбор сотрудника")
                    {
                        tasks.ListTasks.Add(t);
                    }
                }
            }
            AddRadioButton(tasks,desk);
        }
        private void TeamAdd_MouseClick(object sender, MouseEventArgs e)
        {
            desk.Controls.Clear();

            Button addTask = new Button();
            addTask.Location = new Point(500, 620);
            addTask.Size = new Size(300, 50);
            addTask.Text = "Добавить сотрудника";
            addTask.BackColor = Color.White;
            addTask.Font = new Font("Srbija Sans", 16);
            addTask.MouseClick += addWorker_MouseClick;
            desk.Controls.Add(addTask);

            StreamReader sr = new StreamReader($"users/{Login}/worker.txt");
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
                name.Font = new Font("Srbija Sans", 14);
                name.MouseClick += DeleteWorker_MouseClick;
                name.Cursor = Cursors.Hand;
                name.MouseHover += name_input_MouseHover;
                name.MouseLeave += name_MouseLeave;
                desk.Controls.Add(name);
            }
            sr.Close();
        }
        private void name_input_MouseHover(object sender, EventArgs e)
        {
            Label name = (Label)sender;
            name.ForeColor = Color.Blue;
        }
        private void name_MouseLeave(object sender, EventArgs e)
        {
            Label name = (Label)sender;
            name.ForeColor = Color.Black; // ставим черный цвет текста
        }
        private void DeleteWorker_MouseClick(object sender, MouseEventArgs e)
        {
            Label buf=(Label)sender;            
            DelWorkerForm form = new DelWorkerForm(buf.Text, Login);
            this.Close();
            form.Show();
        }
        private void addWorker_MouseClick(object sender, MouseEventArgs e)
        {
            AddWorkerForm AddWorker= new AddWorkerForm(Login);
            AddWorker.Show();
        }

        private void addButton(string Login)
        {
            int y=180;
            foreach (string file in Directory.EnumerateFiles($"users/{Login}/project", "*", SearchOption.AllDirectories))
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
                test.Cursor = Cursors.Hand;
                test.Font = new Font("Srbija Sans", 10);
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
            addTask.Location = new Point(500, 620);
            addTask.Size = new Size(300, 50);            
            addTask.Text = "Добавить задачу";
            addTask.Font = new Font("Srbija Sans", 16);
            addTask.BackColor = Color.White;
            addTask.MouseClick += addTask_MouseClick;
            desk.Controls.Add(addTask);

            Button saveTask = new Button();
            saveTask.Location = new Point(50, 620);
            saveTask.Size = new Size(300, 50);
            saveTask.Text = "Удалить проект";
            saveTask.Font=  new Font("Srbija Sans", 16);
            saveTask.BackColor = Color.White;
            saveTask.MouseClick += DelProject_MouseClick;
            desk.Controls.Add(saveTask);

            hat= new Panel();
            hat.Location = new Point(0, 50);
            hat.Size = new Size(ClientSize.Width - menu.Width, ClientSize.Height-100);
            hat.MouseMove += panel_MouseMove;
            hat.MouseDown += panel_MouseDown;
            desk.Controls.Add(hat);

            Button diagramma = new Button();
            diagramma.Location = new Point(50, 10);
            diagramma.Size = new Size(120, 30);
            diagramma.Text = "Диаграмма";
            diagramma.BackColor = Color.White;
            diagramma.Font = new Font("Srbija Sans", 12);
            diagramma.MouseClick += Diagramma_MouseClick;
            desk.Controls.Add(diagramma);
            
            Button spisok= new Button();
            spisok.Location = new Point(180, 10);
            spisok.Size = new Size(120, 30);
            spisok.Text = "Список";
            spisok.BackColor = Color.White;
            spisok.Font = new Font("Srbija Sans", 12);
            spisok.MouseClick += spisok_MouseClick;
            desk.Controls.Add(spisok);

            AddRadioButton(Desserialized(NameProject),hat);
        }
        private void spisok_MouseClick(object sender, MouseEventArgs e)
        {
            hat.Controls.Clear();
            AddRadioButton(Desserialized(NameProject),hat);
        }
        private void Diagramma_MouseClick(object sender, MouseEventArgs e)
        {
            hat.Controls.Clear();
            ganta= new DataGridView();
            ganta.AllowUserToAddRows = false;
            ganta.AllowUserToDeleteRows = false;
            ganta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ganta.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            ganta.BackgroundColor = SystemColors.ActiveCaption;
            ganta.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ganta.Dock = DockStyle.Fill;
            ganta.GridColor = Color.Black;
            ganta.BackgroundColor = Color.White; ganta.BorderStyle = BorderStyle.None;
            ganta.Location = new Point(0, 0);
            ganta.Name = "dataGridView1";
            ganta.ReadOnly = true;
            ganta.RowHeadersWidth = 200;
            ganta.RowTemplate.Height = 24;
            ganta.Size = new Size(hat.Width, hat.Height-70);
            hat.Controls.Add(ganta);
            ganta.ColumnHeadersHeight = 200;
            ganta.SelectionChanged += ganta_SelectionChanged;
            addrows(NameProject);
        }
        private void addrows(string NameProject)
        {
            ganta.ClearSelection();
            int i = 0;
            MasTasks mt = Desserialized(NameProject);
            addColumn(mt);
            ganta.DefaultCellStyle.Font = new Font("Srbija Sans", 32);
            foreach (Tasks t in mt.ListTasks)
            {
                ganta.Rows.Add();
                ganta.Rows[i].HeaderCell.Value = string.Format(t.Name, "0");
                i++;
            }
            paintingСells(mt);
        }
        private void addColumn(MasTasks mt)
        {
            int i = 0;
            DateTime[] mas = new DateTime[mt.ListTasks.Count()];
            DateTime[] masSt = new DateTime[mt.ListTasks.Count()];
            foreach (Tasks t in mt.ListTasks)
            {
                mas[i] = t.date.Date;
                masSt[i] = t.dateSt.Date;
                i++;
            }
            sorting(mas, mas.Length);
            sorting(masSt, masSt.Length);
            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = masSt[0].Date; date <= mas[masSt.Length - 1].Date; date = date.AddDays(1))
            { allDates.Add(date); }
            foreach (DateTime t in allDates)
            {
                DataGridViewColumn column = new DataGridViewColumn();
                column.HeaderText = t.ToString("dd/MM");
                column.CellTemplate = new DataGridViewTextBoxCell();
                ganta.Columns.Add(column);
            }
        }
        private void paintingСells(MasTasks mt)
        {
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            foreach (Tasks t in mt.ListTasks)
            {
                List<string> list = new List<string>();
                for (DateTime date = t.dateSt.Date; date <= t.date.Date; date = date.AddDays(1))
                { list.Add(date.ToString("dd/MM")); }
                dict.Add(t.Name, list);
            }
            Random random = new Random();
            for (int i = 0; i < ganta.Rows.Count; i++)
            {
                int k = 0;
                List<string> list = dict[ganta.Rows[i].HeaderCell.Value.ToString()];
                Color color = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                for (int j = 0; j < ganta.Columns.Count; j++)
                {
                    if (list[k] == ganta.Columns[j].HeaderCell.Value.ToString())
                    {
                        ganta.Rows[i].Cells[j].Style.BackColor = color;
                        k++;
                        if (k == dict[$"{ganta.Rows[i].HeaderCell.Value}"].Count())
                        {
                            k--;
                        }
                    }
                }
            }
        }
        private int AddPyramid(DateTime[] mas, int i, int N)
        {
            int imax;
            DateTime buf;
            if ((2 * i + 2) < N)
            {
                if (mas[2 * i + 1] < mas[2 * i + 2]) imax = 2 * i + 2;
                else imax = 2 * i + 1;
            }
            else imax = 2 * i + 1;
            if (imax >= N) return i;
            if (mas[i] < mas[imax])
            {
                buf = mas[i];
                mas[i] = mas[imax];
                mas[imax] = buf;
                if (imax < N / 2) i = imax;
            }
            return i;
        }
        private void sorting(DateTime[] mas, int len)
        {
            for (int i = len / 2 - 1; i >= 0; --i)
            {
                long prev_i = i;
                i = AddPyramid(mas, i, len);
                if (prev_i != i) ++i;
            }
            DateTime buf;
            for (int k = len - 1; k > 0; --k)
            {
                buf = mas[0];
                mas[0] = mas[k];
                mas[k] = buf;
                int i = 0, prev_i = -1;
                while (i != prev_i)
                {
                    prev_i = i;
                    i = AddPyramid(mas, i, k);
                }
            }
        }
        private void ganta_SelectionChanged(object sender, EventArgs e)
        {
            if (MouseButtons != System.Windows.Forms.MouseButtons.None)
                ((DataGridView)sender).CurrentCell = null;
        }

        private void AddRadioButton(MasTasks Listtast,Panel panel)
        {
            int y = 40;
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
                panel.Controls.Add(rb);
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
                File.Delete($@"users\{Login}\project\#{NameProject}.txt");
                MenuForm menu=new MenuForm(Login);
                this.Close();
                menu.Show();
            }                        
        }
        private void Serealize(MasTasks Listtas)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(MasTasks));
            File.Delete($@"users\{Login}\project\#{NameProject}.txt");
            using (FileStream fs = new FileStream($@"users\{Login}\project\#{NameProject}.txt", FileMode.OpenOrCreate,FileAccess.Write))
            {
                formatter.Serialize(fs, Listtas);
                MessageBox.Show("ok");
            }                     
        }
        private MasTasks Desserialized(string NameProject)
        {
            MasTasks Listtast=new MasTasks();
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
