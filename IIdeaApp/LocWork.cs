using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.IO;

namespace IIdeaApp
{
    public partial class LocWork : Form
    {
        FormAuth Auth = new FormAuth();
        public Project projectsLock;
        string prList = "";
        Conection con = new Conection();
        string url;
        //List<int> counter = new List<int>();
        public LocWork(FormAuth auth)
        {
            InitializeComponent();
            Auth = auth;
            con = null;
            url = null;//передаём первую форму, чтобы суметь контролировать ее из новой формы
        }

        public LocWork(FormAuth auth, Conection conection, string str)
        {
            InitializeComponent();
            con = conection; 
            Auth = auth; //передаём первую форму, чтобы суметь контролировать ее из новой формы
            url = str;
        }

        private void LocWork_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Auth.Close();//с закрытием этой формы, закрываем приложение
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (con!=null)
            {
                if (con.Projects.Length < con.Http_GET_id(url).Projects.Length)
                {
                    MessageBox.Show("Data from the server does not match data on the local device. "+
                    "Sync your data before making changes by clicking the 'Sync' button in the menu.");
                    return;
                }
            }
            List<int> counter = new List<int>();
            if (tbAncestor.TextLength==0)
            {
                projectsLock = new Project(tbPoint.Text, tbAncestor.Text);
            }
            else
            {
                if((prList.IndexOf(tbAncestor.Text)>-1)&&(tbLink.Text.Length>0))
                {
                    projectsLock.PartAdd(tbPoint.Text, tbAncestor.Text, Convert.ToInt32(tbLink.Text));

                }
                else
                {
                    MessageBox.Show("An ancestor with this name or id was not found!");
                }
            }
            projectsLock.levels.Clear();
            prList = projectsLock.WritePrList("", counter, projectsLock.levels);
            con.SerProj(projectsLock);
            rtbList.Text = prList;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            GraphForm graphForm = new GraphForm(projectsLock); //создам новую форму
            graphForm.Show();
        }

        private void btLink_Click(object sender, EventArgs e)
        {
            List<string> projInList = new List<string>();
            projectsLock.toJSONstring(projInList);
            string jsonString = JsonSerializer.Serialize(projInList);
            MessageBox.Show(jsonString);

            projInList = null;
            projInList = new List<string>();
            projInList = JsonSerializer.Deserialize<List<string>>(jsonString);
            projectsLock = new Project(projInList[0], projInList[1], Convert.ToInt32(projInList[2]), Convert.ToInt32(projInList[3]));
            int i = 4;
            while (i<projInList.Count)
            {
                projectsLock.PartAdd(projInList[i], projInList[i + 1], Convert.ToInt32(projInList[i + 2]), Convert.ToInt32(projInList[i+3]));
                i += 4;
            }
            List<int> counter = new List<int>();
            prList = projectsLock.WritePrList("", counter, projectsLock.levels);

            rtbList.Text = prList;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) //кнопка синхронизации с сервером
        {
            Conection conection = new Conection();
            conection = con.Http_GET_id(url);
            Project project = new Project();
            project = conection.DeserProj();
            List<int> counter = new List<int>();
            string str = project.WritePrList("", counter, project.levels);
            DialogResult result = MessageBox.Show(str, "Syncing will replace your current project with this one.", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                con = conection;
                projectsLock = project;
                prList = str;
                rtbList.Text = prList;
            }
        }

        private void btChangeStat_Click(object sender, EventArgs e)
        {
            if (con != null)
            {
                if (con.Projects.Length < con.Http_GET_id(url).Projects.Length)
                {
                    MessageBox.Show("Data from the server does not match data on the local device. " +
                    "Sync your data before making changes by clicking the 'Sync' button in the menu.");
                    return;
                }
            }
            List<int> counter = new List<int>();
            projectsLock.StatusChange(cbChange.Text, Convert.ToInt32(tbIdStat.Text));
            projectsLock.levels.Clear();
            prList = projectsLock.WritePrList("", counter, projectsLock.levels);
            con.SerProj(projectsLock);
            rtbList.Text = prList;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (con != null)
            {
                con.Http_put(url);
                if (con.Projects != con.Http_GET_id(url).Projects)
                {
                    MessageBox.Show("An error occurred! Failed to upload data to the server.");
                }
                else
                {
                    MessageBox.Show("The data was successfully uploaded!");
                }
            }
            else
            {
                MessageBox.Show("You cannot upload data because you are not logged in to the project account.");
            }
            
        }

        private void tbIdStat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && (e.KeyChar != 127)&& (e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }

        private void tbLink_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && (e.KeyChar != 127) && (e.KeyChar!=8))
            {
                e.Handled = true;
            }
        }
    }
}
