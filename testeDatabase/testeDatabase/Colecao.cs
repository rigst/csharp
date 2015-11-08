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
        private List<string> vet;
        public Colecao()
        {
            vet = new List<string>();
            vet.Add("casa");
            vet.Add("carro");
        }

        public void adiciona(string s) { vet.Add(s); }
        public List<string> getVet() { return vet; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

