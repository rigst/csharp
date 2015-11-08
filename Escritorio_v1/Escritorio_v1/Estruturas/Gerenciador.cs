using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Escritorio_v1
{
    class Gerenciador : ObservableCollection<Condominio>
    {
        public Gerenciador() : base() { }
        public void addCondominio(String nome)
        {
            Add(new Condominio(nome));
        }
        public void addCondominio(Condominio c)
        {
            Add(c);
        }
    }
}
