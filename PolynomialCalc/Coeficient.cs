using System;
using System.Collections.Generic;
using System.Text;

namespace PolynomialCalc
{
    public struct Coeficient
    {
        public readonly int level;
        public int coeficient { get; private set; }
        public Coeficient(int level) : this(level, 0)
        {

        }
        public Coeficient(int level, int startCoeficient)
        {
            this.level = level;
            coeficient = startCoeficient;
        }
        public static Coeficient? FromString(string s)
        {
            if (s.Contains('x'))
            {
                string[] parts;
                if (s.Contains('^'))
                {
                    parts = s.Split("x^", StringSplitOptions.RemoveEmptyEntries);
                    int coef;
                    int level;
                    if (parts.Length > 2 || parts.Length == 0)
                    {
                        return null;
                    }
                    switch (parts.Length)
                    {
                        case 1:
                            coef = 1;
                            if (!int.TryParse(parts[0], out level))
                            {
                                return null;
                            }
                            break;
                        case 2:
                            if (parts[0] == "-") coef = -1;
                            else if (!int.TryParse(parts[0], out coef))
                            {
                                return null;
                            }
                            if (!int.TryParse(parts[1], out level))
                            {
                                return null;
                            }
                            break;
                        default:
                            return null;
                    }
                    return new Coeficient(level, coef);
                }
                else
                {
                    int coef;
                    if (s.Substring(0, s.Length - 1).Length == 0)
                    {
                        return new Coeficient(1,1);
                    }
                    else if (s.Substring(0, s.Length - 1) == "-") return new Coeficient(1, -1);
                    else if (int.TryParse(s.Substring(0, s.Length - 1), out coef)){
                        return new Coeficient(1, coef);
                    }
                    else
                    {
                        return null;
                    }
                    
                }
            }
            else
            {
                int coef;
                if (int.TryParse(s, out coef)){
                    return new Coeficient(0, coef);
                }
                else
                {
                    return null;
                }
            }
        }


        internal static bool IsValid(string s)
        {
            Coeficient? c = FromString(s);
            if (c.HasValue) return true;
            else return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (level == 0)
            {
                sb.Append(coeficient);
                return sb.ToString();
            }
            if (level == 1)
            {
                if (coeficient != 1 && coeficient != -1)
                sb.Append(coeficient);
                else if (coeficient == -1) sb.Append('-');
                sb.Append('x');
                return sb.ToString();
            }

            if (coeficient != 1 && coeficient != -1)
                sb.Append(coeficient);
            else if (coeficient == -1) sb.Append('-');
            if (level != 0)
                sb.Append('x');
            if ( level != 1 && level != 0 )
            {
                sb.Append('^');
                sb.Append(level);
            } 
            return sb.ToString();
        }
        public Coeficient AddCoeficient(int c)
        {
            this.coeficient = this.coeficient + c;
            return this;
        }

        internal Coeficient SubtractCoeficient(int c)
        {
             this.coeficient = this.coeficient - c;
            return this;
        }
    }
}
