using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Illustrator.v1
{
    class Ellipse : Figure
    {
        private Ellipse(float x, float y, float w, float h) : base(x, y, w, h)
        {
        }

        public override bool Selected { get; set; }

        public override Figure Clone()
        {
            return new Ellipse(X, Y, W, H);
        }

        public override void Draw(Graphics gr)
        {
            gr.FillEllipse(Brushes.Yellow, x, y, w, h);
            gr.DrawEllipse(Pens.Black, x, y, w, h);
        }
        public override bool Touch(float x, float y) => (x < this.x + w && x > this.x && y < this.y + h && y > this.y); // не надо править

        public class EllipseCreator : IFigureCreator
        {
            public Figure Create(float x, float y)
            {
                return new Ellipse(x, y, 20, 20);
            }
        }
    }




}
