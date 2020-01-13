using System;
using System.Collections.Generic;
using System.Text;

namespace PolynomialCalc
{
    class Polynome
    {
        public readonly int maxLevel;
        public readonly int minLevel;
        List<Coeficient> coefs;
        private Polynome(int from, int to)
        {
            maxLevel = from;
            minLevel = to;
            coefs = new List<Coeficient>();
            for (int i = from; i >= to; i--)
            {
                coefs.Add(new Coeficient(i));
            }
        }
        private Polynome(List<Coeficient> l)
        {
            coefs = l;
            maxLevel = coefs[0].level;
            minLevel = coefs[coefs.Count - 1].level;
        }
        private Polynome(Coeficient[] l)
        {
            List<Coeficient> list = new List<Coeficient>();
            for (int i = 0; i < l.Length; i++)
            {
                list.Add(l[i]);
            }
            coefs = list;
            maxLevel = coefs[0].level;
            minLevel = coefs[coefs.Count - 1].level;
        }
        public static Polynome FromStringArray(string[] coefs) //null if not valid
        {
            List<Coeficient> tempCoefs = new List<Coeficient>();
            Coeficient? c = null;
            for (int i = 0; i < coefs.Length; i++)
            {
                c = Coeficient.FromString(coefs[i]);
                if (c.HasValue)
                {
                    tempCoefs.Add(c.Value);
                }
                else
                {
                    return null;
                }
                
            }
            return new Polynome(tempCoefs);
        }
        private void RemoveUnnecessary()
        {
            for (int i = 0; i < this.coefs.Count; i++)
            {
                if (coefs[i].coeficient == 0)
                {
                    coefs.RemoveAt(i--);
                }
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < coefs.Count; i++)
            {
                if (i > 0) sb.Append(' ');
                sb.Append(coefs[i].ToString());
            }
            return sb.ToString();
        }

        internal Polynome AddPolynome(Polynome p2)
        {
            int maxLevel = this.maxLevel;
            int minLevel = this.minLevel;
            if (p2.maxLevel > maxLevel)
            {
                maxLevel = p2.maxLevel;
            }
            if (p2.minLevel < minLevel)
            {
                minLevel = p2.minLevel;
            }
            Polynome res = new Polynome(maxLevel, minLevel);

            Coeficient c;

            for (int i = 0; i < this.coefs.Count; i++)
            {
                c = this.coefs[i];
                res.coefs[res.maxLevel - c.level] = res.coefs[res.maxLevel - c.level].AddCoeficient(c.coeficient);
            }

            for (int i = 0; i < p2.coefs.Count; i++)
            {
                c = p2.coefs[i];
                res.coefs[res.maxLevel - c.level] = res.coefs[res.maxLevel - c.level].AddCoeficient(c.coeficient);
            }

            res.RemoveUnnecessary();

            return res;
        }

        internal Polynome MultiplyByPolynome(Polynome p2)
        {
            if (p2.coefs.Count == 1 && p2.coefs[0].coeficient == 0)
            {
                return new Polynome(0,0);
            }
            int maxLevel = this.maxLevel + p2.maxLevel;
            int minLevel = this.minLevel + p2.minLevel;
            Polynome res = new Polynome(maxLevel, minLevel);
            for (int i = 0; i < this.coefs.Count; i++)
            {
                var thisC = this.coefs[i];
                for (int k = 0; k < p2.coefs.Count; k++)
                {
                    var p2C = p2.coefs[k];
                    int pow = thisC.level + p2C.level;
                    int c = thisC.coeficient*p2C.coeficient;
                    res.coefs[maxLevel - pow] = res.coefs[maxLevel - pow].AddCoeficient(c);
                }
            }

            res.RemoveUnnecessary();
            return res;
        }

        internal Polynome SubtractPolynome(Polynome p2)
        {
            int maxLevel = this.maxLevel;
            int minLevel = this.minLevel;
            if (p2.maxLevel > maxLevel)
            {
                maxLevel = p2.maxLevel;
            }
            if (p2.minLevel < minLevel)
            {
                minLevel = p2.minLevel;
            }
            Polynome res = new Polynome(maxLevel, minLevel);

            Coeficient c;

            for (int i = 0; i < this.coefs.Count; i++)
            {
                c = this.coefs[i];
                res.coefs[res.maxLevel - c.level] = res.coefs[res.maxLevel - c.level].AddCoeficient(c.coeficient);
            }

            for (int i = 0; i < p2.coefs.Count; i++)
            {
                c = p2.coefs[i];
                res.coefs[res.maxLevel - c.level] = res.coefs[res.maxLevel - c.level].SubtractCoeficient(c.coeficient);
            }

            res.RemoveUnnecessary();

            return res;
        }

        internal Polynome Substitute(Polynome p2)
        {
            int maxLevel = this.maxLevel * p2.maxLevel;
            int minLevel = this.minLevel * p2.minLevel;
            var res = new Polynome(maxLevel, minLevel);
            Polynome tempConstant = p2.MultiplyByPolynome(new Polynome(new Coeficient[] { new Coeficient(0, 1) }));; //get equal polynome

            for (int i = 0; i < this.coefs.Count; i++)
            {
                var thisC = this.coefs[i];
                    int pow;
                    Polynome temp = p2.MultiplyByPolynome(new Polynome(new Coeficient[] { new Coeficient(0, 1) })); //get equal polynome
                    if (thisC.level >= 1){
                        for (int l = 1; l < thisC.level; l++)
                        {
                            temp = temp.MultiplyByPolynome(tempConstant);
                        }
                    }
                    else
                    {
                        temp = Polynome.FromStringArray(new string[] { "1" });
                    }
                    for (int l = 0; l < temp.coefs.Count; l++)
                    {
                        var tempC = temp.coefs[l];
                        pow = tempC.level;
                        if (thisC.level == 0)
                        {

                        }
                        int c = thisC.coeficient*tempC.coeficient;
                        res.coefs[maxLevel - pow] = res.coefs[maxLevel - pow].AddCoeficient(c);
                    }
                
            }

            res.RemoveUnnecessary();
            return res;
        }

        internal Polynome Derive()
        {
            int minLevel;
            if (this.minLevel == 0)
            {
                minLevel = 0;
            }
            else
            {
                minLevel = this.minLevel - 1;
            }
            if (this.maxLevel == this.minLevel && this.minLevel == 0)
            {
                return new Polynome(0, 0);
            }
            Polynome p = new Polynome(this.maxLevel - 1, minLevel);

            for (int i = 0; i < coefs.Count; i++)
            {
                int pow = this.coefs[i].level;
                int level = pow - 1;
                int c = this.coefs[i].coeficient*pow;
                if ( i >= p.coefs.Count)
                {
                    continue;
                }
                p.coefs[i] = new Coeficient(level, c );
            }
            p.RemoveUnnecessary();
            return p;
        }

        public int Eval(int c)
        {
            int res = 0;
            Coeficient co;
            for (int i = 0; i < coefs.Count; i++)
            {
                co = coefs[i];
                res += co.coeficient*Power(c, co.level);
            }
            return res;
        }
        private int Power(int c, int power)
        {
            int res = 1;
            for (int i = 0; i < power; i++)
            {
                res *= c;
            }
            return res;
        }
    }
}
