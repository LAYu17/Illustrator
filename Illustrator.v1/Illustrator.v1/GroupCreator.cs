using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Illustrator.v1
{
    class GroupCreator : IFigureCreator
    {
        private Figure p;

        public Figure Create(float x, float y)
        {
            Figure figure;
            figure = p.Clone();
            figure.Resize(0, 0);
            figure.Move(x - figure.X - (figure.W / 2), y - figure.Y - (figure.H / 2));
            return figure;
        }
        public Figure Create(float x, float y, float w, float h)
        {
            Figure figure;
            figure = p.Clone();
            figure.Resize(figure.W - w, figure.H - h);
            figure.Move(x - figure.X- (figure.W / 2), y - figure.Y - (figure.H / 2));
            return figure;
        }
        public void CopyProto(Group gp)
        {
            p = gp.Clone();
        }
    }
}
