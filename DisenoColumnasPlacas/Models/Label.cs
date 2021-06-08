using ETABS2016;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DisenoColumnasPlacas.Models
{
    public class LabelCollection : ObservableCollection<Label>
    {

    }
    public class Label
    {
        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public Label (string nombreLabel)
        {
            Nombre = nombreLabel;
        }
        public override string ToString()
        {
            return Nombre;
        }

        public static LabelCollection GetLabelColumns(cSapModel modeloSap)
        {
            int num = 1;
            string[] nombres = new string[1];
            string[] labels = new string[1];
            string[] storys = new string[1];
            int ret = modeloSap.FrameObj.GetLabelNameList(ref num, ref nombres, ref labels, ref storys);
            List<string> labelsColumns = new List<string>();
            foreach (string label in labels)
            {
                if (label[0] == 'C' && !labelsColumns.Exists(x => x == label))
                {
                    labelsColumns.Add(label);
                }
            }
            LabelCollection labelsColumnsC = new LabelCollection();
            foreach (string nameLab in labelsColumns.OrderBy(x=>x))
            {
                labelsColumnsC.Add(new Label(nameLab));
            }
            
            return labelsColumnsC;
        }
    }
}
