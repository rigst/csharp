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
            return "BLOCO 0" + numero;
        }

        public String ToStringEspecifico()
        {
            String msg = "--BLOCO " + numero + "--\n";
            foreach (var v in apartamentos)
            {
                msg += v + "\n";
            }
            return msg;
        }
    }

}
