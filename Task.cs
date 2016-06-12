using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektChaszOJPII
{
    public class Task
    {
        public string Body { get; set; }
        public Task(string value)
        {
            Body = value;
        }
        public Task()
        {
            Body = "";
        }


    }

}
