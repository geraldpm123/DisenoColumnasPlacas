using ETABS2016;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisenoColumnasPlacas.Models
{
    public class NivelCollection : ObservableCollection<Nivel>
    {

    }
    public class Nivel
    {
        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public Nivel(string nombreNivel)
        {
            Nombre = nombreNivel;
        }
        public override string ToString()
        {
            return Nombre;
        }

        public static NivelCollection GetNivelesLabel(cSapModel modeloSap,Label label)
        {
            int num = 1;
            string[] nombres = new string[1];
            string[] labels = new string[1];
            string[] storys = new string[1];
            int ret = modeloSap.FrameObj.GetLabelNameList(ref num, ref nombres, ref labels, ref storys);
            List<string> nivelsLabel = new List<string>();
            for (int i = 0; i < nombres.Length; i++)
            {
                if (labels[i] == label.Nombre && !nivelsLabel.Exists(x => x == storys[i]))
                {
                    nivelsLabel.Add(storys[i]);
                }
            }
            
            NivelCollection nivelesLabel = new NivelCollection();
            foreach (string nameStory in nivelsLabel.OrderBy(x => x))
            {
                nivelesLabel.Add(new Nivel(nameStory));
            }

            return nivelesLabel;
        }
    }
}
