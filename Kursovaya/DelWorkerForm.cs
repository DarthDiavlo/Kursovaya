﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Kursovaya
{
    public partial class DelWorkerForm : AddWorkerForm
    { 
        string NameWor;
        public DelWorkerForm(string name, string login) : base(login)
        {
            InitializeComponent();
            NameWor = name;
            DataEntry();
            AddBut.Text = "Изменить";
            OutBut.Text = "Удалить";
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void DataEntry()
        {
            NameText.Text = NameWor;
            StreamReader sr = new StreamReader($"users/{Login}/worker.txt");
            string line;
            string[] masproject;
            while ((line = sr.ReadLine()) != null)
            {
                masproject = line.Split('*');
                if (masproject[0] == NameWor)
                {
                    for (int i = 0; i < ChooseProject.Items.Count; i++)
                    {
                        for (int j = 1; j < masproject.Length; j++)
                        {
                            string buf = ChooseProject.Items[i].ToString();
                            if (buf == masproject[j])
                            {
                                ChooseProject.SetItemChecked(i, true);
                            }
                        }

                    }
                    break;
                }
            }
            sr.Close();
        }

        override internal void AddBut_MouseClick(object sender, MouseEventArgs e)
        {
            StreamReader sr = new StreamReader($"users/{Login}/worker.txt");
            string line;
            string[] masproject;
            int index=0;
            while ((line = sr.ReadLine()) != null)
            {
                masproject = line.Split('*');
                if (masproject[0] == NameWor)
                {
                    break;
                }
                index++;
            }            
            sr.Close();
            string work = NameText.Text;
            foreach (string it in ChooseProject.CheckedItems)
            {
                work += $"*{it}";
            }
            RewriteLine(index,work);
            MenuForm menu = new MenuForm(Login);
            menu.Show();
            MessageBox.Show("Сотрудник изменён");
            this.Close();
        }
        private void RewriteLine(int lineIndex, string newValue)
        {
            int i = 0;
            string path = $"users/{Login}/worker.txt";
            string tempPath = path + ".txt";
            using (StreamReader sr = new StreamReader(path)) // читаем
            using (StreamWriter sw = new StreamWriter(tempPath, true, Encoding.UTF8)) // и сразу же пишем во временный файл
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (lineIndex == i)
                    {
                        sw.WriteLine(newValue);
                    }
                    else
                    {
                        sw.WriteLine(line);                                                
                    }
                    i++;
                }
            }
            File.Delete(path); // удаляем старый файл
            File.Move(tempPath, path); // переименовываем временный файл
        }
        override internal void OutBut_MouseClick(object sender, MouseEventArgs e)
        {
            StreamReader sr = new StreamReader($"users/{Login}/worker.txt");
            string line;
            string[] masproject;
            int index = 0;
            while ((line = sr.ReadLine()) != null)
            {
                masproject = line.Split('*');
                if (masproject[0] == NameWor)
                {
                    break;
                }
                index++;
            }
            sr.Close();
            string test = null;
            RewriteLine(index, test);
            MenuForm menu= new MenuForm(Login);
            menu.Show();
            MessageBox.Show("Сотрудник удалён");
            this.Close();
            
        }
    }
}
