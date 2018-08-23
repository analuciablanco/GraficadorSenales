using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSenales
{
    class SenalSenoidal
    {
        public double amplitud { get; set; }
        public double fase { get; set; }
        public double frecuencia { get; set; }

        public SenalSenoidal()
        {
            amplitud = 1.0;
            fase = 0.0;
            frecuencia = 1.0;
        }

        public SenalSenoidal(double Amplitud, double Fase, double Frecuencia)
        {
            amplitud = Amplitud;
            fase = Fase;
            frecuencia = Frecuencia;
        }

        public double evaluar(double tiempo)
        {
            double resultado;
            resultado = amplitud * Math.Sin((2 * Math.PI * tiempo * frecuencia) + fase);

            return resultado;
        }
    }
}
