using System;
using System.Collections.Generic;
using System.Text;

namespace PolynomialCalc
{
    public struct Coeficient
    {
        int level;
        int coeficient;
        public Coeficient(int level) : this(level, 0)
        {

        }
        public Coeficient(int level, int startCoeficient)
        {
            this.level = level;
            coeficient = startCoeficient;
        }
    }
}
