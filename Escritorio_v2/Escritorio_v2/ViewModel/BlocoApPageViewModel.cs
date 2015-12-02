using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio_v2.ViewModel
{
    public class BlocoApPageViewModel : INotifyPropertyChanged
    {

        private string nome = " ";
        private string original = "";
        private List<Bloco> blocos;
        private List<Apartamento> apartamentos;
        private Apartamento apAtual;

        public Cota CotaAux { get; set; }
        public Acordo AcordoAux { get; set; }
        public Processo ProcessoAux { get; set; }

        public string Original { get { return original; } set { original = value; } }
        public string NomeCondominio { get { return nome; } set { original = value;  nome = "Condomínio " + value; } }
        public List<Bloco> Blocos {
            get
            {
                return blocos;
            }
            set
            {
                blocos = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Blocos"));
            }    
        }
        
        public List<Apartamento> Apartamentos
        {
            get
            {
                return apartamentos;
            }
            set
            {
                apartamentos = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Apartamentos"));
            }
        }
        
        public Apartamento ApAtual
        {
            get
            {
                return apAtual;
            }
            set
            {
                apAtual = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ApAtual"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
