using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Illustrator.v1
{
    interface IFigureCreator
    {
        Figure Create(float x, float y);
    }
}
