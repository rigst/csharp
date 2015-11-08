using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio_v1
{

    public class Processo
    {
        private String reu, especie;
        private int numProcesso;
        private double valorAjuizado;
        private Data dataAjuizamento, periodo, ultMov;
        private List<Data> ajuizado;
        private List<Custa> custas;

        class Custa
        {
            public Data Data { get; set; }
            public double Valor { get; set; }
            public Custa(Data d, double v) { this.Data = Data; this.Valor = v; }
            public override String ToString() { return this.Data + "\t" + this.Valor; }
        }

        public Processo(String reu, String especie, int numProcesso, double valorAjuizado, Data dataAjuizamento,
                Data periodo, Data ultMov)
        {
            this.reu = reu; this.especie = especie; this.numProcesso = numProcesso; this.valorAjuizado = valorAjuizado;
            this.dataAjuizamento = dataAjuizamento; this.periodo = periodo; this.ultMov = ultMov;
            ajuizado = null; //chamar metodo
            custas = null; //chamar metodo
        }

        public void addAjuizado(Data d)
        {
            ajuizado.Add(d);
        }

        public void addCustas(Data d, double v)
        {
            custas.Add(new Custa(d, v));
        }

        public int getNumProcesso() { return numProcesso; }


        public String toString()
        {
            String msg = "PROCESSO :\n" + "Reu : " + reu + "\nEspecie : " + especie + "\nNumero Processo : " + numProcesso +
                    "\nValor Ajuizado : " + valorAjuizado + "\nData Ajuizamento : " + dataAjuizamento +
                    "\nPeriodo : " + periodo + "\nUltimo Movimento : " + ultMov + "Ajuizados : \n";
            foreach (var v in ajuizado)
            {
                msg += "Ajuizado : " + v;
            }
            msg += "Custas :\n";
            foreach (var v in custas)
            {
                msg += "Custas : " + v;
            }
            return msg;
        }
    }

}
