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

        public void SetFlightPlanList(FlightPlanList listaVuelos)
        {
            this.ListaVuelos = listaVuelos;
        }
        public FormNombrarTXT()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
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

        private void Cancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
