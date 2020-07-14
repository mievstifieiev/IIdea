using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IIdeaApp
{
    public partial class LocWork : Form
    {
        FormAuth Auth = new FormAuth();
        Project projectsLock;
        string prList = "";
        //List<int> counter = new List<int>();
        public LocWork(FormAuth auth)
        {
            InitializeComponent();
            Auth = auth; //передаём первую форму, чтобы суметь контролировать ее из новой формы
        }

        private void LocWork_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Auth.Close();//с закрытием этой формы, закрываем приложение
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //показываем управляющие объекты после нажатия кнопки в меню Файл
            tbAncestor.Visible = true;
            tbPoint.Visible = true;
            tbLink.Visible = true;
            btAdd.Visible = true;
            rtbList.Visible = true;
            btLink.Visible = true;
            
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            List<int> counter = new List<int>();
            if (tbAncestor.TextLength==0)
            {
                projectsLock = new Project(tbPoint.Text, tbAncestor.Text);
            }
            else
            {
                if(prList.IndexOf(tbAncestor.Text)>-1)
                {
                    projectsLock.PartAdd(tbAncestor.Text, tbPoint.Text, Convert.ToInt32(tbLink.Text));

                }
                else
                {
                    MessageBox.Show("An ancestor with this name was not found!");
                }
            }
            projectsLock.levels.Clear();
            prList = projectsLock.WritePrList("", counter, projectsLock.levels);

            rtbList.Text = prList;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            GraphForm graphForm = new GraphForm(projectsLock); //создам новую форму
            graphForm.Show();
        }
    }
}
