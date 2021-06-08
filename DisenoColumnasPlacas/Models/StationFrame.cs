using ETABS2016;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisenoColumnasPlacas.Models
{
    public class StationFrameCollection : ObservableCollection<StationFrame>
    {

    }
    public class StationFrame
    {
        private double _valor;
        public double Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        public StationFrame(double valorStation_m)
        {
            Valor = valorStation_m;
        }
        public override string ToString()
        {
            return Valor.ToString();
        }

        public static StationFrameCollection GetStationsFrame(cSapModel modeloSap,FrameColumn columna)
        {
            StationFrameCollection stations = new StationFrameCollection();
            int ret = modeloSap.SetPresentUnits_2(eForce.kgf, eLength.m, eTemperature.C);
            ret = modeloSap.SetPresentUnits(eUnits.kgf_m_C);
            //Primero Deseleccionar los casos de carga
            ret = modeloSap.Results.Setup.DeselectAllCasesAndCombosForOutput();
            //Seleccionar el caso modal
            ret = modeloSap.Results.Setup.SetCaseSelectedForOutput("Modal");

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
            List<double> stas = new List<double>();
            foreach (double sta in objSta)
            {
                if (!stas.Exists(x => x == sta))
                {
                    stas.Add(sta);
                }
            }
            foreach (double staa in stas.OrderBy(x=>x))
            {
                stations.Add(new StationFrame(staa));
            }
            return stations;
        }
    }
}
