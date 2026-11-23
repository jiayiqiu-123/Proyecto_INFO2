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
    public partial class FormTablaVuelos : Form
    {
        FlightPlanList ListaVuelos = new FlightPlanList();
        public FormTablaVuelos()
        {
            InitializeComponent();
        }
        // Setter para la lista de vuelos, que se usará para rellenar la tabla.
        public void SetListaVuelos(FlightPlanList listaVuelos)
        {
            this.ListaVuelos = listaVuelos;
        }
        // Configura las columnas de la tabla y la rellena con los datos actuales de cada vuelo.
        private void FormTablaVuelos_Load(object sender, EventArgs e)
        {
            dgvVuelos.Columns.Clear();
            dgvVuelos.Columns.Add("Id", "Identificador");
            dgvVuelos.Columns.Add("Company", "Compañía");
            dgvVuelos.Columns.Add("Velocidad", "Velocidad (km/h)");
            dgvVuelos.Columns.Add("PosX", "Posición X");
            dgvVuelos.Columns.Add("PosY", "Posición Y");

            // Permite seleccionar un único vuelo para consultar las distancias
            dgvVuelos.MultiSelect = false;
            dgvVuelos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Permite ver la fila seleccionada con un color
            dgvVuelos.DefaultCellStyle.SelectionBackColor = Color.LightGoldenrodYellow;
            dgvVuelos.DefaultCellStyle.SelectionForeColor = Color.Black;

            for (int i = 0; i < ListaVuelos.GetNumber(); i++)
            {
                FlightPlan vuelo = ListaVuelos.GetFlightPlan(i);

                if (vuelo == null)
                    continue;

                int fila = dgvVuelos.Rows.Add(
                    vuelo.GetId(),
                    vuelo.GetCompany(),
                    vuelo.GetVelocidad().ToString("F2"),
                    vuelo.GetCurrentPosition().GetX().ToString("F2"),
                    vuelo.GetCurrentPosition().GetY().ToString("F2"));

                // Se guarda el vuelo en la fila (Tag) para recuperarlo después al hacer clic
                dgvVuelos.Rows[fila].Tag = vuelo;
            }
        }

        // Al hacer clic en una fila, abre el formulario de distancias del vuelo seleccionado.
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            FlightPlan vueloSeleccionado = dgvVuelos.Rows[e.RowIndex].Tag as FlightPlan;
            if (vueloSeleccionado == null)
                return;

            FormDistancia formulario = new FormDistancia();
            formulario.SetVueloSeleccionado(vueloSeleccionado);
            formulario.SetListaVuelos(ListaVuelos);
            formulario.ShowDialog();
        }
    }
}

