using System;

namespace PolynomialCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            string[] lineParts;
            Polynome p = null;
            while ( (line = Console.ReadLine()) != null )
            {
                lineParts = line.Split( ' ' , StringSplitOptions.RemoveEmptyEntries);
                if (lineParts.Length == 0)
                {
                    Console.WriteLine("Syntax Error");
                }
                else
                {
                    if (Coeficient.IsValid(lineParts[0]))
                    {
                        p = Polynome.FromStringArray(lineParts);
                        if (p == null)
                        {
                            Console.WriteLine("Syntax Error");
                        }
                        Console.WriteLine(p);
                        continue;
                    }

                    string[] temp;
                    Polynome p2;
                    switch (lineParts[0])
                    {
                        case "+":
                            temp = new string[lineParts.Length - 1];
                            Array.Copy(lineParts, 1, temp, 0, lineParts.Length - 1);
                            p2 = Polynome.FromStringArray(temp);
                            if (p2 == null || p == null)
                            {
                                Console.WriteLine("Syntax Error");
                                continue;
                            }
                            p = p.AddPolynome(p2);
                            Console.WriteLine(p);
                            break;
                        case "-":
                            temp = new string[lineParts.Length - 1];
                            Array.Copy(lineParts, 1, temp, 0, lineParts.Length - 1);
                            p2 = Polynome.FromStringArray(temp);
                            if (p2 == null || p == null)
                            {
                                Console.WriteLine("Syntax Error");
                                continue;
                            }
                            p = p.SubtractPolynome(p2);
                            Console.WriteLine(p);
                            break;
                        case "*":
                            temp = new string[lineParts.Length - 1];
                            Array.Copy(lineParts, 1, temp, 0, lineParts.Length - 1);
                            p2 = Polynome.FromStringArray(temp);
                            if (p2 == null || p == null)
                            {
                                Console.WriteLine("Syntax Error");
                                continue;
                            }
                            p = p.MultiplyByPolynome(p2);
                            Console.WriteLine(p);
                            break;
                        case "e":
                            int val;
                            if (p == null || lineParts.Length != 2 || !int.TryParse(lineParts[1], out val))
                            {
                                Console.WriteLine("Syntax Error");
                                continue;
                            }
                            Console.WriteLine(p.Eval(val));
                            break;
                        case "d":
                            if (p == null)
                            {
                                Console.WriteLine("Syntax Error");
                                continue;
                            }
                            p = p.Derive();
                            Console.WriteLine(p);
                            break;
                        case "s":
                            temp = new string[lineParts.Length - 1];
                            Array.Copy(lineParts, 1, temp, 0, lineParts.Length - 1);
                            p2 = Polynome.FromStringArray(temp);
                            if (p2 == null || p == null)
                            {
                                Console.WriteLine("Syntax Error");
                                continue;
                            }
                            p = p.Substitute(p2);
                            Console.WriteLine(p);
                            break;
                        default:
                            Console.WriteLine("Syntax Error");
                            break;
                    }
                }
            }
        }
    }
}
