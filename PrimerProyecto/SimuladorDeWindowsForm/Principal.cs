using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightLib;

namespace SimuladorDeWindowsForm
{
    public partial class Principal : Form
    {
        // Variables donde se guardan los datos introducidos
        FlightPlan vuelo1;
        FlightPlan vuelo2;
        double distanciaSeguridad;
        double tiempoCiclo;


        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {

        }

        private void menuPlanesDeVueloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPlanesDeVuelo f = new FormPlanesDeVuelo();
            f.ShowDialog(); // Abre la ventana y espera a que se cierre

            // Si se crearon correctamente los vuelos, los guardamos aquí
            if (f.p1 != null && f.p2 != null)
            {
                vuelo1 = f.p1;
                vuelo2 = f.p2;
                MessageBox.Show("Planes de vuelo guardados.");
            }
        }

        private void parámetrosDeSeguridadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormParametros f = new FormParametros();
            f.ShowDialog();

            // Si los datos son válidos, los guardamos aquí
            if (f.distSeguridad > 0 && f.tiempoCiclo > 0)
            {
                distanciaSeguridad = f.distSeguridad;
                tiempoCiclo = f.tiempoCiclo;
                MessageBox.Show("Parámetros guardados.");
            }
        }

        private void espacióAerioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vuelo1 == null || vuelo2 == null)
            {
                MessageBox.Show("Primero introduce los planes de vuelo.");
                return;
            }
            if (tiempoCiclo <= 0)
            {
                MessageBox.Show("Primero introduce el tiempo del ciclo.");
                return;
            }

            FormSimulacion formulario = new FormSimulacion(vuelo1, vuelo2, tiempoCiclo);
            formulario.ShowDialog();
        }
    }
}
