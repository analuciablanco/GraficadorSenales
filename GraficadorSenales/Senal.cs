﻿using System;
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

        public void escalar(double factor)
        {
            //por cara muestra se va a realizar esto
            foreach (Muestra muestra in Muestras)
            {
                //se multiplica por Y para escalar, no por X para conservar el instante de tiempo
                muestra.y *= factor;
            }
        }

        public void actualizarAmplitudMaxima()
        {
            amplitudMaxima = 0;

            foreach (Muestra muestra in Muestras)
            {
                if (Math.Abs(muestra.y) > amplitudMaxima)
                {
                    amplitudMaxima = Math.Abs(muestra.y);
                }
            }
        }

        public void desplazar(double factor)
        {
            //por cara muestra se va a realizar esto
            foreach (Muestra muestra in Muestras)
            {
                //se suma en Y para desplazar, no por X para conservar el instante de tiempo
                muestra.y += factor;
            }
        }

        public void truncar(double umbral)
        {
            //por cara muestra se va a realizar esto
            foreach (Muestra muestra in Muestras)
            {
                //
                if (muestra.y > umbral) muestra.y = umbral;
                else if (muestra.y < (umbral * -1)) muestra.y = umbral * -1;
            }
        }

        public static Senal sumar(Senal suma1, Senal suma2)
        {
            //construimos la señal resultado
            SenalPersonalizada resultado = new SenalPersonalizada ();
            //sumamos muestra por muestra
            resultado.TiempoInicial = suma1.TiempoInicial;
            resultado.TiempoFinal = suma1.TiempoFinal;
            resultado.FrecMuestreo = suma1.FrecMuestreo;
            //recorremos 1 lista de muestras y a la 2 señal accedemos por un indice
            int indice = 0;
            foreach (Muestra muestra in suma1.Muestras)
            {
                Muestra muestraResultado = new Muestra();
                muestraResultado.x = muestra.x;
                muestraResultado.y = muestra.y + suma2.Muestras[indice].y;
                indice++;
                resultado.Muestras.Add(muestraResultado);
            }
            return resultado;
        }
    }
}
