using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio_v2
{
    public class Cota
    {
        public double valPago;
        public Data dataVencimento, dataPagamento;
        public string obs;

        public double ValorPago { get { return valPago; } set { valPago = value; } }
        public Data DataVencimento { get { return dataVencimento; } set { dataVencimento = value; } }
        public Data DataPagamento { get { return dataPagamento; } set { dataPagamento = value; } }
        public string Observacao { get { return obs; } set { obs = value; } }

        public Cota() { }

        public Cota(double valP, Data dVenc, Data dPag, String obs)
        {
            valPago = valP; dataVencimento = dVenc; dataPagamento = dPag; this.obs = obs;
        }

        public Data getDataVencimento() { return dataVencimento; }

        public static string espacos(int n)
        {
            string s = "";
            for(int i=0; i< n; i++)
            {
                s += "\t";
            }
            return s;
        }

        public override string ToString()
        {
            string msg = espacos(1) + "" + dataVencimento;
            msg += espacos(2);
            msg += "\tR$" + valPago;
            msg += espacos(2);
            msg += "" + dataPagamento;
            msg += espacos(2);
            msg += "" + obs;

            return msg;
        }

        public string getRelatorio()
        {
            return ToString();
        }
    }

}

