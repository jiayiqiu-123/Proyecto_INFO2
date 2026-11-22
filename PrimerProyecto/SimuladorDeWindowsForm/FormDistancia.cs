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

        public void SetVueloSeleccionado(FlightPlan vuelo)
        {
            vueloSeleccionado = vuelo;
        }

        public void SetListaVuelos(FlightPlanList vuelos)
        {
            listaVuelos = vuelos;
        }

        private void FormDistancia_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();

            if (vueloSeleccionado == null || listaVuelos == null)
                return;

            dataGridView1.Columns.Add("Id", "Vuelo");
            dataGridView1.Columns.Add("Distancia", vueloSeleccionado.GetId());

            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.Text = "Distancias desde " + vueloSeleccionado.GetId();

            for (int i = 0; i < listaVuelos.GetNumber(); i++)
            {
                FlightPlan vuelo = listaVuelos.GetFlightPlan(i);
                if (vuelo == null || vuelo == vueloSeleccionado)
                    continue;

                double distancia = vueloSeleccionado.Distance(vuelo);
                dataGridView1.Rows.Add(vuelo.GetId(), distancia.ToString("F2"));
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
