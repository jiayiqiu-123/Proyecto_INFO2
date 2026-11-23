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
    public partial class FormPlanesDeVuelo : Form
    {
        public FlightPlan p1;
        FlightPlanList ListaVuelos;

        public FormPlanesDeVuelo()
        {
            InitializeComponent();
        }
        // GETTERS Y SETTERS
        public void SetFlightPlanList(FlightPlanList lista)
        {
            ListaVuelos = lista;
        }
        public FlightPlanList GetFlightPlanList()
        {
            return ListaVuelos;
        }

        // BOTÓN AGREGAR
        private void Agregar_Click(object sender, EventArgs e)
        {
            if (txtid1.Text == "" || txtCompany1.Text == "" || txtx1.Text == "" || txty1.Text == "" ||
                txtx1F.Text == "" || txty1F.Text == "" || txtVel1.Text == "")
            {
                MessageBox.Show("Faltan datos por rellenar.");
                return;
            }
            if (txtid1.Text.Contains(" ") || txtCompany1.Text.Contains(" "))
            {
                MessageBox.Show("El identificador y la compañía no pueden contener espacios.");
                return;
            }

            try
            {
                double xIni1 = Convert.ToDouble(txtx1.Text);
                double yIni1 = Convert.ToDouble(txty1.Text);
                double xFin1 = Convert.ToDouble(txtx1F.Text);
                double yFin1 = Convert.ToDouble(txty1F.Text);
                double vel1 = Convert.ToDouble(txtVel1.Text);

                if (xIni1 < 0 || xIni1 > 800 || yIni1 < 0 || yIni1 > 550 ||
                    xFin1 < 0 || xFin1 > 800 || yFin1 < 0 || yFin1 > 550)
                {
                    MessageBox.Show("Las coordenadas deben estar dentro del espacio aéreo.");
                    return;
                }

                if (vel1 <= 0)
                {
                    MessageBox.Show("La velocidad debe ser mayor que cero");
                    return;
                }

                p1 = new FlightPlan(txtid1.Text, txtCompany1.Text, xIni1, yIni1, xFin1, yFin1, vel1);
                int resultado = ListaVuelos.AddFlightPlan(p1);
                if (resultado != 0)
                {
                    MessageBox.Show("No se ha podido añadir el plan de vuelo.");
                    return;
                }

                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Has puesto letras o caracteres no válidos en campos numéricos.");
            }
        }

        // BOTÓN CANCELAR
        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
