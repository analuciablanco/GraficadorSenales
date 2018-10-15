using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraficadorSenales
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGraficar_Click(object sender, RoutedEventArgs e)
        {
            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecMuestreo = double.Parse(txtFrecMuestreo.Text);

            Senal senal;
            Senal senal2;

            //Primer señal.
            switch(cbTipoSenal.SelectedIndex)
            {
                case 0: //Senoidal.
                    double amplitud = double.Parse(((ConfiguracionSenoidal)(panelConfiguracion.Children[0])).txtAmplitud.Text);
                    double fase = double.Parse(((ConfiguracionSenoidal)(panelConfiguracion.Children[0])).txtFase.Text);
                    double frecuencia = double.Parse(((ConfiguracionSenoidal)(panelConfiguracion.Children[0])).txtFrecuencia.Text);

                    senal = new SenalSenoidal(amplitud, fase, frecuencia);
                    break;

                case 1: //Rampa.
                    senal = new SenalRampa();
                    break;

                case 2: //Exponencial.
                    double alfa = double.Parse(((ConfiguracionExponencial)(panelConfiguracion.Children[0])).txtAlfa.Text);

                    senal = new SenalExponencial(alfa);
                    break;

                default: senal = null;
                    break;
            }

            senal.TiempoInicial = tiempoInicial;
            senal.TiempoFinal = tiempoFinal;
            senal.FrecMuestreo = frecMuestreo;

            //Segunda señal.
            switch (cbTipoSenal_2.SelectedIndex)
            {
                case 0: //Senoidal.
                    double amplitud = double.Parse(((ConfiguracionSenoidal)(panelConfiguracion_2.Children[0])).txtAmplitud.Text);
                    double fase = double.Parse(((ConfiguracionSenoidal)(panelConfiguracion_2.Children[0])).txtFase.Text);
                    double frecuencia = double.Parse(((ConfiguracionSenoidal)(panelConfiguracion_2.Children[0])).txtFrecuencia.Text);

                    senal2 = new SenalSenoidal(amplitud, fase, frecuencia);
                    break;

                case 1: //Rampa.
                    senal2 = new SenalRampa();
                    break;

                case 2: //Exponencial.
                    double alfa = double.Parse(((ConfiguracionExponencial)(panelConfiguracion_2.Children[0])).txtAlfa.Text);

                    senal2 = new SenalExponencial(alfa);
                    break;

                default:
                    senal2 = null;
                    break;
            }

            senal2.TiempoInicial = tiempoInicial;
            senal2.TiempoFinal = tiempoFinal;
            senal2.FrecMuestreo = frecMuestreo;

            //Construye señal
            senal.construirSenalDigital();
            senal2.construirSenalDigital();

            //Escalar PRIMERA SEÑAL.
            double factorEscala = double.Parse(txtFactorEscalaAmplitud.Text);
            senal.escalar(factorEscala);
            //Desplazar PRIMERA SEÑAL.
            double factorDesplazar = double.Parse(txtFactorDesplazamiento.Text);
            senal.desplazar(factorDesplazar);
            senal.actualizarAmplitudMaxima();
            //Truncar PRIMERA SEÑAL.
            double factorTruncar = double.Parse(txtUmbral.Text);
            senal.truncar(factorTruncar);

            //Escalar SEGUNDA SEÑAL.
            double factorEscala2 = double.Parse(txtFactorEscalaAmplitud_2.Text);
            senal2.escalar(factorEscala2);
            //Desplazar SEGUNDA SEÑAL.
            double factorDesplazar2 = double.Parse(txtFactorDesplazamiento_2.Text);
            senal2.desplazar(factorDesplazar2);
            senal2.actualizarAmplitudMaxima();
            //Truncar SEGUNDA SEÑAL.
            double factorTruncar2 = double.Parse(txtUmbral_2.Text);
            senal2.truncar(factorTruncar2);

            //Limpia las gráficas.
            plnGrafica.Points.Clear();

            if (senal != null)
            {
                //Recorrer una colección o arreglo.
                foreach (Muestra muestra in senal.Muestras)
                {
                    plnGrafica.Points.Add(new Point((muestra.x - tiempoInicial) * scrContenedor.Width,
                        (muestra.y / senal.amplitudMaxima) * ((scrContenedor.Height / 2.0) - 30) * -1 + (scrContenedor.Height / 2)));
                }

                lblAmplitudMaximaY.Text = senal.amplitudMaxima.ToString("F");
                lblAmplitudMaximaY_Negativa.Text = "-" + senal.amplitudMaxima.ToString("F");
            }

            //Graficando el eje de X
            plnEjeX.Points.Clear();
            //Punto de inicio.
            plnEjeX.Points.Add(new Point(0, (scrContenedor.Height / 2)));
            //Punto de fin.
            plnEjeX.Points.Add(new Point((tiempoFinal - tiempoInicial) * scrContenedor.Width, (scrContenedor.Height / 2)));

            //Graficando el eje de Y
            plnEjeY.Points.Clear();
            //Punto de inicio.
            plnEjeY.Points.Add(new Point(0 - tiempoInicial * scrContenedor.Width, scrContenedor.Height));
            //Punto de fin.
            plnEjeY.Points.Add(new Point(0 - tiempoInicial * scrContenedor.Width, scrContenedor.Height * -1));
        }

        private void cbTipoSenal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (panelConfiguracion != null)
            {
                panelConfiguracion.Children.Clear();

                switch (cbTipoSenal.SelectedIndex)
                {
                    case 0: //Senoidal
                        panelConfiguracion.Children.Add(new ConfiguracionSenoidal());
                        break;

                    case 1: //Rampa
                        break;

                    case 2: //Exponencial
                        panelConfiguracion.Children.Add(new ConfiguracionExponencial());
                        break;

                    default:
                        break;
                }
            }
        }

        private void cbTipoSenal_2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (panelConfiguracion_2 != null)
            {
                panelConfiguracion_2.Children.Clear();

                switch (cbTipoSenal_2.SelectedIndex)
                {
                    case 0: //Senoidal
                        panelConfiguracion_2.Children.Add(new ConfiguracionSenoidal());
                        break;

                    case 1: //Rampa
                        break;

                    case 2: //Exponencial
                        panelConfiguracion_2.Children.Add(new ConfiguracionExponencial());
                        break;

                    default:
                        break;
                }
            }
        }

        //Primera señal
        private void cbAmplitud_Checked(object sender, RoutedEventArgs e)
        {
            txtFactorEscalaAmplitud.IsEnabled = true;

        }
        private void cbAmplitud_UnChecked(object sender, RoutedEventArgs e)
        {
            txtFactorEscalaAmplitud.IsEnabled = false;
            txtFactorEscalaAmplitud.Text = "1";
        }

        private void cbDesplazamiento_Checked(object sender, RoutedEventArgs e)
        {
            txtFactorDesplazamiento.IsEnabled = true;
        }
        private void cbDesplazamiento_UnChecked(object sender, RoutedEventArgs e)
        {
            txtFactorDesplazamiento.IsEnabled = false;
            txtFactorDesplazamiento.Text = "0";
        }

        private void cbUmbral_Checked(object sender, RoutedEventArgs e)
        {
            txtUmbral.IsEnabled = true;
        }
        private void cbUmbral_UnChecked(object sender, RoutedEventArgs e)
        {
            txtUmbral.IsEnabled = false;
            txtUmbral.Text = "1";
        }

        //Segunda señal
        private void cbAmplitud_2_Checked(object sender, RoutedEventArgs e)
        {
            txtFactorEscalaAmplitud_2.IsEnabled = true;
        }
        private void cbAmplitud_2_UnChecked(object sender, RoutedEventArgs e)
        {
            txtFactorEscalaAmplitud_2.IsEnabled = false;
            txtFactorEscalaAmplitud_2.Text = "1";
        }

        private void cbDesplazamiento_2_Checked(object sender, RoutedEventArgs e)
        {
            txtFactorDesplazamiento_2.IsEnabled = true;
        }
        private void cbDesplazamiento_2_UnChecked(object sender, RoutedEventArgs e)
        {
            txtFactorDesplazamiento_2.IsEnabled = false;
            txtFactorDesplazamiento_2.Text = "0";
        }

        private void cbUmbral_2_Checked(object sender, RoutedEventArgs e)
        {
            txtUmbral_2.IsEnabled = true;
        }
        private void cbUmbral_2_UnChecked(object sender, RoutedEventArgs e)
        {
            txtUmbral_2.IsEnabled = false;
            txtUmbral_2.Text = "1";
        }
    }
}
