using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio_v2
{

    public class Processo
    {
        private string reu, especie;
        private string numProcesso;
        private double valorAjuizado;
        private Data dataAjuizamento, periodo, ultMov;
        private List<Data> ajuizado;
        private List<Custa> custas;

        public string Reu { get { return reu; } set { reu = value; } }
        public string NumProcesso { get { return numProcesso; } set { numProcesso = value; } }
        public double ValorAjuizado { get { return valorAjuizado; } set { valorAjuizado = value;} }
        public string Especie { get { return especie; } set { especie = value; } }
        public Data DataAjuizamento { get { return dataAjuizamento; } set { dataAjuizamento = value; } }
        public Data Periodo { get { return periodo; } set { periodo = value; } }
        public Data UltimoMovimento { get { return ultMov; } set { ultMov = value; } }
        public List<Data> Ajuizados { get { return ajuizado; } set { ajuizado = value; } }
        public List<Custa> Custas { get { return custas; } set { custas = value; } }

        public class Custa
        {
            public Data Data { get; set; }
            public double Valor { get; set; }
            public Custa() { }
            public Custa(Data d, double v) { this.Data = d; this.Valor = v; }
            public override string ToString() {
                return Data.ToString() + "\t" + Valor;                
            }
        }

        public Processo() { }

        public Processo(String reu, String especie, string numProcesso, double valorAjuizado, Data dataAjuizamento,
                Data periodo, Data ultMov)
        {
            this.reu = reu; this.especie = especie; this.numProcesso = numProcesso; this.valorAjuizado = valorAjuizado;
            this.dataAjuizamento = dataAjuizamento; this.periodo = periodo; this.ultMov = ultMov;
            ajuizado = new List<Data>();
            custas = new List<Custa>();
        }

        public void addAjuizado(Data d)
        {
            ajuizado.Add(d);
        }

        public void addCustas(Data d, double v)
        {
            custas.Add(new Custa(d, v));
        }

        public string getNumProcesso() { return numProcesso; }

        public override string ToString()
        {
            return Cota.espacos(2) + ultMov + Cota.espacos(4) + numProcesso + Cota.espacos(4) + reu;
        }

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


        public string getRelatorio()
        {
            return ToString();
        }
    }

}
