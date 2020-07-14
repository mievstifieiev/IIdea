using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Reflection.PortableExecutable;

namespace IIdeaApp
{
    public partial class GraphForm : Form
    {
        Project project;
        Graphics graphic;
        Bitmap bmp;
        Pen pen;
        SolidBrush brush;
        public GraphForm(Project _project)
        {
            project = _project;
            InitializeComponent();
            bmp = new Bitmap(pbGraph.Width, pbGraph.Height);
            //graphic = pbGraph.CreateGraphics();
            pen = new Pen(Color.Black);
            brush = new SolidBrush(Color.Black);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
        }

        public void DrowTree(Point point1, Project pr, Graphics graphic, int deep, List<int> levels, int p)
        {
            Random rnd = new Random();
            graphic.FillEllipse(brush, (point1.X/2)-5, point1.Y, 20, 20);
            int counter = pr.part.Count;
            deep++;
            int part = 100;
            int j = 0;
            if (pr.part.Count > 0)
            {
                part = pbGraph.Width / levels[deep];
            }
            for (int i = 0; i < counter; i++)
            {
                Point pointNew = new Point(part*(i+1) + part*p , point1.Y+50);
                DrowTree(pointNew, pr.part[i], graphic,deep, levels, j);
                graphic.DrawLine(pen, new Point(point1.X / 2, point1.Y), new Point(pointNew.X/2,pointNew.Y));
                j += pr.part[i].part.Count;
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Point point = new Point(pbGraph.Width, 30);
            graphic = pbGraph.CreateGraphics();
            DrowTree(point, project, graphic, -1, project.levels, 0);
        }
        
        private void PbGraph_Paint(object sender, PaintEventArgs e)
        {
            Graphics grap = e.Graphics;
            //Point point = new Point(pbGraph.Width / 2, 20);
            //DrowTree(point, project, grap);
            //throw new NotImplementedException();
        }
    }
}
