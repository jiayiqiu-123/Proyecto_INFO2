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
        public void SetListaVuelos(FlightPlanList listaVuelos)
        {
            this.ListaVuelos = listaVuelos;
        }
        private void FormTablaVuelos_Load(object sender, EventArgs e)
        {
            dgvVuelos.Columns.Clear();
            dgvVuelos.Columns.Add("Id", "Identificador");
            dgvVuelos.Columns.Add("Velocidad", "Velocidad (km/h)");
            dgvVuelos.Columns.Add("PosX", "Posición X");
            dgvVuelos.Columns.Add("PosY", "Posición Y");

            // Permite la selección múltiple de filas
            dgvVuelos.MultiSelect = true;
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
                    vuelo.GetVelocidad().ToString("F2"),
                    vuelo.GetCurrentPosition().GetX().ToString("F2"),
                    vuelo.GetCurrentPosition().GetY().ToString("F2"));

                // Guardamos la fila seleccionada
                dgvVuelos.Rows[fila].Tag = vuelo;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvVuelos.SelectedRows.Count != 2)
                return;

            FlightPlan vuelo1 = dgvVuelos.SelectedRows[0].Tag as FlightPlan;
            FlightPlan vuelo2 = dgvVuelos.SelectedRows[1].Tag as FlightPlan;

            if (vuelo1 == null || vuelo2 == null)
                return;

            double distancia = vuelo1.Distance(vuelo2);

            FormDistancia formulario = new FormDistancia();
            string a ="La distancia entre " + vuelo1.GetId() + " y " + vuelo2.GetId() + " es: " + distancia.ToString("F2") + " km";
            formulario.SetText(a);
            formulario.ShowDialog();
        }

        private void dgvVuelos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

