using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio_v2
{

    public class Apartamento
    {
        private string nome;
        private int numero;
        private string box;
        private List<Cota> cotas;
        private List<Acordo> acordos;
        private List<Processo> processos;

        public string Nome { get { return nome; } set { nome = value; } }
        public string Box { get { return box; } set { box = value; } }
        public int Numero { get { return numero; } set { numero = value; } }

        public List<Cota> Cotas { get { return cotas; } }

        public List<Acordo> Acordos { get { return acordos; } }

        public Apartamento(int numero)
        {
            nome = " "; this.box = ""; this.numero = numero;
            cotas = new List<Cota>(); acordos = new List<Acordo>(); processos = new List<Processo>();
        }

        public int getNumero() { return numero; }

        public void setNome(String nome) { this.nome = nome; }

        public void setBox(string box) { this.box = box; }

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


        public override String ToString()
        {
            return "Apartamento " + numero;
        }

        public string toStringEspecifico()
        {
            return "Apartamento " + numero + " :\n" + "NOME : " + nome + "\nCOTAS :\n" + cotas + "\nACORDOS :\n" + acordos + "\nPROCESSOS :\n" + processos;

        }


    }

}
