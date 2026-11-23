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
    public partial class FormDistancia : Form
    {
        FlightPlan vueloSeleccionado;
        FlightPlanList listaVuelos;

        public FormDistancia()
        {
            InitializeComponent();
        }

        // Setters para el vuelo seleccionado y la lista de vuelos.
        public void SetVueloSeleccionado(FlightPlan vuelo)
        {
            vueloSeleccionado = vuelo;
        }

        public void SetListaVuelos(FlightPlanList vuelos)
        {
            listaVuelos = vuelos;
        }

        // Al abrir el formulario, muestra la distancia entre el vuelo seleccionado y el resto de vuelos.
        private void FormDistancia_Load(object sender, EventArgs e)
        {
            dgvDistancias.Columns.Clear();

            if (vueloSeleccionado == null || listaVuelos == null)
                return;

            dgvDistancias.Columns.Add("Id", "Vuelo");
            dgvDistancias.Columns.Add("Distancia", vueloSeleccionado.GetId());

            dgvDistancias.ReadOnly = true;
            dgvDistancias.MultiSelect = false;
            dgvDistancias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.Text = "Distancias desde " + vueloSeleccionado.GetId();

            for (int i = 0; i < listaVuelos.GetNumber(); i++)
            {
                FlightPlan vuelo = listaVuelos.GetFlightPlan(i);
                if (vuelo == null || vuelo == vueloSeleccionado)
                    continue;

                double distancia = vueloSeleccionado.Distance(vuelo);
                dgvDistancias.Rows.Add(vuelo.GetId(), distancia.ToString("F2"));
            }
        }
    }
}
