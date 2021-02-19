using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Illustrator.v1
{
    class Group : Figure
    {
        List<Figure> figures = new List<Figure>();

        public override bool Selected { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public Group(float x, float y, float w, float h) : base(x, y, w, h)
        {

        }
        public Group() { }
        public override void Draw(Graphics gr)
        {
            foreach (Figure fig in figures) { fig.Draw(gr); }
        }

        public override bool Touch(float x, float y) => x < this.x + w && x > this.x && y > this.y && y < this.y + h;

        public override void Move(float dx, float dy)
        {
            foreach (Figure fig in figures) { fig.Move(dx, dy); }
            x += dx;
            y += dy;
        }

        public override void Resize(float dw, float dh)
        {
            float kw = dw / w, kh = dh / h;
            foreach (Figure fig in figures)
            {
                fig.Resize(fig.W * kw, fig.H * kh);
                fig.Move(kw * (fig.X - x), kh * (fig.Y - y));
            }
            w += dw;
            h += dh;
        }

        public void Add(Figure simpleFigure)
        {
            int count = 0;
            foreach (Figure fig in figures)
            {
                if (fig == simpleFigure)
                    count++;
            }
            if (count == 0)
            {
                figures.Add(simpleFigure);
            }
            Update();
        }

        public void Clear()
        {
            figures.Clear();
            x = 0;
            y = 0;
            h = 0;
            w = 0;
        }

        private void Update()
        {
            x = figures.Min(f => f.X);
            y = figures.Min(f => f.Y);
            w = figures.Max(f => f.X + f.W) - x;
            h = figures.Max(f => f.Y + f.H) - y;
        }
        public override Figure Clone()
        {
            Group gr = new Group(x, y, w, h);
            foreach (Figure a in figures)
                gr.Add(a.Clone());
            return gr;
        }

        public bool Touch(int x, int y)
        {
            throw new System.NotImplementedException();
        }
    }
}