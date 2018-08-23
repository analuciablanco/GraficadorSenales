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
            double amplitud = double.Parse(txtAmplitud.Text);
            double fase = double.Parse(txtFase.Text);
            double frecuencia = double.Parse(txtFrecuencia.Text);
            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecMuestreo = double.Parse(txtFrecMuestreo.Text);

            double periodoMuestreo = 1 / frecMuestreo;

            SenalSenoidal senal = new SenalSenoidal(amplitud, fase, frecuencia);

            plnGrafica.Points.Clear();

            for(double i=tiempoInicial; i<=tiempoFinal; i+=periodoMuestreo)
            {
                plnGrafica.Points.Add(new Point(i * scrContenedor.Width, 
                    senal.evaluar(i) * ((scrContenedor.Height/2.0)-30)*-1 + (scrContenedor.Height/2)));
            }
    }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtAmplitud.Text = "1";
            txtFase.Text = "0";
            txtFrecuencia.Text = "1";
            txtTiempoInicial.Text = "0";
            txtTiempoFinal.Text = "1";
            txtFrecMuestreo.Text = "1000";
        }
    }
}
