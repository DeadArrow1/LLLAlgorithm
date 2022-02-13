using System;
using System.Collections.Generic;
using System.Windows;

namespace LLLAlgorithm
{
    class Program
    {
        public class OurVector
        {
            public List<double> basis = new List<double>();

            public OurVector()
            {
            }

            public static double Multiply(OurVector a, OurVector b)
            {
                double suma = 0;
                for (int i = 0; i < a.basis.Count; i++)
                {
                    double multiplication = a.basis[i] * b.basis[i];
                    suma = suma + multiplication;
                }
                return suma;
            }

            public static OurVector Multiply(OurVector a, double j)
            {
                OurVector v = new OurVector();
                for (int i = 0; i < a.basis.Count; i++)
                {
                    v.basis.Add(a.basis[i] * j);

                }
                return v;
            }

            public static OurVector Multiply(double j, OurVector a)
            {
                OurVector v = new OurVector();
                for (int i = 0; i < a.basis.Count; i++)
                {
                    v.basis.Add(a.basis[i] * j);

                }
                return v;
            }

            public static OurVector Subtract(OurVector a, OurVector b)
            {
                OurVector v= new OurVector();
               

                for (int i = 0; i < a.basis.Count; i++)
                {
                    v.basis.Add(a.basis[i] - b.basis[i]);
                }
                return v;
            }

            public static OurVector operator /(OurVector vector, double scalar)
            {
                for (int i = 0; i < vector.basis.Count; i++)
                {
                    vector.basis[i] = vector.basis[i] / scalar;

                }

                return vector;
            }


        }


        static void Main(string[] args)
        {

            //a 47 215
            //b 95 460

            //a 201 37
            //b 1648 297
            int k = 1;
            List<OurVector> OurWorkingVectors = new List<OurVector>();
            OurVector a = new OurVector();
            a.basis.Add(47);
            a.basis.Add(215);

            OurVector b = new OurVector();
            b.basis.Add(95);
            b.basis.Add(460);

            OurWorkingVectors.Add(a);
            OurWorkingVectors.Add(b);

            
            /*Console.WriteLine("Start bases are:        " + OurWorkingVectors[k - 1].basis[0] + "    " + OurWorkingVectors[k].basis[0]);
            Console.WriteLine("                        " + OurWorkingVectors[k - 1].basis[1] + "    " + OurWorkingVectors[k].basis[1]);
            Console.WriteLine();
            Console.WriteLine();*/

            List<OurVector> OurGramSchmidtVectors = new List<OurVector>();

            OurVector ga = new OurVector();
            ga.basis.Add(a.basis[0]);
            ga.basis.Add(a.basis[1]);
            OurGramSchmidtVectors.Add(ga);

            OurVector gb = new OurVector();
            gb = OurGramSchmidtReduction(b);          
           
            OurGramSchmidtVectors.Add(gb);

            double u10;
            Console.WriteLine(OurGramSchmidtVectors[1].basis[0]+";"+ OurGramSchmidtVectors[1].basis[1]);





            while (k < OurWorkingVectors.Count)
            {
              

                u10 = OurSizeCondition(OurWorkingVectors[k], OurGramSchmidtVectors[k - 1]);  //b1,Gb0           
                Console.WriteLine(u10);
                if (u10 >= 0.5)
                {

                    OurWorkingVectors[k] = OurModifyVector(OurWorkingVectors[k], OurWorkingVectors[k - 1], u10);
                    //Console.WriteLine(OurWorkingVectors[k].basis[0]+";"+ OurWorkingVectors[k].basis[1]);
                   
                }
                u10 = OurSizeCondition(OurWorkingVectors[k], OurGramSchmidtVectors[k - 1]);

                if (OurLovasvCondition(OurGramSchmidtVectors[k - 1], OurGramSchmidtVectors[k], u10))
                {
                    Console.WriteLine("true");
                    k = k + 1;
                    if (k >= OurWorkingVectors.Count)
                    {
                        break;
                    }
                }
                else
                {

                    OurSwapWorkingVectors();
                    k = Math.Max(k - 1, 1);
                    OurGramSchmidtVectors[0] = OurWorkingVectors[0];
                    OurGramSchmidtVectors[1] = (OurGramSchmidtReduction(OurWorkingVectors[1]));
                }
                // Console.WriteLine(OurWorkingVectors[k - 1].basis[0] + ";" + OurWorkingVectors[k - 1].basis[1]);
                // Console.WriteLine(OurWorkingVectors[k].basis[0] + ";" + OurWorkingVectors[k].basis[1]);

            }

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Our reduced bases are:");
            for (int i = 0; i < k; i++)
            {
                int j = 0;
                string vector = "";
                while (j < OurWorkingVectors.Count)
                {
                    if (j == 0)
                    {
                        vector = vector + OurWorkingVectors[i].basis[j].ToString();
                    }
                    else
                    {
                        vector = vector + " ; "+ OurWorkingVectors[i].basis[j].ToString();
                    }
                    
                    j++;
                }
                Console.WriteLine(vector);
            }

            
            
            
            
            //Console.WriteLine(OurWorkingVectors[k - 2].basis[0] + ";" + OurWorkingVectors[k - 2].basis[1]);
            //Console.WriteLine(OurWorkingVectors[k-1].basis[0] + ";" + OurWorkingVectors[k-1].basis[1]);

            // Console.WriteLine(OurGramSchmidtVectors[k - 2].basis[0] + ";" + OurGramSchmidtVectors[k - 2].basis[1]);
            //Console.WriteLine(OurGramSchmidtVectors[k - 1].basis[0] + ";" + OurGramSchmidtVectors[k - 1].basis[1]);

            /*Console.WriteLine("Reduced bases are: " + OurWorkingVectors[0] + " " + OurWorkingVectors[1]);

            List<Vector> workingVectors = new List<Vector>();
            workingVectors.Add(new Vector(47, 215));
            workingVectors.Add(new Vector(95, 460));

            List<Vector> GramSchmidtVectors = new List<Vector>();
            GramSchmidtVectors.Add(workingVectors[0]);
            GramSchmidtVectors.Add(GramSchmidtReduction(workingVectors[1]));
            Console.WriteLine("Input bases were: " + workingVectors[0] + " " + workingVectors[1]);
           






           Vector b0 = new Vector(47, 215);
            Vector b1 = new Vector(95, 460);

             Vector Gb0 = new Vector(47, 215);
            Vector Gb1 = GramSchmidtReduction(b1);
            double u10;
            Console.WriteLine(GramSchmidtVectors[1]);


            //MAKING GRAM-SCHMIDTT REDUCTION




            Vector CGb1 = Vector.Subtract(b1, Vector.Multiply(Vector.Multiply(b1, b0),b0)/Vector.Multiply(b0,b0));
             CGb1.X = Math.Round(CGb1.X, 2);
             CGb1.Y = Math.Round(CGb1.Y, 2);
             Console.WriteLine(CGb1);
             Console.WriteLine(Gb1);




            while (k < workingVectors.Count)
            {
                u10 = SizeCondition(workingVectors[k], GramSchmidtVectors[k - 1]);  //b1,Gb0           
                Console.WriteLine(u10);
                if (u10 >= 0.5)
                {
                    //updating b1 vector//
                    workingVectors[k] = ModifyVector(workingVectors[k], workingVectors[k - 1], u10);
                    Console.WriteLine(workingVectors[k]);
                    //b1 = Vector.Subtract(b1, Vector.Multiply(Math.Round(u10), b0));
                }
                //Console.WriteLine(b1);
                //Console.WriteLine((Gb1.X * Gb1.X + Gb1.Y * Gb1.Y));
                //Console.WriteLine((3 / 4 - u10 * u10) * Gb0.X * Gb0.X + Gb0.Y * Gb0.Y);
                if (LovasvCondition(GramSchmidtVectors[k - 1], GramSchmidtVectors[k], u10))
                {
                    Console.WriteLine("true");
                    k = k + 1;
                    if (k >= workingVectors.Count)
                    {
                        break;
                    }
                }
                else
                {

                    SwapWorkingVectors();
                    k = Math.Max(k - 1, 1);
                    GramSchmidtVectors[0] = workingVectors[0];
                    GramSchmidtVectors[1] = (GramSchmidtReduction(workingVectors[1]));
                }



                /*u10 = SizeCondition(b1, Gb0);
                Console.WriteLine(u10);

                if (u10 >= 0.5)
                {
                    /*updating b1 vector

                    b1 = Modifyb1();
                    Console.WriteLine(b1);
                }
                if (LovasvCondition())
                {
                    Console.WriteLine("true");
                    k = k + 1;
                }              

            }


            Console.WriteLine("Reduced bases are: " + workingVectors[0] + " " + workingVectors[1]);

            */


            Console.ReadKey();


            /*double SizeCondition(Vector va, Vector vb)
            {
                return Math.Abs((Vector.Multiply(va, vb)) / (Vector.Multiply(vb, vb)));
            }*/

            double OurSizeCondition(OurVector va, OurVector vb)
            {
                return Math.Abs((OurVector.Multiply(va, vb)) / (OurVector.Multiply(vb, vb)));
            }

            /*Vector ModifyVector(Vector vb, Vector va, double vu)
            {
                vb = Vector.Subtract(vb, Vector.Multiply(Math.Round(vu), va));
                return vb;
            }*/

            OurVector OurModifyVector(OurVector vb, OurVector va, double vu)
            {
                vb = OurVector.Subtract(vb, OurVector.Multiply(va, Math.Round(vu)));
                return vb;
            }

            /*bool LovasvCondition(Vector Ga, Vector Gb, double u)
            {
                if ((Gb.X * Gb.X + Gb.Y * Gb.Y) >= (3 / 4 - u * u) * Ga.X * Ga.X + Ga.Y * Ga.Y)
                {
                    return true;
                }
                return false;
            }*/

            bool OurLovasvCondition(OurVector Ga, OurVector Gb, double u)
            {
                double levaStrana = Gb.basis[0] * Gb.basis[0] + Gb.basis[1] * Gb.basis[1];
                double pravaStrana = (0.75 - u * u) * (Ga.basis[0] * Ga.basis[0] + Ga.basis[1] * Ga.basis[1]);



                if ((Gb.basis[0] * Gb.basis[0] + Gb.basis[1] * Gb.basis[1]) >= (0.75 - u * u) * Ga.basis[0] * Ga.basis[0] + Ga.basis[1] * Ga.basis[1])
                {
                    return true;
                }
                return false;
            }

            /*Vector GramSchmidtReduction(Vector va)
            {

                va = Vector.Subtract(va, Vector.Multiply(Vector.Multiply(va, workingVectors[k - 1]), workingVectors[k - 1]) / Vector.Multiply(workingVectors[k - 1], workingVectors[k - 1]));
                va.X = Math.Round(va.X, 2);
                va.Y = Math.Round(va.Y, 2);

                return va;
            }*/

            OurVector OurGramSchmidtReduction(OurVector va)
            {

                OurVector vb = OurVector.Subtract(va, OurVector.Multiply(OurVector.Multiply(va, OurGramSchmidtVectors[k - 1]), OurWorkingVectors[k - 1]) / OurVector.Multiply(OurGramSchmidtVectors[k - 1], OurGramSchmidtVectors[k - 1]));
                for (int i = 0; i < va.basis.Count; i++)
                {
                    vb.basis[i] = Math.Round(vb.basis[i], 2);
                }
               

                return vb;
            }

            /*void SwapWorkingVectors()
            {
                Vector t = workingVectors[k - 1];
                workingVectors[k - 1] = workingVectors[k];
                workingVectors[k] = t;
            }*/

            void OurSwapWorkingVectors()
            {
                OurVector t = OurWorkingVectors[k - 1];
                OurWorkingVectors[k - 1] = OurWorkingVectors[k];
                OurWorkingVectors[k] = t;
            }
        }
    }

}
