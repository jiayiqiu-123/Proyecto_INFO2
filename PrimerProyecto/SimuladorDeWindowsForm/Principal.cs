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
            //se crea el formulario de usuario y se muestra hasta que se cierre
            FormUser f = new FormUser();
            f.ShowDialog();
            //se le pregunta quién ha entrado; si nadie, se cierra el programa
            if (f.GetUsuario() == "")
                this.Close();

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
                ListaVuelos.SetDistanciaSeguridad(distanciaSeguridad);
                ListaVuelos.SetTiempoCiclo(tiempoCiclo);
                MessageBox.Show("Parámetros guardados.");
            }
        }
        private void espacioAéreoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Si no hay vuelos ni por fichero ni configurados manualmente, avisamos
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
            ListaVuelos.SetDistanciaSeguridad(distanciaSeguridad);
            ListaVuelos.SetTiempoCiclo(tiempoCiclo);


            FormSimulacion formulario = new FormSimulacion();
            formulario.SetDSeguridad(distanciaSeguridad);
            formulario.SetTiempoCiclo(tiempoCiclo);
            formulario.SetListaVuelos(ListaVuelos);
            formulario.ShowDialog();
        }

        private void cargarPlanesDeVueloDesdeFicheroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // para abrir el explorador de archivos y seleccionar el fichero
            OpenFileDialog dialogo = new OpenFileDialog();
            // titulo de la ventana
            dialogo.Title = "Selecciona el fichero de planes de vuelo";
            // tipos de fichero que se pueden elegir (texto a mostrar | filtro)
            dialogo.Filter = "Ficheros de texto (*.txt)|*.txt|Todos los ficheros (*.*)|*.*";
            // se muestra el explorador y se comprueba si ha pulsado "abrir"/"aceptar" (no cancelar)
            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                // se obtiene la ruta del fichero seleccionado
                string rutaFichero = dialogo.FileName;
                // se lee el fichero y nos devuelve la lista con los vuelos cargados usando el método que hemos hecho en FlightPlanList
                FlightPlanList listacargada = ListaVuelos.CargaDeFichero(rutaFichero);

                if (listacargada == null)
                {
                    // mensaje general de que el fichero está mal escrito o no existe
                    MessageBox.Show("No se ha podido cargar el fichero. Compruebe que el formato y información sea el correcto");
                }

                else if (listacargada.GetNumber() == 0)
                {
                    // wl fichero no tiene ningún vuelo a cargar, si se abre pero no hay nada dentro, devolviendo una lista vacía
                    MessageBox.Show("El fichero está vacío.");
                }

                else
                {
                    // la lista de vuelos cargada desde el fichero es válida
                    ListaVuelos = listacargada;
                    // parámetros de seguridad por defecto, si no se han cargado desde el fichero? (REVISAR)
                    ListaVuelos.SetDistanciaSeguridad(distanciaSeguridad);
                    ListaVuelos.SetTiempoCiclo(tiempoCiclo);
                    MessageBox.Show("Planes de vuelo cargados desde el fichero.");
                }

            }

        }
    }
}