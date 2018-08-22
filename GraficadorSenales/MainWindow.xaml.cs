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

            plnGrafica.Points.Add(new Point(50, 20));
            plnGrafica.Points.Add(new Point(200, 25));
            plnGrafica.Points.Add(new Point(250, 40));
            plnGrafica.Points.Add(new Point(400, 45));
            plnGrafica.Points.Add(new Point(450, 30));
            plnGrafica.Points.Add(new Point(600, 35));
            plnGrafica.Points.Add(new Point(650, 60));
            plnGrafica.Points.Add(new Point(800, 65));
            plnGrafica.Points.Add(new Point(850, 40));
            plnGrafica.Points.Add(new Point(1000, 45));
            plnGrafica.Points.Add(new Point(1050, 70));
        }
    }
}
