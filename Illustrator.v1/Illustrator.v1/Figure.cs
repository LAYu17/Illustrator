using System.Drawing;
using System.Drawing.Drawing2D;

namespace Illustrator.v1
{
    abstract class Figure
    {
        protected float x, y, w, h;
        public float X => x;
        public float Y => y;
        public float W => w;
        public float H => h;
        public Figure()
        {
            x = y = h = w = 1;
        }
        public virtual void Resize(float dx, float dy)
        {
            w += dx;
            h += dy;
        }
        public Figure(float x, float y, float w, float h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }
        public abstract bool Selected { get; set; }
        public abstract void Draw(Graphics gr);
        public abstract bool Touch(float x, float y);
        public abstract Figure Clone();
        public virtual void Move(float dx, float dy)
        {
            x += dx;
            y += dy;
        }

    }

}