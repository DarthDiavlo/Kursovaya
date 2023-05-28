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

namespace Kursovaya
{
    public partial class AddProjectForm : Form
    {
        string login;
        int index=0;
        public AddProjectForm(string login)
        {
            InitializeComponent();
            this.login = login;
            AddWorker();
        }
        private void ButOut_MouseClick(object sender, MouseEventArgs e)
        {
            MenuForm menu = new MenuForm(login);
            menu.Show();
            this.Close();
        }
        private void AddWorker()
        {
            StreamReader sr = new StreamReader($@"users\{login}\worker.txt");
            string line;
            string[] mas;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line)) {continue;}
                mas = line.Split('*');
                workercheck.Items.Add(mas[0]);
                index++;
            }
            sr.Close();
        }
        private void ButAdd_MouseClick(object sender, MouseEventArgs e)
        {
            File.Create($@"users\{login}\project\#{nameproject.Text}.txt").Close();
            int[] mas = new int[index];
            int indexnew = 0;
            int j=0;
            string line;
            string[] masline;
            foreach(string t in workercheck.CheckedItems)
            { 
                StreamReader sr = new StreamReader($@"users\{login}\worker.txt");
                while ((line = sr.ReadLine()) != null)
                {                    
                    masline = line.Split('*');
                    if (t == masline[0])
                    {
                        mas[j++] = indexnew;                        
                    }
                    indexnew++;
                }
                sr.Close();
            }
            rewrite(mas);

            MenuForm menu = new MenuForm(login);
            menu.Show();
            this.Close();
        }


        private void rewrite(int[] mas)
        {
            int i = 0;
            int j = 0;
            string path = $"users/{login}/worker.txt";
            string tempPath = path + ".txt";
            using (StreamReader sr = new StreamReader(path)) // читаем
            using (StreamWriter sw = new StreamWriter(tempPath)) // и сразу же пишем во временный файл
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (mas[j] == i)
                    {
                        sw.WriteLine(line+"*"+nameproject.Text);
                        j++;
                    }
                    else { sw.WriteLine(line); }                       
                    i++;
                }
            }
            File.Delete(path); // удаляем старый файл
            File.Move(tempPath, path); // переименовываем временный файл
        }
        

        private void nameproject_MouseClick(object sender, MouseEventArgs e)
        {
            nameproject.Clear();
        }
    }
}
