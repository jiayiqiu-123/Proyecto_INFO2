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
        FlightPlanList ListaVuelos;
        FlightPlan vuelo1;
        FlightPlan vuelo2;
        float distanciaSeguridad;
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
            f.ShowDialog(); 
            ListaVuelos = f.GetFlightPlanList();
        }

        private void parámetrosDeSeguridadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormParametros f = new FormParametros();
            f.ShowDialog();
            // Si los datos son válidos, los guardamos aquí
            if (f.GetDistSeguridad() > 0 && f.GetTiempoCiclo() > 0)
            {
                distanciaSeguridad = f.GetDistSeguridad();
                tiempoCiclo = f.GetTiempoCiclo();
                ListaVuelos.SetDistanciaSegurdad(distanciaSeguridad);
                ListaVuelos.SetTiempoCiclo(tiempoCiclo);
                MessageBox.Show("Parámetros guardados.");
            }
        }
        private void espacioAéreoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListaVuelos.GetNumber(); i++)
            {
                FlightPlan vuelo = ListaVuelos.GetFlightPlan(i);
                if (vuelo == null)
                {
                    MessageBox.Show("Primero introduce los planes de vuelo.");
                    return;
                }
            }
            if (tiempoCiclo <= 0)
            {
                MessageBox.Show("Primero introduce el tiempo del ciclo.");
                return;
            }
            FormSimulacion formulario = new FormSimulacion();
            formulario.SetDSeguridad(distanciaSeguridad);
            formulario.SetTiempoCiclo(tiempoCiclo);
            formulario.SetListaVuelos(ListaVuelos);
            formulario.ShowDialog();
        }
        private void btnPruebas_Click(object sender, EventArgs e)
        {
            ListaVuelos = new FlightPlanList();
            vuelo1 = new FlightPlan("VLG1234", 50, 100, 500, 300, 10);
            vuelo2 = new FlightPlan("IBE1234", 50, 300, 500, 100, 10);

            int resultado = ListaVuelos.AddFlightPlan(vuelo1);
            int resultado2 = ListaVuelos.AddFlightPlan(vuelo2);

            distanciaSeguridad = 20;
            tiempoCiclo = 1;
            ListaVuelos.SetDistanciaSegurdad(distanciaSeguridad);
            ListaVuelos.SetTiempoCiclo(tiempoCiclo);


            FormSimulacion formulario = new FormSimulacion();
            formulario.SetDSeguridad(distanciaSeguridad);
            formulario.SetTiempoCiclo(tiempoCiclo);
            formulario.SetListaVuelos(ListaVuelos);
            formulario.ShowDialog();
        }
    }
}
