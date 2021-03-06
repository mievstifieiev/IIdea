﻿using System;
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
                if (prList != "")
                {
                    if ((prList.IndexOf(tbAncestor.Text) > -1) && (tbLink.Text.Length > 0))
                    {
                        projectsLock.PartAdd(tbPoint.Text, tbAncestor.Text, Convert.ToInt32(tbLink.Text));

                    }
                    else
                    {
                        MessageBox.Show("An ancestor with this name or id was not found!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("To create a project, just fill in the 'Point's Name' field.");
                    return;
                }
            }
            projectsLock.levels.Clear();
            prList = projectsLock.WritePrList("", counter, projectsLock.levels);
            if (con!=null)
            {
                con.SerProj(projectsLock);
            }
            rtbList.Text = prList;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            GraphForm graphForm = new GraphForm(projectsLock); //создам новую форму
            graphForm.Show();
        }

        private void btLink_Click(object sender, EventArgs e)
        {
            
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) //кнопка синхронизации с сервером
        {
            if (con != null)
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
        }

        private void btChangeStat_Click(object sender, EventArgs e)
        {
            if (tbIdStat.Text != "")
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
                if (con != null)
                {
                    con.SerProj(projectsLock);
                }
                rtbList.Text = prList;
            }
            else
            {
                MessageBox.Show("Fill in the ID field.");
            }
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

        private void LocWork_Load(object sender, EventArgs e)
        {

        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (tbLink.Text!="")
            {
                Project project = projectsLock.Find(Convert.ToInt32(tbLink.Text));
                if (project == null)
                {
                    MessageBox.Show("This element does not exist.");
                }
                else
                {
                    DialogResult result = MessageBox.Show("This element and all its subtasks will be erased. Continue?", "Attention!", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        projectsLock.Find(Convert.ToInt32(tbLink.Text)).ignore = true;
                    }
                }
                List<int> counter = new List<int>();
                projectsLock.levels.Clear();
                prList = projectsLock.WritePrList("", counter, projectsLock.levels);
                if (con != null)
                {
                    con.SerProj(projectsLock);
                }
                rtbList.Text = prList;
            }
            else
            {
                MessageBox.Show("Fill in the ID field.");
            }
        }
    }
}
