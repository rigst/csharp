using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio_v2
{

    public class Apartamento
    {
        private String nome;
        private int numero, box;
        private List<Cota> cotas;
        private List<Acordo> acordos;
        private List<Processo> processos;

        public Apartamento(int numero)
        {
            nome = " "; this.box = -1; this.numero = numero;
            cotas = new List<Cota>(); acordos = new List<Acordo>(); processos = new List<Processo>();
        }

        public int getNumero() { return numero; }

        public void setNome(String nome) { this.nome = nome; }

        public void setBox(int box) { this.box = box; }

        /******************************COTAS*****************************************************/

        public void addCota(String nome, double valOriginal, double valPago, Data dataVencimento, Data dataPagamento, String obs)
        {
            cotas.Add(new Cota(valPago, dataVencimento, dataPagamento, obs));
        }

        public void addCota(Cota cota)
        {
            cotas.Add(cota);
        }

        public void delCota(Cota cota)
        {
            cotas.Remove(cota);
        }

        /******************************ACORDOS****************************************************/

        public void addAcordo(Acordo acordo)
        {
            acordos.Add(acordo);
        }


        public bool addItemTabela(String devedor, String parcela, double valO, double valP, Data vencO, Data dataP)
        { 
            foreach(var v in acordos)
            {
                if(v.getDevedor().Equals(devedor))
                {
                    v.addItemTabela(parcela, valO, valP, vencO, dataP);
                    return true;
                }
            }
            return false;
        }

        public bool addItemDebitos(String devedor, Data c)
        {

            foreach (var v in acordos)
            {
                if (v.getDevedor().Equals(devedor))
                {
                    v.addItemDebitos(c);
                    return true;
                }
            }
            return false;
        }

        /******************************PROCESSOS****************************************************/
        public void addProcesso(String reu, String especie, int numProcesso, double valorAjuizado, Data dataAjuizamento,
                Data periodo, Data ultMov)
        {
            Processo p = new Processo(reu, especie, numProcesso, valorAjuizado, dataAjuizamento, periodo, ultMov);
            processos.Add( p);
        }

        public bool delProcesso(int numProcesso)
        {
            foreach (var v in processos)
            {
                if (v.getNumProcesso() == numProcesso)
                {
                    processos.Remove(v);
                    return true;
                }
            }
            return false;
        }

        /*******************************************************************************************/


        public String toString()
        {
            if (box != -1) return "Apartamento " + numero + " - Box " + box + " :\n" + "NOME : " + nome + "\nCOTAS :\n" + cotas + "\nACORDOS :\n" + acordos + "\nPROCESSOS :\n" + processos;
            else return "Apartamento " + numero + " :\n" + "NOME : " + nome + "\nCOTAS :\n" + cotas + "\nACORDOS :\n" + acordos + "\nPROCESSOS :\n" + processos;

        }


    }

}
