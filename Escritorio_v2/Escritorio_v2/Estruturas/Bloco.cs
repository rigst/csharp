using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio_v2
{
    public class Bloco
    {
        private int numero;
        private List<Apartamento> apartamentos;
        public List<Apartamento> Apartamentos { get { return apartamentos; } }

        public int Numero { get { return numero; } set { numero = value; } }

        public Bloco(int numero)
        {
            this.numero = numero; apartamentos = new List<Apartamento>();
        }

        public void addApartamento(int num)
        {
            apartamentos.Add(new Apartamento(num));
        }

        public void addApartamento(Apartamento apartamento)
        {
            apartamentos.Add(apartamento);
        }

        public Apartamento getApartamento(int numAp)
        {
            foreach (var v in apartamentos)
            {
                if (v.getNumero() == numAp) return v;
            }
            return null;
        }

        public int getNumero() { return numero; }

        public override String ToString()
        {
            if(Numero < 10) return "Bloco 0" + numero;
            return "Bloco " + numero;
        }

        public string getRelatorio()
        {
            String msg = "--BLOCO " + numero + "--\n";
            foreach (var v in apartamentos)
            {
                msg += v.getRelatorio() + "\n";
            }
            return msg;
        }
    }

}
