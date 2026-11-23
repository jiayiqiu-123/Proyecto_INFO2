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
        FlightPlanList ListaVuelos = new FlightPlanList();
        FlightPlan vueloprueba1;
        FlightPlan vueloprueba2;
        float distanciaSeguridad;
        double tiempoCiclo;


        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            //se crea el formulario de usuario y se muestra hasta que se cierre
            FormUser f = new FormUser();
            f.ShowDialog();
            if (f.GetUsuario() == "")
                this.Close();

        }

        private void menuPlanesDeVuelo_Click(object sender, EventArgs e)
        {
            // Abre el formulario de planes de vuelo de forma modal y actualiza ListaVuelos y sus parámetros si la nueva lista tiene vuelos.
            FormPlanesDeVuelo f = new FormPlanesDeVuelo();
            f.SetFlightPlanList(ListaVuelos);
            f.ShowDialog();
            FlightPlanList nueva = f.GetFlightPlanList();
            if (nueva.GetNumber() > 0)
            {
                ListaVuelos = nueva;
                ListaVuelos.SetDistanciaSeguridad(distanciaSeguridad);
                ListaVuelos.SetTiempoCiclo(tiempoCiclo);
            }
        }

        private void parámetrosDeSeguridad_Click(object sender, EventArgs e)
        {
            //Abre el formulario de parámetros de forma modal y actualiza distanciaSeguridad, tiempoCiclo y ListaVuelos si ambos valores son mayores que 0.
            FormParametros f = new FormParametros();
            f.ShowDialog();
            if (f.GetDistSeguridad() > 0 && f.GetTiempoCiclo() > 0)
            {
                distanciaSeguridad = f.GetDistSeguridad();
                tiempoCiclo = f.GetTiempoCiclo();
                ListaVuelos.SetDistanciaSeguridad(distanciaSeguridad);
                ListaVuelos.SetTiempoCiclo(tiempoCiclo);
                MessageBox.Show("Parámetros guardados.");
            }
        }
        private void espacioAéreoTool_Click(object sender, EventArgs e)
        {
            // Revisa los datos de la ListaVuelos y abre FormSimulacion de forma modal con dichos datos si están todos correctamente introducidos.
            if (ListaVuelos == null || ListaVuelos.GetNumber() == 0)
            {
                MessageBox.Show("No se puede acceder al espacio aéreo hasta que haya un archivo de flight plan seleccionado o planes de vuelo configurados.");
                return;
            }

            for (int i = 0; i < ListaVuelos.GetNumber(); i++)
            {
                FlightPlan vuelo = ListaVuelos.GetFlightPlan(i);
                if (vuelo == null)
                {
                    MessageBox.Show("Primero introduce los planes de vuelo.");
                    return;
                }
            }
            if (distanciaSeguridad <= 0 || tiempoCiclo <= 0)
            {
                MessageBox.Show("Primero introduce la distancia de seguridad y el tiempo de ciclo.");
                return;
            }
            FormSimulacion formulario = new FormSimulacion();
            formulario.SetListaVuelos(ListaVuelos);
            formulario.ShowDialog();
        }
        private void btnPruebas_Click(object sender, EventArgs e)
        {
            // Botón de pruebas para comprobar que la simulación funciona correctamente con vuelos de prueba. (Con 2 vuelos)
            ListaVuelos = new FlightPlanList();
            vueloprueba1 = new FlightPlan("VLG1234", "Vueling", 50, 100, 500, 300, 10);
            vueloprueba2 = new FlightPlan("IBE1234", "Iberia", 50, 300, 500, 100, 10);

            int resultado = ListaVuelos.AddFlightPlan(vueloprueba1);
            int resultado2 = ListaVuelos.AddFlightPlan(vueloprueba2);

            distanciaSeguridad = 20;
            tiempoCiclo = 1;
            ListaVuelos.SetDistanciaSeguridad(distanciaSeguridad);
            ListaVuelos.SetTiempoCiclo(tiempoCiclo);


            FormSimulacion formulario = new FormSimulacion();
            formulario.SetListaVuelos(ListaVuelos);
            formulario.ShowDialog();
        }

        // Abre un cuadro de diálogo para seleccionar un fichero de vuelos, lo carga en ListaVuelos y actualiza distanciaSeguridad y tiempoCiclo si los datos son válidos.
        private void cargarPlanesDeVueloDesdeFichero_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Title = "Selecciona el fichero de estado de simulación";
            dialogo.Filter = "Ficheros de texto (*.txt)|*.txt|Todos los ficheros (*.*)|*.*";

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                string rutaFichero = dialogo.FileName;
                ListaVuelos.CargaDeFichero(rutaFichero);

                if (ListaVuelos==null)
                {
                    MessageBox.Show("No se ha podido cargar el fichero. Compruebe que el formato y información sea el correcto");
                }
                else if (ListaVuelos.GetNumber() == 0)
                {
                    MessageBox.Show("El fichero está vacío.");
                }
                else
                {
                    // Los parámetros del fichero tienen prioridad; si no los trae, se mantienen los ya configurados.
                    if (ListaVuelos.GetDistanciaSeguridad() > 0)
                        distanciaSeguridad = (float)ListaVuelos.GetDistanciaSeguridad();
                    if (ListaVuelos.GetTiempoCiclo() > 0)
                        tiempoCiclo = ListaVuelos.GetTiempoCiclo();

                    ListaVuelos.SetDistanciaSeguridad(distanciaSeguridad);
                    ListaVuelos.SetTiempoCiclo(tiempoCiclo);

                    MessageBox.Show("Estado de simulación cargado desde el fichero.");
                }
            }
        }
    }
}