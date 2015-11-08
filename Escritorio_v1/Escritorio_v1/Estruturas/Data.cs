using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio_v1
{
    public class Data
    {
        private int dia, mes, ano;

        public Data(String dataStr)
        {
            String[] data = dataStr.Split('/');
            if (data.Length == 3)
            { //dia mes e ano
                dia = Int32.Parse(data[0]);
                mes = Int32.Parse(data[1]);
                ano = Int32.Parse(data[2]);
            }
            else
            {
                //mes e ano
                dia = 0;
                mes = Int32.Parse(data[0]);
                ano = Int32.Parse(data[1]);
            }
        }

        public int getDia() { return dia; }
        public int getMes() { return mes; }
        public int getAno() { return ano; }

        public String toString()
        {
            if (dia != 0) return dia + "/" + mes + "/" + ano;
            else return mes + "/" + ano;
        }

        public bool after(Data d)
        {
            if (this.ano > d.getAno()) return true;  //ano desse é maior
            else if (d.getAno() == this.ano)
            { //anos são iguais
                if (this.mes > d.getMes()) return true; //mes desse é maior
                else if (this.mes == d.getMes())
                { //meses são iguais
                    if (this.dia > d.getDia()) return true; //dia desse é maior
                    return false; //senão vem antes
                }
                return false; //mes desse é menor
            }
            return false; //ano do outro é menor
        }

    }

}
