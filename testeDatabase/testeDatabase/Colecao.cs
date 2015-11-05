using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testeDatabase
{
    class Colecao : INotifyPropertyChanged
    {
        private List<String> vet;
        public Colecao()
        {
            vet = new List<String>();
            vet.Add("casa");
            vet.Add("carro");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

