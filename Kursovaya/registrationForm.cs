using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovaya
{
    public partial class registrationForm : Form
    {
        public registrationForm()
        {
            InitializeComponent();
            check.Hide();
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void Password_MouseClick(object sender, MouseEventArgs e)
        {
            Password.Clear();
        }

        private void Login_MouseClick(object sender, MouseEventArgs e)
        {
            Login.Clear();
        }

        private void OutBut_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
            input input = new input();
            input.Show();
        }

        private async void RegistrationBut_MouseClick(object sender, MouseEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Text;

            string filepath = @"users.txt"; //путь к файлу csv
            StreamReader sr = new StreamReader(filepath);
            string line;
            string[] mas = new string[2];
            bool flag = false;
            while ((line = sr.ReadLine()) != null)
            {
                mas = line.Split(',');
                if (mas[0] == login)
                {
                    flag = true;
                }
            }
            sr.Close();
            if (!flag)
            {
                Directory.CreateDirectory($@"users\{login}");
                Directory.CreateDirectory($@"users\{login}\project");
                File.Create($@"users\{login}\worker.txt").Close();
                using (StreamWriter sw = new StreamWriter(filepath, true,
                Encoding.UTF8))
                {
                    sw.WriteLine($"{login},{password}");
                }
                MessageBox.Show("успешно");
            }
            else
            {
                check.Show();
                await Task.Delay(1000);
                check.Hide();
            }
        }
        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
    }
}
