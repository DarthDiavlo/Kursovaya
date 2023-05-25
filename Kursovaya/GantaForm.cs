using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Kursovaya
{
    public partial class GantaForm : Form
    {
        public GantaForm()
        {
            InitializeComponent();
            addrows();
            dataGridView1.ColumnHeadersHeight=200;
            dataGridView1.SelectionChanged+=dataGridView1_SelectionChanged;
            dataGridView1.CurrentCell = null;
        }

        private void addrows() 
        {
            dataGridView1.ClearSelection();
            int i = 0;
            MasTasks mt= Desserialized();
            addColumn(mt);
            foreach(Tasks t in mt.ListTasks)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].HeaderCell.Value = string.Format(t.Name, "0");
                i++;
            }
            paintingСells(mt);
        }
        private void addColumn(MasTasks mt)
        {
            int i= 0;
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
            List<DateTime> allDates= new List<DateTime>();
            for (DateTime date = masSt[0].Date; date <= mas[masSt.Length - 1].Date; date = date.AddDays(1))
            { allDates.Add(date); }
            foreach(DateTime t in allDates)
            {
                DataGridViewColumn column= new DataGridViewColumn();
                column.HeaderText = t.ToString("dd/MM");
                column.CellTemplate = new DataGridViewTextBoxCell();
                dataGridView1.Columns.Add(column);
            }
        }

        private void paintingСells(MasTasks mt)
        {
            Dictionary<string, List<string>> dict= new Dictionary<string, List<string>>();            
            foreach (Tasks t in mt.ListTasks)
            {
                List<string> list = new List<string>();
                for (DateTime date = t.dateSt.Date; date <= t.date.Date; date = date.AddDays(1))
                { list.Add(date.ToString("dd/MM")); }                
                dict.Add(t.Name,list);
            }
            Random random= new Random();
            Console.WriteLine(dataGridView1.Rows.Count);
            Console.WriteLine(dataGridView1.Columns.Count);
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                int k = 0;
                List<string> list = dict[dataGridView1.Rows[i].HeaderCell.Value.ToString()];                
                Color color = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {                     
                    if (list[k] == dataGridView1.Columns[j].HeaderCell.Value.ToString())
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = color;
                        k++;
                        if (k == dict[$"{dataGridView1.Rows[i].HeaderCell.Value}"].Count())
                        {
                            k--;
                        }
                    }
                }
            }
        }
        private Color GetRandomColor(Random random)
        {
            return Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
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
        private MasTasks Desserialized()
        {
            MasTasks Listtast = new MasTasks();
            XmlSerializer formatter = new XmlSerializer(typeof(MasTasks));
            using (FileStream fs = new FileStream($@"..\..\users\re\project\#test.txt", FileMode.Open))
            {
                if (new FileInfo($@"..\..\users\re\project\#test.txt").Length != 0)
                {
                    Listtast = (MasTasks)formatter.Deserialize(fs);
                }
            }
            return Listtast;
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (MouseButtons != System.Windows.Forms.MouseButtons.None)
                ((DataGridView)sender).CurrentCell = null;
        }
    }
}
