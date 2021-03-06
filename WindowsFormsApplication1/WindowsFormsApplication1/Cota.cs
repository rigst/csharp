﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class Cota
    {
        public double valPago;
        public Data dataVencimento, dataPagamento;
        public String obs;

        public Cota(double valP, Data dVenc, Data dPag, String obs)
        {
            valPago = valP; dataVencimento = dVenc; dataPagamento = dPag; this.obs = obs;
        }

        public Data getDataVencimento() { return dataVencimento; }

        public String toString()
        {
            return "\tData Vencimento: " + dataVencimento +
                    "\tValor Pago : " + valPago + "\tData Pagamento : " + dataPagamento +
                        "\tObservação : " + obs;
        }
    }

}

