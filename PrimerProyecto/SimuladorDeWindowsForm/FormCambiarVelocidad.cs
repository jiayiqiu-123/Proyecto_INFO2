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
        FlightPlan vuelo1;
        FlightPlan vuelo2;
        // Indica si el usuario ha aceptado algún cambio
        public bool CambiosAceptados = false;
        // Velocidades finales aceptadas por el usuario
        public double Velocidad1Nueva;
        public double Velocidad2Nueva;
        
        public FormCambiarVelocidad(FlightPlan vuelo1, FlightPlan vuelo2)
        {
            InitializeComponent();

            this.vuelo1 = vuelo1;
            this.vuelo2 = vuelo2;

            tablaVuelos.Rows.Add(vuelo1.GetId(),vuelo1.GetVelocidad(),"");
            tablaVuelos.Rows.Add(vuelo2.GetId(),vuelo2.GetVelocidad(),"");
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string textoVelocidad1 = Convert.ToString(tablaVuelos.Rows[0].Cells[2].Value);
            string textoVelocidad2 = Convert.ToString(tablaVuelos.Rows[1].Cells[2].Value);
            double velocidad1Nueva = vuelo1.GetVelocidad();
            double velocidad2Nueva = vuelo2.GetVelocidad();
            try
            {
                if (textoVelocidad1 != "")
                {
                    velocidad1Nueva = Convert.ToDouble(textoVelocidad1);
                    if (velocidad1Nueva <= 0)
                    {
                        MessageBox.Show("La velocidad del vuelo 1 debe ser mayor que 0.");
                        return;
                    }
                }
                if (textoVelocidad2 != "")
                {
                    velocidad2Nueva = Convert.ToDouble(textoVelocidad2);
                    if (velocidad2Nueva <= 0)
                    {
                        MessageBox.Show("La velocidad del vuelo 2 debe ser mayor que 0.");
                        return;
                    }
                }
                Velocidad1Nueva = velocidad1Nueva;
                Velocidad2Nueva = velocidad2Nueva;
                CambiosAceptados = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Introduce velocidades válidas.");
            }
        }
    }
}
