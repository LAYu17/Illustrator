using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Illustrator.v1
{
    public partial class Form1 : Form
    {
        //List<Figure> figures = new List<Figure>();
        List<IFigureCreator> tools = new List<IFigureCreator>();
        IFigureCreator currentTool;
        public Form1()
        {
            InitializeComponent();
            gr = pictureBox1.CreateGraphics();
            tools.Add(new Ellipse.EllipseCreator());
            tools.Add(null);
            tools.Add(new Rectangle.RectangleCreator());
        }
        Figure fig;
        private Graphics gr;
        private int oldX, oldY;
        Picture pic;
        Manipulator m;
        Group group;
        GroupCreator groupcreator;
        bool ctrlStates = false;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentTool == null)
            {
                //if (m.Touch(e.X, e.Y)) return;
                //else if (group.Touch(e.X, e.Y)) return;
                //else if (!ctrlStates)                     
                //                if (pic.Figure.Touch(e.X, e.Y))
                //                {
                //                    group.Clear();
                //                    group.Add(fig);
                //                    m.Attach(fig);
                //                    oldX = e.X;
                //                    oldY = e.Y;
                //                }
                //          else      if (pic.Figure.Touch(e.X, e.Y))
                //                {
                //                    group.Add(fig);
                //                    m.Attach(group);
                //                    oldX = e.X;
                //                    oldY = e.Y;
                //                    m.Update();
                //                }
                //    }
                oldX = e.X;
                oldY = e.Y;
                if (m.Touch(e.X, e.Y))
                {
                    return;
                }
                if (pic.Figure != null)
                {
                    if (pic.Figure.Touch(e.X, e.Y)) return;
                }
                Figure sh = pic.Select(e.X, e.Y);
                if (sh != null)
                {
                    m.Attach(sh);
                    m.Touch(e.X, e.Y);
                }
                pictureBox1.Refresh();
            }
            else
            {
                fig = currentTool.Create(e.X, e.Y);
                pic.Add(fig);
                fig.Draw(gr);
                oldX = e.X;
                oldY = e.Y;
                pic.Deselect();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pic = new Picture();
            m = new Manipulator();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            pic.Draw(e.Graphics);
            m.Draw(e.Graphics);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (currentTool != null)
            {
                pic.Deselect();
                pictureBox1.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentTool = tools[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentTool = tools[1];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentTool = tools[2];
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return; 
            currentTool = tools[listBox1.SelectedIndex+3];
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                ctrlStates = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (m.figure == null)
                return;
            GroupCreator groupCreator = new GroupCreator();
            groupCreator.CopyProto(group);
            tools.Add(groupCreator);                                
            listBox1.Items.Add("Group " + (listBox1.Items.Count + 1));
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (!e.Control)
                ctrlStates = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (m.figure != null)
            {
                    m.Drag(e.X - oldX, e.Y - oldY);
                    oldX = e.X;
                    oldY = e.Y;
                    pictureBox1.Refresh();
            }

        }
    }
}
