using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Illustrator.v1
{
    class Rectangle : Figure
    {

        private Rectangle(float x, float y, float w, float h) : base(x, y, w, h)
        {
        }

        public override void Draw(Graphics gr)
        {
            gr.FillRectangle(Brushes.Red, x, y, w, h);
            gr.DrawRectangle(Pens.Black, x, y, w, h);
        }
        public override bool Selected { get; set; }
        public override bool Touch(float x, float y) => (x < this.x + w && x > this.x && y < this.y + h && y > this.y);

        public override Figure Clone()
        {
            return new Rectangle(X, Y, W, H);
        }

        public class RectangleCreator : IFigureCreator
        {
            public Figure Create(float x, float y)
            {
                return new Rectangle(x, y, 20, 20);
            }
        }
    }
}
