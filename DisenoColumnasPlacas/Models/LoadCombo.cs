using ETABS2016;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisenoColumnasPlacas.Models
{
    public class LoadComboCollection : ObservableCollection<LoadCombo>
    {

    }
    public class LoadCombo
    {
        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public LoadCombo(string nombreCombo)
        {
            Nombre = nombreCombo;
        }

        public override string ToString()
        {
            return Nombre;
        }

        public static LoadComboCollection GetCombos(cSapModel modeloSap)
        {
            LoadComboCollection col = new LoadComboCollection();
            int num = 1;
            string[] nombres = new string[1];
            int ret = modeloSap.RespCombo.GetNameList(ref num, ref nombres);
            foreach (string nombreCombo in nombres)
            {
                col.Add(new LoadCombo(nombreCombo));
            }
            return col;
        }
    }
}
