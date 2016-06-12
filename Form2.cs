using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;


namespace ProjektChaszOJPII
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            var message = new MailMessage();
            message.From = new MailAddress("aplikacja.organizer@gmail.com", "Aplikacja Organizer");
            try
            {
                message.To.Add(new MailAddress(textBoxTo.Text));
            }
            catch
            {
                MessageBox.Show("Nieprawidłowy format adresu e-mail odbiorcy.");
                return;
            }

            if (String.IsNullOrEmpty(textBoxSubject.Text))
            {
                if (MessageBox.Show("Czy na pewno chcesz wysłać wiadomość bez tematu?", "Temat", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                    DialogResult.No) return;
            }
            message.Subject = textBoxSubject.Text;
            message.Body = textBoxBody.Text;
   
            var smtp = new SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("aplikacja.organizer@gmail.com", "TestAplikacji123");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            try
            {
                smtp.Send(message);
                MessageBox.Show("Wiadomość została wysłana poprawnie!");
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Błąd wysyłania wiadomości!"+ex);
            }
           
            
        }
    }
}
