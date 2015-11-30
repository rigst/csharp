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
        private List<Bloco> blocos;
        private List<Apartamento> apartamentos;
        private Apartamento apAtual;

        public string NomeCondominio { get { return nome; } set { nome = "Condomínio " + value; } }
        public List<Bloco> Blocos {
            get
            {
                return blocos;
            }
            set
            {
                blocos = value;
                if(PropertyChanged != null)
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
