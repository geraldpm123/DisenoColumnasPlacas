using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace DisenoColumnasPlacas.Models
{
    public class DiagramaInteraccion
    {
        private List<double> _p2 = new List<double>();
        private List<double> _p3 = new List<double>();
        private List<double> _m2 = new List<double>();
        private List<double> _m3 = new List<double>();
        
        public List<double> P2
        {
            get { return _p2; }
            set { _p2 = value; }
        }
        public List<double> P3
        {
            get { return _p3; }
            set { _p3 = value; }
        }
        public List<double> M2
        {
            get { return _m2; }
            set { _m2 = value; }
        }
        public List<double> M3
        {
            get { return _m3; }
            set { _m3 = value; }
        }
        public DiagramaInteraccion(string ruta)
        {
            if (ruta != "")
            {
                string[] lineas = File.ReadAllLines(ruta);
                IList<int> num = new List<int>();
                IList<double> P = new List<double>();
                IList<double> M = new List<double>();

                for (int i = 0; i < lineas.Length; i++)
                {
                    string[] valores = Regex.Split(lineas[i],"\t");
                    if (valores[0]!="Point")
                    {
                        try
                        {
                            int x = Convert.ToInt32(valores[0]);
                            double p = Convert.ToDouble(valores[1]);
                            double m = Convert.ToDouble(valores[2]);
                            num.Add(x);
                            P.Add(p);
                            M.Add(m);
                        }
                        catch (Exception){}   
                    }
                }
                for (int i = 0; i < 30; i++)
                {
                    _p2.Add(P[i]);
                    _m2.Add(M[i]);
                }
                for (int i = 30; i < 60; i++)
                {
                    _p3.Add(P[i]);
                    _m3.Add(M[i]);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No se ha establecido una ruta valida");
            }
        }
        public static string GetRutaDeTXTDiagramaIteraccion()
        {
            string ruta = "";
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Archivo TXT (*.txt)|*.txt";
            if (open.ShowDialog() == DialogResult.OK)
            {
                ruta = open.FileName;
            }
           
            return ruta;
        }

        
    }
}
