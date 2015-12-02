using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio_v2
{
    public class Condominio
    {
        private List<Bloco> blocos;
        public List<Bloco> Blocos { get { return blocos; } }

        public string Nome
        {
            get; set;
        }
        public Condominio(string nome) : base()
        {
            Nome = nome;
            blocos = new List<Bloco>();
        }

        public void addBloco(int numero)
        {
            blocos.Add(new Bloco(numero));
        }

        public void addBloco(Bloco bloco)
        {
            blocos.Add(bloco);
        }

        public Bloco getBloco(int numero)
        {
            foreach (var v in blocos)
            {
                if (v.getNumero() == numero) return v;
            }
            return null;
        }

        public override string ToString()
        {
            return "Condomínio " + Nome + "\n";            
        }

        public string getRelatorio()
        {
            String msg = "CONDOMINIO " + Nome + "\n";
            msg += "BLOCOS\nBlocos";
            foreach (var v in blocos)
            {
                msg += v.getRelatorio() + "\n";
            }
            return msg;
        }
    }

}
