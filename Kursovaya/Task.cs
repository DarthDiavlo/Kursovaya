using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    [Serializable]
    public class MasTasks
    {
        public List<Tasks> ListTasks = new List<Tasks>();
    }
    [Serializable] public  class Tasks
    {
        public string Name;
        public string worker;
        public int level;
        public string Description;
        public DateTime date;
        public Tasks()
        {
        }
        public Tasks(string name, string worker, int level, string description, DateTime date)
        {
            Name = name;
            this.worker = worker;
            this.level = level;
            Description = description;
            this.date = date;
        }
        public string Writer()
        {
            return ($"{this.Name}; {this.worker}; {this.date}");
        }
    }
}
