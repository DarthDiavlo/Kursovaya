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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kursovaya
{
    public partial class AddWorkerForm : Form
    {
        public string Login;
        int y = 0;
        public AddWorkerForm(string login)
        {
            InitializeComponent();
            Login= login;
            addProject(login);
        }
        private void addProject(string Login)
        {
            foreach (string file in Directory.EnumerateFiles($"../../users/{Login}/project", "*", SearchOption.AllDirectories))
            {
                string name = file.Split('#')[1];
                ChooseProject.Items.Add($"{name.Substring(0, name.Length - 4)}");
            }
        }

        private void Name_Click(object sender, EventArgs e)
        {
            NameText.Clear();
        }

        virtual internal void OutBut_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        virtual internal void AddBut_MouseClick(object sender, MouseEventArgs e)
        {
            string work = NameText.Text;
            foreach(string it in ChooseProject.CheckedItems)
            {
                work+= $"*{it}";
            }
            using (StreamWriter sw = new StreamWriter($"../../users/{Login}/worker.txt", true,Encoding.Default))
            {
                sw.WriteLine(work);                
            }            
            MessageBox.Show("успешно");
        }

    }
}
