using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio_v2
{
    public class Gerenciador : INotifyPropertyChanged
    {
        private List<Condominio> condominios;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<Condominio> Condominios { get { return condominios; } set { condominios = value; } }

        public Gerenciador() {
            condominios = new List<Condominio>();
            
            Condominio c1 = new Condominio("Águas Claras");
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
            ap2.addCota("Maria", 1000, 500, new Data("12/11/97"), new Data("27/01/97"), "observacao");
            ap2.addCota("Silvia", 200, 100, new Data("02/11/97"), new Data("27/11/97"), "observacao");
            ap2.addCota("Silvia", 1000, 100, new Data("02/11/97"), new Data("27/11/97"), "observacao");
            ap2.addAcordo(new Acordo("Catia Jose", "1124/568-4", "IBM", new Data("13/11/87"), new Data("11/11/87"), new Data("11/12/87"), 1200, 200, 4));
            ap2.addAcordo(new Acordo("Maria Oliveira", "1124/568-4", "IBM", new Data("13/11/87"), new Data("11/11/87"), new Data("11/12/87"), 1200, 200, 4));
            ap2.addAcordo(new Acordo("Claudia Oliveira", "1124/568-4", "IBM", new Data("13/11/87"), new Data("11/11/87"), new Data("11/12/87"), 1200, 200, 4));
            Processo proc = new Processo("Simão", "Dinheiro", "1136-9", 2000, new Data("13/11"), new Data("13/5/69"), new Data("05/14/05"));
           proc.addAjuizado(new Data("11/12/12"));
            proc.addAjuizado(new Data("13/13/13"));
            proc.addCustas(new Data("25/12/15"), 15000);
            proc.addCustas(new Data("01/01/16"), 20000);
            ap2.addProcesso(proc);
            ap2.addProcesso("Carlos", "Dinheiro", "1-3256", 2000, new Data("13/11"), new Data("13/5/69"), new Data("05/14/05"));
            ap2.addProcesso("Roberto", "Dinheiro", "8.9798", 2000, new Data("13/11"), new Data("13/5/69"), new Data("05/14/05"));
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
        
        public string getRelatorio()
        {
            string msg = "";
            foreach(Condominio cond in Condominios)
            {
                msg += cond.getRelatorio() + "\n";
            }
            return msg;
        }

        public override String ToString()
        {
            return "Gerenciador";
        }
    }
}
