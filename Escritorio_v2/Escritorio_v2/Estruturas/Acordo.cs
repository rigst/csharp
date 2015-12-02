using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio_v2
{
    public class Acordo
    {
        private string devedor, formaAtualizacao;
        private string processo;
        private Data dataAssinatura, inicio, fim;
        private double valorTotal, valorParcela;
        private int numParcelas;
        private List<Data> debitosInclusos;
        private List<Item> tabela;

        public string Devedor
        {
            get { return devedor; }
            set { devedor = value; }
        }

        public string FormaAtualizacao
        {
            get { return formaAtualizacao; }
            set { formaAtualizacao = value; }
        }

        public string Processo
        {
            get { return processo; }
            set { processo = value; }
        }

        public Data DataAssinatura
        {
            get { return dataAssinatura; }
            set { dataAssinatura = value; }
        }

        public Data Inicio
        {
            get { return inicio; }
            set { inicio = value; }
        }

        public Data Fim
        {
            get { return fim; }
            set { fim = value; }
        }

        public double ValorTotal
        {
            get { return valorTotal; }
            set { valorTotal = value; }
        }
        
        public double ValorParcela
        {
            get { return valorParcela; }
            set { valorParcela = value; }
        }

        public int NumParcelas
        {
            get { return numParcelas; }
            set { numParcelas = value; }
        }
        
        public List<Data> DebitosInclusos
        {
            get { return debitosInclusos; }
            set { debitosInclusos = value; }
        }

        public List<Item> Tabela
        {
            get { return tabela; }
            set { tabela = value; }
        }
        
        public class Item
        {
            private String parcela;
            private double valorOriginal, valorPago;
            private Data vencimentoOriginal, dataPagamento;

            public Item(String parc, double valO, double valP, Data vencO, Data dataP)
            {
                parcela = parc; valorOriginal = valO; valorPago = valP; vencimentoOriginal = vencO; dataPagamento = dataP;
            }
            public String toString()
            {
                return "Parcela : " + parcela + "\tValorOriginal : " + valorOriginal + "\tValorPago : " + valorPago +
                            "\tVencimento Original : " + vencimentoOriginal + "\tData Pagamento : " + dataPagamento;
            }
        }

        public Acordo() { }

        public Acordo(String devedor, String processo, String formaAtualizacao, Data dataAssinatura, Data inicio,
                Data fim, double valorTotal, double valorParcela, int numParcelas)
        {
            this.processo = processo;
            this.devedor = devedor; this.formaAtualizacao = formaAtualizacao; this.dataAssinatura = dataAssinatura;
            this.inicio = inicio; this.fim = fim; this.valorTotal = valorTotal; this.valorParcela = valorParcela;
            this.numParcelas = numParcelas;
            debitosInclusos = new List<Data>(); //chamar funcao para criar debitos
            tabela = new List<Item>(); //chamar funcao para criar tabela
        }

        public void addItemTabela(String parcela, double valO, double valP, Data vencO, Data dataP)
        {
            tabela.Add(new Item(parcela, valO, valP, vencO, dataP));
        }

        public void addItemDebitos(Data d)
        {
            debitosInclusos.Add(d);
        }

        public Data getDataAssinatura() { return dataAssinatura; }

        public String getDevedor() { return devedor; }

        public override string ToString()
        {
            return Cota.espacos(1) + dataAssinatura + Cota.espacos(3) + processo + Cota.espacos(2) + inicio + Cota.espacos(2) + fim;
        }

        public String toString()
        {
            String msg = "ACORDO:\nProcesso " + processo + "\nDevedor : " + devedor + "\nForma Atualização : " + formaAtualizacao +
                    "\nData Assinatura : " + dataAssinatura + "\nInicio : " + inicio + "\nFim : " + fim +
                    "\nValor Total : " + valorTotal + "\nValor Parcela : " + valorParcela +
                    "\nNúmero Parcelas : " + numParcelas + "\nDebitos Inclusos :\n";
            foreach (var v in debitosInclusos)
            {
                msg += "Debito : " + v + "\t";
            }

            msg += "\nTabela : \n";

            foreach (var v in tabela)
            {
                msg += "Item : " + v + "\n";
            }
            return msg;
        }
    }
}
