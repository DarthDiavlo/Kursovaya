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
using System.Xml.Serialization;

namespace Kursovaya
{
    public partial class WatchTask : AddTaskForm
    {
        Tasks task;
        public WatchTask(string nameProject, string login, Tasks task) : base(nameProject, login)
        {
            InitializeComponent();
            this.task = task;
        }

        private void WatchTask_Load(object sender, EventArgs e)
        {
            ButAdd.Dispose();
            MasTasks mt = Desserialized();

            foreach(Tasks t in mt.ListTasks)
            {
                if ((t.Name==task.Name)&&(t.worker==task.worker) && (t.level == task.level) && (t.date == task.date) && (t.dateSt == task.dateSt) && (t.Description == task.Description))
                {
                    Console.WriteLine("dsdsd");
                    NameTask.Text = t.Name;
                    Description.Text = t.Description;
                    Worker.Text = t.worker;
                    date.Value=t.date;
                    dateSt.Value=t.dateSt;
                    switch (t.level)
                    {
                        case 1:Easy.Select();break; 
                        case 2:Medium.Select(); break; 
                        case 3: Hard.Select(); break;
                        default: break;
                    }
                    break;
                }
            }
        }

        protected override void ButOut_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }
        private MasTasks Desserialized()
        {
            MasTasks Listtast = new MasTasks();
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
