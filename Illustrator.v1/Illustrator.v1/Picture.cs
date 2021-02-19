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
    class Picture
    {
        private List<Figure> figures = new List<Figure>();
        public Figure Figure { get; private set; }

        public void Add(Figure figureToAdd)
        {
            figures.Add(figureToAdd);
            if (Figure != null)
            {
                Figure.Selected = false;
            }
            Figure = figureToAdd;
        }
        public void Deselect()
        {
            if (Figure == null)
                return;
            Figure.Selected = false;
            Figure = null;
        }

        public Figure Select(float x, float y)
        {
            Figure selectedFigure = null;
            Deselect();
            foreach (var item in figures)
            {
                if (item.Touch(x, y))
                {
                    selectedFigure = item;
                }
                if (selectedFigure != null)
                {
                    selectedFigure.Selected = true;
                    Figure = selectedFigure;
                }
            }
            return selectedFigure;
        }
        public void Draw(Graphics gr)
        {
            foreach (var item in figures)
            {
                item.Draw(gr);
            }           
        }
    }
}

