using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ProjektChaszOJPII
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            
            taskList = new List<Task>();

            XDocument xml = XDocument.Load("Task.xml");

            taskList = (
                from task in xml.Root.Elements("zadanie")
                select new Task(
                    task.Attribute("body").Value
                    )
                ).ToList<Task>();

            listBox1.DataSource = taskList;
            listBox1.DisplayMember = "Body";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxAdd.Text))
            {
                Task task1 = new Task();
                task1.Body = textBoxAdd.Text;
                taskList.Add(task1);

                listBox1.DataSource = null;
                listBox1.DataSource = taskList;
                listBox1.DisplayMember = "Body";

                textBoxAdd.Clear();

            }
            else {
                MessageBox.Show("Podaj zadanie do dodania!");
                return;
            }

            
            
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index>=0)
            {
                taskList.RemoveAt(index);
                listBox1.DataSource = null;
                listBox1.DataSource = taskList;
                listBox1.DisplayMember = "Body";
            }
            else
            {
                MessageBox.Show("Nie masz już zadań do usunięcia!");
            }
            
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            XDocument xml = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("Lista zadań"),
                new XElement("zadanie",
                    from task in taskList
                    orderby task.Body
                    select new XElement("zadanie",
                        new XAttribute("body", task.Body)
                        )
                    )
                );

            xml.Save("Task.xml");
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            labelClock.Text = currentTime.ToLongTimeString();
            labelDate.Text = currentTime.ToShortDateString();
        }
    }
}
