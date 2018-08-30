using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSenales
{
    class SenalRampa
    {
        public double amplitud { get; set; }
        public double fase { get; set; }
        public double frecuencia { get; set; }
        public double amplitudMaxima { get; set; }

        public List<Muestra> Muestras { get; set; }

        public SenalRampa()
        {
            amplitud = 1.0;
            fase = 0.0;
            frecuencia = 1.0;
            amplitudMaxima = 0.0;
            Muestras = new List<Muestra>();
        }

        public double evaluar(double tiempo)
        {
            double resultado;

            if(tiempo >= 0)
            {
                resultado = tiempo;
            }
            else
            {
                resultado = 0;
            }

            return resultado;
        }
    }
}
