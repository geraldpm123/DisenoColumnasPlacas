using ETABS2016;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisenoColumnasPlacas.Models
{
    public class ResultFrameCollection : ObservableCollection<ResultFrame>
    {

    }
    public class ResultFrame
    {
        private string _nombreFrame;
        private double _station;
        private double _p;
        private double _v2;
        private double _v3;
        private double _t;
        private double _m2;
        private double _m3;

        #region CAMPOS
        public string NombreFrame
        {
            get { return _nombreFrame; }
            set { _nombreFrame = value; }
        }
        public double Station
        {
            get { return _station; }
            set { _station = value; }
        }
        public double P
        {
            get { return _p; }
            set { _p = value; }
        }
        public double V2
        {
            get { return _v2; }
            set { _v2 = value; }
        }
        public double V3
        {
            get { return _v3; }
            set { _v3 = value; }
        }
        public double T
        {
            get { return _t; }
            set { _t = value; }
        }
        public double M2
        {
            get { return _m2; }
            set { _m2 = value; }
        }
        public double M3
        {
            get { return _m3; }
            set { _m3 = value; }
        }
        #endregion

        public ResultFrame(string nombreFrame,double station,
            double p, double v2, double v3, double t, double m2, double m3)
        {
            NombreFrame = nombreFrame;
            Station = station;
            P = p;
            V2 = v2;
            V3 = v3;
            T = t;
            M2 = m2;
            M3 = m3;
        }
        public override string ToString()
        {
            return "P="+Math.Round(P/1000,2)+", M2="+ Math.Round(M2/1000,2) +", M3="+ Math.Round(M3/1000,2);
        }

        public static PlotModel GraficaEje2(ResultFrameCollection resultados, DiagramaInteraccion di)
        {
            /*
            double maxM = 0;
            double minM = 0;
            double MaxP = 200;
            double MinP = -100;
            foreach (ResultFrame resultFrame in resultados)
            {
                if (resultFrame.M2/1000>maxM)
                {
                    maxM = Math.Ceiling(resultFrame.M2 / 1000);
                }
                if (resultFrame.M2 / 1000 < minM)
                {
                    minM = Math.Floor(resultFrame.M2 / 1000);
                }
            }
            if (maxM<5)
            {
                maxM = 5;
            }
            if (minM>5)
            {
                minM = -5;
            }*/
            
            LinearAxis ejeX = new LinearAxis();
            /*ejeX.Minimum = -minM;
            ejeX.Maximum = maxM;*/
            ejeX.Position = AxisPosition.Bottom;

            LinearAxis ejeY = new LinearAxis();
            /*ejeY.Minimum = MinP;
            ejeY.Maximum = MaxP;*/
            ejeY.Position = AxisPosition.Left;
            

            PlotModel Graficaa = new PlotModel();
            Graficaa.Axes.Add(ejeX);
            Graficaa.Axes.Add(ejeY);
            Graficaa.Title = "Diagrama de Interaccion";

            LineSeries lineaPuntosSolicitacion = new LineSeries();
            foreach (ResultFrame result in resultados)
            {
                lineaPuntosSolicitacion.Points.Add(new DataPoint(result.M2 / 1000, -result.P / 1000));
            }
            lineaPuntosSolicitacion.LineStyle = LineStyle.None;
            lineaPuntosSolicitacion.MarkerType = MarkerType.Circle;
            lineaPuntosSolicitacion.Title = "Solicitacion";
            Graficaa.Series.Add(lineaPuntosSolicitacion);

            if (di != null)
            {
                LineSeries lineaDi2 = new LineSeries();
                for (int i = 0; i < di.P2.Count; i++)
                {
                    lineaDi2.Points.Add(new DataPoint(di.M2[i], di.P2[i]));
                }
                lineaDi2.LineStyle = LineStyle.Solid;
                lineaDi2.Title = "DI";
                Graficaa.Series.Add(lineaDi2);
            }
            
            
            
            
            return Graficaa;
        }

        public static ResultFrameCollection GetResults5Combos(cSapModel modeloSap, FrameColumn columna, StationFrame station,
            LoadCombo combo1,LoadCombo combo2, LoadCombo combo3, LoadCombo combo4, LoadCombo combo5)
        {
            ResultFrameCollection results = new ResultFrameCollection();
            //Combo1
            results.Add(GetFrameResult(modeloSap, columna, station, combo1, "Max"));
            //Combo2
            results.Add(GetFrameResult(modeloSap, columna, station, combo2, "Max"));
            //Combo3
            results.Add(GetFrameResult(modeloSap, columna, station, combo2, "Min"));
            //Combo4
            results.Add(GetFrameResult(modeloSap, columna, station, combo3, "Max"));
            //Combo5
            results.Add(GetFrameResult(modeloSap, columna, station, combo3, "Min"));
            //Combo6
            results.Add(GetFrameResult(modeloSap, columna, station, combo4, "Max"));
            //Combo7
            results.Add(GetFrameResult(modeloSap, columna, station, combo4, "Min"));
            //Combo8
            results.Add(GetFrameResult(modeloSap, columna, station, combo5, "Max"));
            //Combo1
            results.Add(GetFrameResult(modeloSap, columna, station, combo5, "Min"));
            return results;
        }

        public static ResultFrame GetFrameResult(
            cSapModel modeloSap, FrameColumn columna, StationFrame station,LoadCombo combo, string MaxMin)
        {
            int ret = modeloSap.SetPresentUnits_2(eForce.kgf, eLength.m, eTemperature.C);
            ret = modeloSap.SetPresentUnits(eUnits.kgf_m_C);
            //Primero Deseleccionar los casos de carga
            ret = modeloSap.Results.Setup.DeselectAllCasesAndCombosForOutput();
            //Seleccionar el caso modal
            ret = modeloSap.Results.Setup.SetComboSelectedForOutput(combo.Nombre);

            //Obtener resultados frame 
            int numberR = 1;
            string[] obj = new string[1];
            double[] objSta = new double[1];
            string[] elm = new string[1];
            double[] elmSta = new double[1];
            string[] loadCase = new string[1];
            string[] stepType = new string[1];
            double[] stepNum = new double[1];
            double[] P = new double[1];
            double[] V2 = new double[1];
            double[] V3 = new double[1];
            double[] T = new double[1];
            double[] M2 = new double[1];
            double[] M3 = new double[1];

            ret = modeloSap.Results.FrameForce(columna.Id, eItemTypeElm.ObjectElm, ref numberR, ref obj, ref objSta,
                ref elm, ref elmSta, ref loadCase, ref stepType, ref stepNum, ref P, ref V2, ref V3, ref T, ref M2, ref M3);
            double p = 0;
            double v2 = 0;
            double v3 = 0;
            double t = 0;
            double m2 = 0;
            double m3 = 0;
            if (stepType[0] == "Single Value")
            {
                for (int i = 0; i < objSta.Length; i++)
                {
                    if (objSta[i] == station.Valor)
                    {
                        p = P[i];
                        v2 = V2[i];
                        v3 = V3[i];
                        t = T[i];
                        m2 = M2[i];
                        m3 = M3[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < objSta.Length; i++)
                {
                    if (objSta[i] == station.Valor && stepType[i] == MaxMin)
                    {
                        p = P[i];
                        v2 = V2[i];
                        v3 = V3[i];
                        t = T[i];
                        m2 = M2[i];
                        m3 = M3[i];
                    }
                }
            }
            //Crear el resultado Frame
            ResultFrame result = new ResultFrame(columna.Id, station.Valor, p, v2, v3, t, m2, m3);
            return result;
        }


    }
}
