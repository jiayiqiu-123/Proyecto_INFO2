using FlightLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuladorDeWindowsForm
{
    public partial class FormNombrarTXT : Form
    {
        FlightPlanList ListaVuelos = new FlightPlanList();

        // Permite establecer la lista de vuelos que se guardará en el archivo .txt.
        public void SetFlightPlanList(FlightPlanList listaVuelos)
        {
            this.ListaVuelos = listaVuelos;
        }
        public FormNombrarTXT()
        {
            InitializeComponent();
        }

        // Guarda la lista de vuelos en un fichero .txt con el nombre introducido.
        private void Guardar_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Por favor, ingrese un nombre de archivo válido.");
                return;
            }

            string ruta = textBox1.Text.Trim();
            if (!ruta.EndsWith(".txt"))
                ruta += ".txt";

            try
            {
                ListaVuelos.GuardarEnArchivo(ruta);
                MessageBox.Show("Lista de vuelos guardada correctamente en " + ruta);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la lista de vuelos: " + ex.Message);
            }
        }

        // Cierra el formulario sin guardar.
        private void Cancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
