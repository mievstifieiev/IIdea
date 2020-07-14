using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
