using DisenoColumnasPlacas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETABS2016;
using System.Windows;
using System.Windows.Input;
using OxyPlot;

namespace DisenoColumnasPlacas.ViewModels
{
    class DisenoColumnasViewModel : NotifyBase
    {
        private cSapModel _modeloSap;
        private Label _currentLabelColumn;
        private Nivel _currentNivel;
        private LoadCombo _currentCombo1;
        private LoadCombo _currentCombo2;
        private LoadCombo _currentCombo3;
        private LoadCombo _currentCombo4;
        private LoadCombo _currentCombo5;
        private LoadComboCollection _listaCombos = new LoadComboCollection();
        private LabelCollection _listaLabelsColumns = new LabelCollection();
        private NivelCollection _listaNiveles = new NivelCollection();
        private StationFrameCollection _listaStations = new StationFrameCollection();
        private FrameColumn _currentFrame;
        private StationFrame _currentStation;
        ResultFrameCollection _resultadosColumn = new ResultFrameCollection();
        private string _rutaDI = "";

        public cSapModel ModeloSap
        {
            get { return this._modeloSap; }
            set
            {
                _modeloSap = value;
                ListaCombos = LoadCombo.GetCombos(value);
                ListaLabelsColumns = Label.GetLabelColumns(value);
            }
        }
        public LoadCombo CurrentCombo1
        {
            get { return this._currentCombo1; }
            set
            {
                if (this._currentCombo1 != value)
                {
                    this._currentCombo1 = value;
                    this.OnPropertyChanged(nameof(CurrentCombo1));
                }
            }
        }
        public LoadCombo CurrentCombo2
        {
            get { return this._currentCombo2; }
            set
            {
                if (this._currentCombo2 != value)
                {
                    this._currentCombo2 = value;
                    this.OnPropertyChanged(nameof(CurrentCombo2));
                }
            }
        }
        public LoadCombo CurrentCombo3
        {
            get { return this._currentCombo3; }
            set
            {
                if (this._currentCombo3 != value)
                {
                    this._currentCombo3 = value;
                    this.OnPropertyChanged(nameof(CurrentCombo3));
                }
            }
        }
        public LoadCombo CurrentCombo4
        {
            get { return this._currentCombo4; }
            set
            {
                if (this._currentCombo4 != value)
                {
                    this._currentCombo4 = value;
                    this.OnPropertyChanged(nameof(CurrentCombo4));
                }
            }
        }
        public LoadCombo CurrentCombo5
        {
            get { return this._currentCombo5; }
            set
            {
                if (this._currentCombo5 != value)
                {
                    this._currentCombo5 = value;
                    this.OnPropertyChanged(nameof(CurrentCombo5));
                }
            }
        }
        public Label CurrentLabelColumn
        {
            get { return this._currentLabelColumn; }
            set {
                if (this._currentLabelColumn != value)
                {
                    this._currentLabelColumn = value;
                    this.OnPropertyChanged(nameof(CurrentLabelColumn));
                    ListaNiveles = Nivel.GetNivelesLabel(_modeloSap, value);
                    CurrentFrame =
                        FrameColumn.ObtenerColumnasPorLabelYNivel(ModeloSap, CurrentLabelColumn, CurrentNivel);
                }
            }
        }
        public Nivel CurrentNivel
        {
            get { return this._currentNivel; }
            set
            {
                if (this._currentNivel != value)
                {
                    this._currentNivel = value;
                    this.OnPropertyChanged(nameof(CurrentNivel));
                    CurrentFrame = 
                        FrameColumn.ObtenerColumnasPorLabelYNivel(ModeloSap, CurrentLabelColumn, CurrentNivel);
                }
            }
        }
        public FrameColumn CurrentFrame
        {
            get { return this._currentFrame; }
            set
            {
                if (this._currentFrame != value)
                {
                    this._currentFrame = value;
                    this.OnPropertyChanged(nameof(CurrentFrame));
                    ListaStations = StationFrame.GetStationsFrame(ModeloSap, value);
                }
            }
        }
        public StationFrame CurrentStation
        {
            get { return this._currentStation; }
            set
            {
                if (this._currentStation != value)
                {
                    this._currentStation = value;
                    this.OnPropertyChanged(nameof(CurrentStation));
                    ResultadosColumn = ResultFrame.GetResults5Combos(ModeloSap, CurrentFrame, CurrentStation,
                            CurrentCombo1, CurrentCombo2, CurrentCombo3, CurrentCombo4, CurrentCombo5);
                }
            }
        }
        
        public ResultFrameCollection ResultadosColumn
        {
            get { return _resultadosColumn; }
            set {
                this._resultadosColumn = value;
                this.OnPropertyChanged(nameof(this.ResultadosColumn));
                this.OnPropertyChanged(nameof(this.GraficaEje2));
            }
        }
        private DiagramaInteraccion _diagrama;
        public DiagramaInteraccion Diagrama
        {
            get { return _diagrama; }
            set
            {
                this._diagrama = value;
                this.OnPropertyChanged(nameof(this.Diagrama));
                this.OnPropertyChanged(nameof(this.GraficaEje2));
            }
        }

        public PlotModel GraficaEje2
        {
            get { return ResultFrame.GraficaEje2(ResultadosColumn,Diagrama); }
        }
        public string RutaDI
        {
            get { return this._rutaDI; }
            set
            {
                if (this._rutaDI != value)
                {
                    this._rutaDI = value;
                    this.OnPropertyChanged(nameof(RutaDI));
                }
            }
        }

        public LoadComboCollection ListaCombos
        {
            get { return _listaCombos; }
            set
            {
                this._listaCombos = value;
                if (value != null && value.Count > 0)
                {
                    //Valor inicial preasignado
                    this.CurrentCombo1 = value[0];
                    this.CurrentCombo2 = value[0];
                    this.CurrentCombo3 = value[0];
                    this.CurrentCombo4 = value[0];
                    this.CurrentCombo5 = value[0];
                    //Si es posible reasignar combos con nombres predefinidos
                    PreasignarCombos();
                }
                this.OnPropertyChanged(nameof(this.ListaCombos));
            }
        }
        public LabelCollection ListaLabelsColumns
        {
            get { return _listaLabelsColumns; }
            set
            {
                this._listaLabelsColumns = value;
                if (value != null && value.Count > 0)
                {
                    this.CurrentLabelColumn = value[0];
                }
                this.OnPropertyChanged(nameof(this.ListaLabelsColumns));
            }
        }
        public NivelCollection ListaNiveles
        {
            get { return _listaNiveles; }
            set
            {
                this._listaNiveles = value;
                if (value != null && value.Count > 0)
                {
                    this.CurrentNivel = value[0];
                }
                this.OnPropertyChanged(nameof(this.ListaNiveles));
            }
        }
        public StationFrameCollection ListaStations
        {
            get { return _listaStations; }
            set
            {
                this._listaStations = value;
                if (value != null && value.Count > 0)
                {
                    this.CurrentStation = value[0];
                }
                this.OnPropertyChanged(nameof(this.ListaStations));
            }
        }

        public ICommand _vincularModeloSapCommand;
        public ICommand _leerRutaDeDiagramaCommand;
        public ICommand _leerDICommand;
        public ICommand VincularModeloSapCommand
        {
            get
            {
                if (_vincularModeloSapCommand == null)
                {
                    _vincularModeloSapCommand = new CommandBase(param => this.VincularModeloSap());
                }
                return _vincularModeloSapCommand;
            }
        }
        public ICommand LeerRutaDeDiagramaCommand
        {
            get
            {
                if (_leerRutaDeDiagramaCommand == null)
                {
                    _leerRutaDeDiagramaCommand = new CommandBase(param => this.LeerRutaDeDiagrama());
                }
                return _leerRutaDeDiagramaCommand;
            }
        }
        public ICommand LeerDICommand
        {
            get
            {
                if (_leerDICommand == null)
                {
                    _leerDICommand = new CommandBase(param => this.LeerDI());
                }
                return _leerDICommand;
            }
        }
        private void VincularModeloSap()
        {
            try
            {
                string texto = "";
                ModeloSap = FileETABS.RecuperarModeloETABSAbierto(ref texto);
                MessageBox.Show(texto);
            }
            catch (Exception)
            {
                MessageBox.Show("No se ha enonctrado modelo ETABS abierto");
            }
        }
        private void PreasignarCombos()
        {
            foreach (LoadCombo combo in _listaCombos)
            {
                if (combo.Nombre == "COMB1")
                {
                    this.CurrentCombo1 = combo;
                }
                if (combo.Nombre == "COMB2+")
                {
                    this.CurrentCombo2 = combo;
                }
                if (combo.Nombre == "COMB3+")
                {
                    this.CurrentCombo3 = combo;
                }
                if (combo.Nombre == "COMB4+")
                {
                    this.CurrentCombo4 = combo;
                }
                if (combo.Nombre == "COMB5+")
                {
                    this.CurrentCombo5 = combo;
                }
            }
        }
        private void LeerRutaDeDiagrama()
        {
            this.RutaDI = DiagramaInteraccion.GetRutaDeTXTDiagramaIteraccion();
        }
        private void LeerDI()
        {
            Diagrama = new DiagramaInteraccion(RutaDI);
        }
        #region CONTRUCTORES
        
        #endregion
    }
}
