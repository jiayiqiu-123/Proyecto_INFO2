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
        public FlightPlan p1;
        public FlightPlan p2;
        public FormTablaVuelos()
        {
            InitializeComponent();
        }

        private void FormTablaVuelos_Load(object sender, EventArgs e)
        {
            dgvVuelos.Columns.Clear();
            dgvVuelos.Columns.Add("Id", "Identificador");
            dgvVuelos.Columns.Add("Velocidad", "Velocidad (km/h)");
            dgvVuelos.Columns.Add("PosX", "Posición X");
            dgvVuelos.Columns.Add("PosY", "Posición Y");

            if (p1 != null)
            {
                dgvVuelos.Rows.Add(p1.GetId(), p1.GetVelocidad().ToString("F2"), p1.GetCurrentPosition().GetX().ToString("F2"), p1.GetCurrentPosition().GetY().ToString("F2"));
            }

            if (p2 != null)
            {
                dgvVuelos.Rows.Add(p2.GetId(), p2.GetVelocidad().ToString("F2"), p2.GetCurrentPosition().GetX().ToString("F2"), p2.GetCurrentPosition().GetY().ToString("F2"));
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Comprobamos que el clic sea en una fila y no en la cabecera (e.RowIndex >= 0)
            if (e.RowIndex >= 0 && p1 != null && p2 != null)
            {
                // Calculamos la distancia usando el método Distance que está en el FlightPlan
                double dist = p1.Distance(p2);

                // Abrimos FormDistancia y le asignamos el texto directo al label público
                FormDistancia fDist = new FormDistancia();
                fDist.labelDistancia.Text = "La distancia entre " + p1.GetId() + " y " + p2.GetId() + " es: " + dist.ToString("F2") + " km";
                fDist.ShowDialog();
            }
        }

        private void dgvVuelos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

