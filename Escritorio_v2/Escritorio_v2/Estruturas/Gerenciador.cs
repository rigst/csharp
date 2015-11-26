using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio_v2
{
    public class Gerenciador
    {
        private List<Condominio> condominios;

        public List<Condominio> Condominios { get { return condominios; } }
        public Gerenciador() {
            condominios = new List<Condominio>();
            
            Condominio c1 = new Condominio("COND 1");
            Condominio c2 = new Condominio("COND 2");
            Condominio c3 = new Condominio("COND 3");
            Condominio c4 = new Condominio("COND 4");
            condominios.Add(c1);
            condominios.Add(c2);
            condominios.Add(c3);
            condominios.Add(c4);
            Bloco b1 = new Bloco(1);
            Bloco b2 = new Bloco(2);
            Bloco b3 = new Bloco(3);
            c1.addBloco(b1);
            c1.addBloco(b2);
            c2.addBloco(b3);
            Apartamento ap1 = new Apartamento(101);
            Apartamento ap2 = new Apartamento(102);
            Apartamento ap3 = new Apartamento(103);
            b1.addApartamento(ap1);
            b2.addApartamento(ap2);
            b2.addApartamento(ap3);
            ap1.Nome = "Maria francisca";
            ap2.Nome = "Jose carlos";
            ap1.Box = "02";
            ap2.addCota("Maria", 1000, 500, new Data("12/11/97"), new Data("27/01/97"), " ");
        }
        public void addCondominio(String nome)
        {
            condominios.Add(new Condominio(nome));
        }
        public void addCondominio(Condominio c)
        {
            condominios.Add(c);
        }

        public Condominio getCondominio(string s)
        {
            foreach(var c in Condominios)
            {
                if (c.Nome == s) return c;
            }
            return null;
        }
        
        public override String ToString()
        {
            return "Gerenciador";
        }
    }
}
