using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSenales
{
    abstract class Senal
    {
        public List<Muestra> Muestras { get; set; }
        public double amplitudMaxima { get; set; }
        public double TiempoInicial { get; set; }
        public double TiempoFinal { get; set; }
        public double FrecMuestreo { get; set; }

        public abstract double evaluar(double tiempo);

        public void construirSenalDigital()
        {
            double periodoMuestreo = 1 / FrecMuestreo;

            for (double i = TiempoInicial; i <= TiempoFinal; i += periodoMuestreo)
            {
                double valorMuestra = evaluar(i);

                if (Math.Abs(valorMuestra) > amplitudMaxima)
                {
                    amplitudMaxima = Math.Abs(valorMuestra);
                }

                //se van añadiendo las muestras a la lista.
                Muestras.Add(new Muestra(i, valorMuestra));
            }
        }
    }
}
