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
    public partial class FormCambiarVelocidad : Form
    {
        // Vuelos que se mostrarán en la tabla
        FlightPlanList ListaVuelos = new FlightPlanList();
        // Indica si el usuario ha aceptado algún cambio
        public bool CambiosAceptados = false;
        // Velocidades finales aceptadas por el usuario
        public double Velocidad1Nueva;
        public double Velocidad2Nueva;

        public void SetListaVuelos(FlightPlanList listaVuelos)
        {
            this.ListaVuelos = listaVuelos;
        }

        public bool GetCambiosAceptados()
        {
            return CambiosAceptados;
        }

        public FormCambiarVelocidad()
        {
            InitializeComponent();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormCambiarVelocidad_Load(object sender, EventArgs e)
        {
            for(int i = 0; i < ListaVuelos.GetNumber();i++)
            {
                tablaVuelos.Rows.Add(ListaVuelos.GetFlightPlan(i).GetId(), ListaVuelos.GetFlightPlan(i).GetVelocidad(), "");

            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListaVuelos.GetNumber(); i++)
            {
                string texto = Convert.ToString(tablaVuelos.Rows[i].Cells[2].Value);

                if (double.TryParse(texto, out double v) && v > 0)
                {
                    ListaVuelos.GetFlightPlan(i).SetVelocidad(v);
                }
            }

            CambiosAceptados = true;
            this.Close();
        }
    }
}
