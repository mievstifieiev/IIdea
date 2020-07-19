using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;


namespace IIdeaApp
{
    public partial class FormAuth : Form
    {
        public FormAuth()
        {
            //инициализация формы
            InitializeComponent();
            
        }

        private void buttLocal_Click(object sender, EventArgs e)
        {
            LocWork locWork = new LocWork(this); //создам новую форму
            locWork.Show();
            this.Hide();//прячем первую форму
        }

        private void butSignUp_Click(object sender, EventArgs e)
        {
            if (tbName.Text =="")
            {
                MessageBox.Show("Invalid user name.");
                return;
            }
            string address = "https://myprjmera.herokuapp.com/api/Conections";
            Conection conection = new Conection(tbName.Text, tbPass.Text);
            List<Conection> conections = new List<Conection>();
            conections = conection.Http_GET(address);
            foreach (var item in conections)
            {
                if (item.Name == conection.Name)
                {
                    MessageBox.Show("This name already exists.");
                    return;
                }
            }
            string data = JsonSerializer.Serialize<Conection>(conection);
            string res = conection.Http_post(address, data);
            if (res.Length > 0)
            {
                MessageBox.Show("Account created");
            }
        }

        private void buttSignIn_Click(object sender, EventArgs e)
        {
            string address = "https://myprjmera.herokuapp.com/api/Conections";
            List<Conection> conections = new List<Conection>();
            Conection conection = new Conection();
            conections = conection.Http_GET(address);
            for (int i = 0; i < conections.Count; i++)
            {
                if ((conections[i].Name == tbName.Text)&&(conections[i].Password == tbPass.Text))
                {
                    conection = conections[i];
                }
            }
            LocWork locWork = new LocWork(this, conection, address); //создам новую форму
            locWork.Show();
            this.Hide();
        }
    }
}
