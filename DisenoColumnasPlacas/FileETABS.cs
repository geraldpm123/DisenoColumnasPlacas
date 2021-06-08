using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ETABS2016;

namespace DisenoColumnasPlacas
{
    public class FileETABS
    {
        public static cSapModel RecuperarModeloETABSAbierto(ref string tx)
        {
            ETABS2016.cOAPI myETABSObject = null;
            try
            {
                //get the active ETABS object
                myETABSObject = (ETABS2016.cOAPI)System.Runtime.InteropServices.Marshal.GetActiveObject("CSI.ETABS.API.ETABSObject");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encontro archivo ETABS Abierto");
                //No se pudo encontrar instancia abierta
            }

            cSapModel modeloSap = default(ETABS2016.cSapModel);
            modeloSap = myETABSObject.SapModel;

            //int ret = modeloSap.InitializeNewModel();
            int numbItems = 0;
            string[] data = new string[1];
            string[] items = new string[1];
            int ret = modeloSap.GetProjectInfo(ref numbItems, ref items, ref data);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.AppendLine(items[i] + ": " + data[i]);
            }
            tx = sb.ToString();

            return modeloSap;
        }

        public static List<string> GetFunctions(cSapModel modeloSap)
        {

            int numeroPisos = 0;
            string[] nombres = new string[1];
            int ret = modeloSap.Func.GetNameList(ref numeroPisos, ref nombres);

            return nombres.ToList();
        }
        public static List<string> GetAllFrames(cSapModel modeloSap)
        {
            int ret = 1;
            ret = modeloSap.SetPresentUnits_2(eForce.kgf, eLength.m, eTemperature.C);
            int numberNames = 1;
            string[] Names = new string[1];
            string[] PropName = new string[1];
            string[] StoryName = new string[1];
            string[] PointName1 = new string[1];
            string[] PointName2 = new string[1];
            double[] Point1X = new double[1];
            double[] Point1Y = new double[1];
            double[] Point1Z = new double[1];
            double[] Point2X = new double[1];
            double[] Point2Y = new double[1];
            double[] Point2Z = new double[1];
            double[] Angle = new double[1];
            double[] Offset1X = new double[1];
            double[] Offset2X = new double[1];
            double[] Offset1Y = new double[1];
            double[] Offset2Y = new double[1];
            double[] Offset1Z = new double[1];
            double[] Offset2Z = new double[1];
            int[] CardinalPoint = new int[1];


            ret = modeloSap.FrameObj.GetAllFrames(ref numberNames, ref Names, ref PropName, ref StoryName, ref PointName1, ref PointName2,
                ref Point1X, ref Point1Y, ref Point1Z, ref Point2X, ref Point2Y, ref Point2Z, ref Angle, ref Offset1X, ref Offset2X, ref Offset1Y,
                ref Offset2Y, ref Offset1Z, ref Offset2Z, ref CardinalPoint);
            return Names.ToList();
        }

        

    }
}
