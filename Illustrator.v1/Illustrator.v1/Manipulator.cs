using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Illustrator.v1
{
    class Manipulator : Figure
    {
        public Figure figure { get; private set; }
        public override bool Selected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        ActivePoint corner = ActivePoint.None;
        enum ActivePoint
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            Figure,
            None
        }
        public void Attach(Figure figureToAdd)
        {
            figure = figureToAdd;
            Update();
        }
        public void Drag(float dx, float dy)
        {
            switch (corner)
            {
                case ActivePoint.TopLeft:
                    figure.Resize(-dx, -dy);
                    figure.Move(dx, dy);
                    Update();
                    break;
                case ActivePoint.TopRight:
                    figure.Resize(dx, -dy);
                    figure.Move(0, dy);
                    Update();
                    break;
                case ActivePoint.BottomLeft:
                    figure.Move(dx, 0);
                    figure.Resize(-dx,dy);
                    Update();
                    break;
                case ActivePoint.BottomRight:
                    figure.Resize(dx, dy);
                    Update();
                    break;
                case ActivePoint.Figure:
                    figure.Move(dx, dy);
                    Update();
                    break;
                default:
                    break;
            }
        }
        public void Update()
        {
            this.x = figure.X;
            this.y = figure.Y;
            this.w = figure.W;
            this.h = figure.H;
        }
        public void Clear() => figure = null;
        public override void Draw(Graphics gr)
        {
            if (figure != null)
            {
                Pen p1 = new Pen(Color.Black, 1);
                gr.DrawRectangle(p1, x - 2, y - 2, w + 4, h + 4); // сетка
                gr.DrawRectangle(p1, x - 4, y - 4, 4, 4); //TopLeft
                gr.DrawRectangle(p1, x + w, y - 4, 4, 4);
                gr.DrawRectangle(p1, x - 4, y + h, 4, 4); //BotLeft
                gr.DrawRectangle(p1, x + w, y + h, 4, 4);
            }
        }
        public override bool Touch(float xx, float yy)
        {
            if (figure == null)
                return false;

           

            if (figure.Touch(xx, yy))
            {
                corner = ActivePoint.Figure;
                return true;
            }
            else if (Math.Abs(xx - x) >= 0 && Math.Abs(xx - x) <= 4 && Math.Abs(yy - y) >= 0 && Math.Abs(yy - y) <= 4)
            {
                corner = ActivePoint.TopLeft;
                return true;
            }
            else if (Math.Abs(xx - x - h) >= 0 && Math.Abs(xx - x - h) <= 4 && Math.Abs(yy - y) >= 0 && Math.Abs(yy - y) <= 4)
            {
                corner = ActivePoint.TopRight;
                return true;
            }
            else if (Math.Abs(xx - x) >= 0 && Math.Abs(xx - x) <= 4 && Math.Abs(yy - y - w) >= 0 && Math.Abs(yy - y - w) <= 4)
            {
                corner = ActivePoint.BottomLeft;
                return true;
            }
            else if (Math.Abs(xx - x - h) >= 0 && Math.Abs(xx - x - h) <= 4 && Math.Abs(yy - y - w) >= 0 && Math.Abs(yy - y - w) <= 4)
            {
                corner = ActivePoint.BottomRight;
                return true;
            }
            if (!figure.Touch(xx, yy))
                return false;

            return false;
        }

        public override Figure Clone()
        {
            return figure;
        }
    }
}
