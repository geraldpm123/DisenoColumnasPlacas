using ETABS2016;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DisenoColumnasPlacas.Models
{
    public class FrameColumn
    {
        public string Id { get; set; }
        public string Nivel { get; set; }
        public string Label { get; set; }
        public double Longitud { get; set; }

        public FrameColumn(string id, string nivel, string label)
        {
            Id = id;
            Nivel = nivel;
            Label = label;

        }
        public override string ToString()
        {
            return Id;
        }
        public static FrameColumn ObtenerColumnasPorLabelYNivel(cSapModel modeloSap, Label label, Nivel level)
        {
            int num = 1;
            string[] nombres = new string[1];
            string[] labels = new string[1];
            string[] storys = new string[1];
            int ret = modeloSap.FrameObj.GetLabelNameList(ref num, ref nombres, ref labels, ref storys);

            FrameColumn columna = null;
            for (int i = 0; i < nombres.Length; i++)
            {
                if (labels[i] == label.Nombre && storys[i] == level.Nombre)
                {
                    columna = new FrameColumn(nombres[i], storys[i], labels[i]);
                }
            }
            return columna;
        }
    }
}
