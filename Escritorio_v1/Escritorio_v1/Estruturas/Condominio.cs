using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio_v1
{
    public class Condominio : ObservableCollection<Bloco>
    {
        private String nome;
        public Condominio(String nome) : base()
        {
            this.nome = nome;
        }

        public void addBloco(int numero)
        {
            Add(new Bloco(numero));
        }

        public void addBloco(Bloco bloco)
        {
            Add(bloco);
        }

        public Bloco getBloco(int numero)
        {
            foreach (var v in Items)
            {
                if (v.getNumero() == numero) return v;
            }
            return null;
        }

        public override String ToString()
        {
            return "CONDOMINIO " + nome + "\n";
           
            
        }

        public String ToStringEspecifico()
        {
            String msg = "CONDOMINIO " + nome + "\n";
            msg += "BLOCOS\nBlocos";
            /*   foreach (var v in blocos)
               {
                   msg += v + "\n";
               }*/
            return msg;
        }
    }

}
